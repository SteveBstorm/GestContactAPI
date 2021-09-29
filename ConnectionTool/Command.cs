using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionTool
{
    

    public class Command
    {
        private Dictionary<string, object> _params;
        private bool _stored;
        private string _query;

        public Command(string query, bool isStoredProcedure = false)
        {
            //je vérifie que ma query est bien présente
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Query cant' be null!");
            }
            _query = query;
            _stored = isStoredProcedure;
            _params = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Params
        {
            get
            {
                return _params;
            }
        }

        public bool Stored
        {
            get
            {
                return _stored;
            }
        }

        public string Query
        {
            get
            {
                return _query;
            }
        }

        public void AddParameter(string parameterName, object value)
        {
            //je vérifie si le paramtre reçu n'est pas null ou absent
            if(parameterName == null || parameterName.Trim().Length == 0)
            {
                throw new ArgumentException("Paremeter can't be null");
            }
            //je vérifie s'il n'existe pas déjà dans mon dictionnaire
            if (_params.ContainsKey(parameterName))
            {
                throw new MissingMemberException($"Paramater {parameterName} already exist");
            }

            //j'ajoute mon parametre et sa valeur à mon dictionnaire
            //afin de pouvoir envoyer une valeur nulle en db, j'utilise le coalesce
            //le null code et le null db sont différent
            _params.Add(parameterName, value ?? DBNull.Value);
            //_params.Add(parameterName, value is null ? DBNull.Value : value);
        }
    }
}
