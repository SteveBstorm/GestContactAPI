/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
EXEC RegisterUser @LastName ='Adnet',
                  @FirstName ='Geoffroy',
                  @Email = 'mail@mail.be',
                  @Passwd = 'Azerty123456!'

EXEC AddContact @LastName = 'Michel',
                  @FirstName = 'Aurélie',
                  @Email = 'mamour@mail.com',
                  @BirthDate = '1984-03-12',
                  @Phone = '+32465656565',
                  @UserId = 1
