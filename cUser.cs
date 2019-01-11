using MySql.Data.MySqlClient;
using System.Data.OleDb;
using System;

namespace FAD_Importation.CLASSES
{
    public class cUser
    {
        public string _userlogin{get;set;}
        public string _fullname{get;set;}
        public string _password{get;set;}
        public string _flag{get;set;}
        public string _sflag{get;set;}
        public string _location { get; set; }
        public string _locationName { get; set; }
        public string _department { get; set; }
        public string _departmentName { get; set; }
        public string _position{get;set;}
        public string _passEncrypt { get; set; }

        private string setDBManual = Config.Default.DATABASE;


        public bool verifyLogin()
        {
            MySqlDataReader reader;
            MD5Sample oMD5 = new MD5Sample();
            oMD5._source = _password;
            cMySQLCommands cMySQLVerify = new cMySQLCommands();
            cMySQLVerify._tblName = "usertab";
            cMySQLVerify._fields = "";
            

            cMySQLVerify._condition = "usertab.usr_login = '" + _userlogin + "' and usertab.usr_pass= '" + oMD5.GetMd5Hash() + "'";
            //oMD5.GetMd5Hash()
            reader = cMySQLVerify.selectQueryWhere();
            if (reader.HasRows) {
                while (reader.Read())
                {
                    _userlogin = reader.GetString(1);
                    _fullname = reader.GetString(2);
                    _location = reader.GetString(6);
                    _department = reader.GetString(7);
                    _passEncrypt = reader.GetString(3);
                    _position = reader.GetString(8);
                    _locationName = getLocation();
                    _departmentName = getCompany();
                }
                return true;
            }
            else
            {
                reader.Dispose();
                return false; 
            }
        }

        public bool verifyUsername()
        {
            MySqlDataReader reader;
            cMySQLCommands cMySQLVerify = new cMySQLCommands();
            cMySQLVerify._tblName = "usertab";
            cMySQLVerify._fields = "";

            cMySQLVerify._condition = "usertab.usr_login = '" + _userlogin + "'";
            reader = cMySQLVerify.selectQueryWhere();
            if (reader.HasRows) { return true; }
            else
            {
                reader.Dispose();
                return false; 
            }
        }

        public void changePassword()
        {

            string newPasswordEncrypt; 
            MD5Sample oMD5 = new MD5Sample();
            oMD5._source = _password;
            newPasswordEncrypt = oMD5.GetMd5Hash();
            cMySQLCommands cMySQLChangePassword = new cMySQLCommands();

            cMySQLChangePassword._tblName = "usertab";

            cMySQLChangePassword._fields = "usertab.usr_pass";
            cMySQLChangePassword._values = "'"+ newPasswordEncrypt + "'";
            cMySQLChangePassword._condition = ("usertab.usr_login = '"+ _userlogin +"'");
            cMySQLChangePassword.updateQuery();
        }

        public void loadUser()
        {
            MySqlDataReader reader;
            cMySQLCommands cMySQLLoad = new cMySQLCommands();
            cMySQLLoad._tblName = "usertab";
            cMySQLLoad._fields = "";

            cMySQLLoad._condition = "usertab.usr_login = '" + _userlogin + "'";
            reader = cMySQLLoad.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _fullname = reader.GetString(2);
                    _password = reader.GetString(3);
                    _location = reader.GetString(6);
                    _department = reader.GetString(7);
                    _position = reader.GetString(8);
                }
            }
        }

        public string getLocation()
        {
            DBConnection dbCon = new DBConnection();
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;
            string ret = "";
            oMySQLCommand._fields = "location.loc_name";
            oMySQLCommand._tblName = "usertab";
            oMySQLCommand._innerTableName = "location";
            oMySQLCommand._innerLeftOn = "usertab.usr_loc";
            oMySQLCommand._innerRightOn = "location.loc_code";

            reader = oMySQLCommand.innerSelectQuery();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    ret = reader.GetString(0);
                }
            }
            return ret;
        }

        public string getCompany()
        {
            DBConnection dbCon = new DBConnection();
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;
            string ret = "";
            oMySQLCommand._fields = "company.com_name";
            oMySQLCommand._tblName = "usertab";
            oMySQLCommand._innerTableName = "company";
            oMySQLCommand._innerLeftOn = "usertab.usr_dept";
            oMySQLCommand._innerRightOn = "company.com_code";

            reader = oMySQLCommand.innerSelectQuery();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    ret = reader.GetString(0);
                }
            }
            return ret;
        }

        public void addToTable()
        {
            cMySQLCommands oMySQLCommands = new cMySQLCommands();
            cMySQLCommands mySQLAddNewUser = new cMySQLCommands();

            MySqlDataReader reader;
            string[] addedUser = { "" };
            
            mySQLAddNewUser.addFields("usertab.usr_login");
            mySQLAddNewUser.addFields("usertab.usr_name");
            mySQLAddNewUser.addFields("usertab.usr_pass");
            mySQLAddNewUser.addFields("usertab.usr_loc");
            mySQLAddNewUser.addFields("usertab.usr_dept");
            mySQLAddNewUser.addFields("usertab.usr_pos");
            mySQLAddNewUser._tblName = "usertab";

            oMySQLCommands._fields = "";
            oMySQLCommands._tblName = "usertab";
            oMySQLCommands._condition = "usertab.usr_login = '" + _userlogin + "'";
            reader = oMySQLCommands.selectQueryWhere();

            if (reader.HasRows == false)
            {

                cOleDBCommand oOledbCommand = new cOleDBCommand();
                OleDbDataReader dbReader;
                oOledbCommand.setConStr(setDBManual);
                dbReader = oOledbCommand.oleDBQueryReturn("SELECT * FROM usertab WHERE alltrim(upper(usertab.usr_login)) = '" + _userlogin + "'");
                if (dbReader.HasRows)
                {
                    if (dbReader.Read())
                    {
                        mySQLAddNewUser._values = "";
                        mySQLAddNewUser.addValues(dbReader.GetString(0));
                        mySQLAddNewUser.addValues(dbReader.GetString(1));
                        mySQLAddNewUser.addValues("7007a91dd5b0e797a63ad8527b73f108");
                        mySQLAddNewUser.addValues(dbReader.GetString(5));
                        mySQLAddNewUser.addValues(dbReader.GetString(6));
                        mySQLAddNewUser.addValues(dbReader.GetString(7));
                        mySQLAddNewUser.insertQuery();
                    }
                }

            }
        }
    }
}
