using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace FAD_Importation.CLASSES
{
    public class cUserRole
    {
        cMySQLCommands mySqlCom = new cMySQLCommands();
        public string _uroleno { get; set; }
        public string _urlLogin { get; set; }
        public string _urlCode { get; set; }

        public cUserRole()
        {
            mySqlCom._tblName = "userrole";
            mySqlCom._fields = "";
        }

        public bool verifyUserAccess()
        {
            mySqlCom._condition = "userrole.url_login='" + _urlLogin + "' and userrole.url_code = '"+ _urlCode + "'";
            MySqlDataReader reader = mySqlCom.selectQueryWhere();
            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
