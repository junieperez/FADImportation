using MySql.Data.MySqlClient;

namespace FAD_Importation.CLASSES
{
    //Class used to manipulate items from table
    public class cItem
    {
        //pre-load | set table item_mst in querying
        public cItem()
        {
            mySqlCom._tblName = "item_mst";
            mySqlCom._fields = "";
        }

        //item properties
        cMySQLCommands mySqlCom = new cMySQLCommands();
        public string _code { get; set; }
        public string _description { get; set; }
        public string _specs { get; set; }
        public string _itmtype { get; set; }
        public string _estlife { get; set; }
        public string _itmcat { get; set; }
        public string _itmuom { get; set; }

        //set | provide details of item retrieved from mysql copied from dbf
        //msaterfile. details will be provided and setted to properties
        public void getDetails()
        {
            MySqlDataReader reader;
            mySqlCom._condition = "itm_code="+_code;
            reader = mySqlCom.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _description = reader.GetString(2);
                    _specs = reader.GetString(3);
                    _itmuom = reader.GetString(5);
                    _estlife = reader.GetString(7);
                    _itmtype = reader.GetString(8);
                    _itmcat = reader.GetString(9);
                }
            }
        }
    }


}
