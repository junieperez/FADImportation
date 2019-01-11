using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace FAD_Importation.CLASSES
{
    public class cModOfPayment
    {
        public string _docno { get; set; }
        public string _pnum { get; set; }
        public string _pcode     { get; set; }
        public string _pname { get; set; }
        public string _pmemo { get; set; }
        public string _puser { get; set; }
        public string _pdate { get; set; }

        public void getPMasterDetails()
        {
            DBConnection dbCon = new DBConnection();
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;
            oMySQLCommand._fields = "";
            oMySQLCommand._tblName = "pmaster";
            oMySQLCommand._condition = "pmaster.p_code = '" + _pcode + "'";

            reader = oMySQLCommand.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _docno = reader.GetString(0);
                    _pnum = reader.GetString(1);
                    _pname = reader.GetString(3);
                    _pmemo = reader.GetString(4);
                    _puser = reader.GetString(5);
                    _pdate = reader.GetString(6);
                }
            }
        }
    }
}
