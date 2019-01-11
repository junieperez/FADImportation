using System;
using MySql.Data.MySqlClient;

namespace FAD_Importation.CLASSES
{
    //a class that manipulates location entries in (.dbf) and mysql
    public class cLocation
    {
        //properties for the table location (properties are based on columns in .dbf)
        public string _locno { get; set; }
        public string _code { get; set; }
        public string _name { get; set; }
        public string _comHed { get; set; }
        public string _main { get; set; }
        public string _navcd { get; set; }
        public string _addr { get; set; }
        public string _conper { get; set; }
        public string _connum { get; set; }
        public string _email { get; set; }
        public string _web { get; set; }
        public string _user { get; set; }
        public string _dupdat { get; set; }

        public cLocation()
        {

        }

        //set location details based on location code
        //retrieved on mysql location table
        public void setLocationDetails()
        {
            cMySQLCommands osetLocationDetails = new cMySQLCommands();
            MySqlDataReader reader;

            osetLocationDetails._fields = "";
            osetLocationDetails._tblName = "location";
            osetLocationDetails._condition = "location.loc_code='" + _code.TrimEnd()+"'";

            reader = osetLocationDetails.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _name = reader.GetString(2);
                    _comHed = reader.GetString(3);
                    _main = reader.GetString(4);
                    _navcd = reader.GetString(5);
                    _addr = reader.GetString(6);
                    _conper = reader.GetString(7);
                    _connum = reader.GetString(8);
                    _email = reader.GetString(9);
                    _web = reader.GetString(10);
                    _user = reader.GetString(11);
                    _dupdat = reader.GetString(12);
                }
            }
        }
    }

    //a class that inherits location
    //used for configuration per location
    public class cLocationConfig : cLocation
    {
        //location config properties
        public string _LocCode { get; set; }
        public string _Database { get; set; }
        public string _Masterfile { get; set; }
        public string _CNVPICT { get; set; }
        public string _SERVER { get; set; }
        public string _DOMAIN { get; set; }
        public string _DBFUSER { get; set; }
        public string _DBFPASS { get; set; }
        public string _DBNAME { get; set; }
        public string _DBUSER { get; set; }
        public string _DBPASS { get; set; }
        public string _SELLOCCODE { get; set; }
        public string _SERVERLOC { get; set; }

        public void setLocationConfig()
        {
            cMySQLCommands oMySQLCommands = new cMySQLCommands();
            MySqlDataReader reader;

            oMySQLCommands._fields = "";
            oMySQLCommands._tblName = " locationconfig ";
            oMySQLCommands._condition = " loc_code = '"+ _LocCode + "'";

            reader = oMySQLCommands.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _Database = reader.GetString(2);
                    _Masterfile = reader.GetString(3);
                    _CNVPICT= reader.GetString(4);
                    _SERVER = reader.GetString(5);
                    _DOMAIN = reader.GetString(6);
                    _DBFUSER = reader.GetString(7);
                    _DBFPASS = reader.GetString(8);
                    _DBNAME = reader.GetString(9);
                    _DBUSER = reader.GetString(10);
                    _DBPASS = reader.GetString(11);
                    _SELLOCCODE = reader.GetString(12);
                    _SERVERLOC = reader.GetString(13);
                }
            }
        }

        public string[] setAllLocation()
        {
            string[] addedLoc = { "" };
            cMySQLCommands oMySQLCommands = new cMySQLCommands();
            MySqlDataReader reader;

            oMySQLCommands._tblName = " locationconfig ";
            oMySQLCommands._orderby = " locationConfig.loc_database ";

            reader = oMySQLCommands.selectQuery();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Array.Resize(ref addedLoc, addedLoc.Length + 1);
                    addedLoc[addedLoc.Length - 1] = reader.GetString(1);
                }
            }
            return addedLoc;
        }

        public string[] setLocationPerServer()
        {
            string[] addedLoc = { "" };
            cMySQLCommands oMySQLCommands = new cMySQLCommands();
            MySqlDataReader reader;

            oMySQLCommands._fields = "";
            oMySQLCommands._tblName = " locationconfig ";
            oMySQLCommands._condition = " locationconfig.serverloc = '"+ _SERVERLOC + "'";
            oMySQLCommands._orderby = " locationConfig.loc_database ";

            reader = oMySQLCommands.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Array.Resize(ref addedLoc, addedLoc.Length + 1);
                    addedLoc[addedLoc.Length - 1] = reader.GetString(1);
                }
            }
            return addedLoc;
        }

        public string[] getAllServer()
        {
            string[] addedLoc = { "" };
            cMySQLCommands oMySQLCommands = new cMySQLCommands();
            MySqlDataReader reader;

            oMySQLCommands.addFields(" DISTINCT(serverloc) ");
            oMySQLCommands._tblName = "locationconfig";
            oMySQLCommands._orderby = "locationConfig.serverloc";

            reader = oMySQLCommands.selectQuery();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Array.Resize(ref addedLoc, addedLoc.Length + 1);
                    addedLoc[addedLoc.Length - 1] = reader.GetString(0);
                }
            }
            return addedLoc;
        }
    }
}
