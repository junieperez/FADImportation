using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
namespace FAD_Importation.CLASSES
{
    class cLoadDBF
    {
        //declare used variables
        MySqlCommand coundata = new MySqlCommand();
        DBConnection dbcon = new DBConnection();
        BindingSource bindingSource1 = new BindingSource();
        DataTable YourResultSet = new DataTable();
        int i,tblFieldCount,tblEntryCount;

        public string _tableFilePath { get; set; }
        public string _tableFileName { get; set; }
        public string _directorySource { get; set; }
        public string[] _tableFileNames { get; set; }
        public DataGridView listHolder {get;set;}
      

        public void loadSingleTable()
        {
            //create an oleDBFoxpro Provider
            string constr = "Provider=VFPOLEDB;Data Source=" + _tableFilePath + ";Extended Properties=dBASE IV;User ID=Admin;Password='';";
            var sql = setTableQuery(_tableFileName);

            //use to count number of entries
            var tblECount = dbcon.commandQuery(@"SELECT count(*) FROM " + _tableFileName + "");
            using (MySqlDataReader reader = tblECount)
            {
                while (reader.Read())
                {
                    tblEntryCount = reader.GetInt32(0);
                    if (tblEntryCount > 0)
                    {
                        tblEntryCount = tblEntryCount - 1;
                    }
                }
            }

            //count number of columns
            using (OleDbConnection con = new OleDbConnection(constr))
            {
                var tbColCount = dbcon.commandQuery(@"SELECT count(*) FROM information_schema.columns WHERE table_name = '" + _tableFileName + "'");
                 using (MySqlDataReader reader = tbColCount)
                        {
                            while (reader.Read())
                            {
                                tblFieldCount = reader.GetInt32(0);
                            }
                        }

                 //read .dbf table 
                 OleDbCommand cmd = new OleDbCommand(sql, con);
                 con.Open();
                 YourResultSet.Load(cmd.ExecuteReader());
                 con.Close();
                        if (YourResultSet.Rows.Count >0) //check number of entries
                        {
                            try
                            {
                                //initialize .dbf processing
                                MySqlCommand newcmd = dbcon.Connection.CreateCommand();

                                int f;
                                string useCmdText = "";
                                string newKey = "";
                                useCmdText = "INSERT INTO " + _tableFileName + " values(";

                                 //process INSERT INTO query'ing to sql string and adding number of columns
                                for (f = 1; f <= tblFieldCount; f++)
                                {
                                    newKey = "@f" + Convert.ToString(f);
                                    if (f < tblFieldCount)
                                    {
                                        useCmdText = useCmdText + newKey + ",";
                                    }
                                    else
                                    {
                                        useCmdText = useCmdText + newKey + ")";
                                    }
                                    newcmd.Parameters.Add(newKey, MySqlDbType.VarChar);

                                }

                                 //process entries and adding values to parameters/fields added
                                for (i = tblEntryCount; i < this.YourResultSet.Rows.Count - 1; i++)
                                {
                                    for (f = 1; f < tblFieldCount; f++)
                                    {
                                         object o = YourResultSet.Rows[i].ItemArray[f-1];

                                        newKey = "@f" + Convert.ToString(f);
                                        if (o != null)
                                        {
                                            newcmd.Parameters[newKey].Value = Convert.ToString(o);
                                        }
                                        else
                                        {
                                            newcmd.Parameters[newKey].Value = "";
                                        }
                                    }
                                    newcmd.CommandText = useCmdText;
                                    newcmd.ExecuteNonQuery();
                                }
                            }
                          catch (MySqlException er)
                            {
                                MessageBox.Show("Error:" + er.ToString());
                            }

                            }

                         } 
        }

        //used to select specific sql query for numeric to decimal values,memo
        private string setTableQuery(string findTable)
        {
            MySqlDataReader reader;
            string useThisQuery = "SELECT * FROM " + findTable;
            reader = dbcon.commandQuery("SELECT sq_useQuery from tbl_setDBFQuery WHERE sq_name='" + findTable + "'");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    useThisQuery = reader.GetString(0);
                }
            }
            return useThisQuery;
        }
    }
}
