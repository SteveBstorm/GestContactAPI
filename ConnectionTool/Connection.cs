using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionTool
{
    public class Connection
    {
        private string _connectionString;

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public Connection(string connectionString)
        {
            _connectionString = connectionString;
            using(SqlConnection connection = CreateConnection())
            {
                connection.Open();
            }
        }

        //afin d'éviter de répéter le code de la connection je crée une méthode que j'appel dans mon constructeur
        //pour y créer et y ouvrir ma connection directement
        private SqlConnection CreateConnection()
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConnectionString;
            return sqlConnection;
        }

        //afin d'éviter de répeter le code de création de commande j'utiliserai cette méthode
        private SqlCommand CreateCommand(Command command, SqlConnection connection)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = command.Query;
            //pour rappel , si je suis dans le cadre d'un procédure sotckée je doit lui donner le type nécessaire
            if (command.Stored)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            //je vais paracourir mon tableau de paramètre
            //et les ajouter à ma commande
            foreach(KeyValuePair<string, object> kvp in command.Params)
            {
                //je vais avoir besoin d'un objet sqlparameter pour définir le parametre
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = kvp.Key;
                parameter.Value = kvp.Value;
                //j'ajoute le parametre à ma sqlcommand
                cmd.Parameters.Add(parameter);
            }
            return cmd;
        }

        public object ExecuteScalar(Command command)
        {
            //je vais utiliser un sql connection créée grace à ma méthode createconnection
            using(SqlConnection connection = CreateConnection())
            {
                //j'utilise une commande crée avecm éthode createcommand
                using(SqlCommand cmd = CreateCommand(command, connection))
                {
                    //j'ouvre la connexion
                    connection.Open();
                    //je stock le résultat de l'executescalara de la commande dans un objet
                    object result = cmd.ExecuteScalar();
                    //si l'objet est un dbnull, je renvois un null code, sinon je renvois l'objet
                    return (result is DBNull) ? null : result;
                }
            }
        }

        public int ExecuteNonQuery(Command command)
        {
            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(command, connection))
                {
                    connection.Open();
                    //je renvois directement le résultat de l'execute non query (le nombre de lignes affectées)
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<TResult> ExecuteReader<TResult>(Command command, Func<IDataRecord, TResult> selector)
        {
            using(SqlConnection connection = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(command, connection))
                {
                    connection.Open();
                    //le sqlreader c'estce qui me permet de récupérer et renvoyer plusieur valeurs
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //si je mets un poitn d'arrêt ici, je pourrai voir que la méthode ne fonctionne pas
                        //car ensortant de la méthode la connection se ferme
                        //du coup je ne peux pas récupérer les éléments du reader
                        //et comme vous l'avez vu , le reader à besoin d'une connection ouverte pour tout parcourir
                        //List<TResult> results = new List<TResult>();
                        //while (reader.Read())
                        //{
                        //    results.Add(selector(reader));
                        //}
                        //return results;
                        while (reader.Read())
                        {
                            yield return selector(reader);
                        }
                        //le yield return indique que vous pourrez itérer sur ce que vous retournerez au moment où vous l'utiliserez
                    }
                }
            }
        }


        public DataTable GetDataTable(Command command)
        {
            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(command, connection))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}
