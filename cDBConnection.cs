using System;
using System.Configuration;
using System.Windows.Forms;


namespace FAD_Importation
{
    using MySql.Data.MySqlClient;  

    namespace CLASSES
    {
        //class used to connect to the database
        public class DBConnection
        {                                                                                                                                                                                                                 
            //declare or set all variables from config default before using
            public string connstring { get; set; }
            public string serverName ="172.16.161.37"; //"172.16.161.37"; 
            //"172.16.43.95"  
             //  
            //Co nfig.Default.SERVER; 

            public string databaseName = "db_importation";
            public string dbUsername = "db_fad_junie";
            public string dbPassword = "pass";
            //constructor to prepare connection string in this class
            public DBConnection() 
            {
                //use variables declared above to create a connection string
                string connstring = string.Format("Server=" + serverName + "; database= '"+ databaseName.ToString() + 
                                    "' ; UID= '" + dbUsername.ToString() + "'; password= '" + dbPassword.ToString() + 
                                    "' ; Connection Timeout = 120000;");

                connection = new MySqlConnection(connstring);
             }

            //method used to change connection from database
            public void DBConnectionChange(string consstring)
            {
                if (consstring != "")
                {
                    string connstring = consstring;
                    connection = new MySqlConnection(connstring);
                }
                else
                {
                    string connstring = string.Format("Server=" + serverName + "; database=" + databaseName.ToString() + "; UID=" + dbUsername.ToString()
                    + "; password=" + dbPassword.ToString() + "; Connection Timeout = 120000;");
                    connection = new MySqlConnection(connstring);
                }
            }

            public string Password { get; set; }
            //method used to return connection.
            private MySqlConnection connection = null;

            public MySqlConnection Connection
            {
                get { return connection; }
            }


            private static DBConnection _instance = null;
 
            public static DBConnection Instance()
            {
                if (_instance == null)
                    _instance = new DBConnection();
                return _instance;
            }

            //used to check the connection to the database, if connection is success
            //it will return true else false
            public bool IsConnect()
            {
                if (Connection == null)
                {
                    if (String.IsNullOrEmpty(databaseName))
                    return false;
            connection = new MySqlConnection(connstring);
                    connection.Open();
                }

                return true;
            }

            //used to close the connection
            public void Close()
            {

                connection.Close();
            }

            //used to open the connection
            public void Open()
            {
               connection = new MySqlConnection(connstring);
               connection.Open();
            }

            //used to send a non-return query to the database such as INSERT, UPDATE, DELETE, TRUNCATE etc.
            public void commandNonQuery(string cmdString)
            {
                connection.Close();
                connection.Open();
                MySqlCommand command= connection.CreateCommand();
                command.CommandText = cmdString;
                command.ExecuteNonQuery();
                connection.Close();
            }

            //used to send a query with return from the database such as SELECT, COUNT etc.
            public MySqlDataReader commandQuery(string cmdString)
            {
                MySqlDataReader reader;
                connection.Close();
                connection.Open();

                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = cmdString;

                reader = cmd.ExecuteReader();
                return reader;
            }
            
        }
    }
}
