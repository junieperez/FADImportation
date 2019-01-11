using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;

namespace FAD_Importation.CLASSES
{
    public class cOleDBCommand
    {
        DBConnection dbcon = new DBConnection();
        public string _constr;
        public string dbUser = Config.Default.DBFUSERNAME;
        public string dbDomain = Config.Default.DBFDOMAIN;
        public string dbPass = Config.Default.DBFPASSWORD;

        public string _dirStorage = Config.Default.DATABASE; //ClassConfig.Instance.Configs[Config.Default.DATABASE];

        public void setConStr()
        {
            _constr = "Provider=VFPOLEDB;Data Source=" + _dirStorage + ";Extended Properties=dBASE IV;User ID=Admin;Password='';Collating Sequence=general";
        }

        public void setConStr(string manualSetConstr)
        {
            _constr = @"Provider=VFPOLEDB;Data Source=" + manualSetConstr + ";Extended Properties=dBASE IV;User ID=Admin;Password='';Collating Sequence=general";
        }

        public OleDbDataReader oleDBQueryReturn(string commandQuery)
        {
            using (new ImpersonateUser(dbUser, dbDomain, dbPass))
            {
                OleDbConnection con = new OleDbConnection(_constr);
                OleDbDataReader reader;
                using (OleDbCommand command = con.CreateCommand())
                {
                    command.CommandText = commandQuery;
                    con.Open();
                    reader = command.ExecuteReader();
                    //con.Close();
                }
            return reader;
            }
        }

        public void oleDBQueryNonReturn(string commandQuery)
        {
            using (new ImpersonateUser(dbUser, dbDomain, dbPass))
            {
                OleDbConnection con = new OleDbConnection(_constr);
                using (OleDbCommand command = con.CreateCommand())
                {
                    command.CommandText = commandQuery;
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void setOleDbfValues(ref List<setDBFValues> dbfValueList)
        {
            foreach (setDBFValues sdf in dbfValueList)
            {
                if (sdf.valueType == "DATE")
                {
                    sdf.valueName = "CTOD('"+ sdf.valueName+"')";
                }
                if (sdf.valueType == "STRING")
                {
                    sdf.valueName = "'" + sdf.valueName + "'";
                }
                if (sdf.valueType == "BOOL")
                {
                    if (sdf.valueName == null || sdf.valueName == "False")
                    { sdf.valueName = ".f."; }
                    else
                    { sdf.valueName = ".t."; }
                }
                if (sdf.valueType == "DATETIME")
                {
                    sdf.valueName = "CTOT('" + sdf.valueName + "')";
                }
                if (sdf.valueType == "NUMERIC")
                {
                    if (sdf.valueName == null) 
                    { sdf.valueName = "0"; }
                }
            }
        }


    }
    public class setDBFValues:cOleDBCommand
    {
        public string valueName;
        public string valueType;
    }


}