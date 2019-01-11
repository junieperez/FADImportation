using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace FAD_Importation.CLASSES
{
    //a class used to manipulate the table company
    public class cCompany
    {
        //properties in the company table
        cMySQLCommands mySqlCom = new cMySQLCommands();
        public string _code { get; set; }
        public string _name { get; set; }
        public string _locCode { get; set; }
        public string _navdeptcd { get; set; }
        public string _navdeptyp { get; set; }
        public string _comprccd { get; set; }
        public string _comhead1 { get; set; }
        public string _comhead2 { get; set; }
        public string _comaddr { get; set; }
        public string _comtelno { get; set; }
        public string _comuseg { get; set; }
        public string _comconper { get; set; }
        public string _commobil { get; set; }
        public string _comemail { get; set; }
        public string _comuser { get; set; }
        public string _comdupdat { get; set; }
        public string _comfad { get; set; }

        //retrieves the department details based on the current
        //company code provided and sets the details on the properties
        public void setDepartmentDetails()
        {
            cMySQLCommands osetDeptDetails = new cMySQLCommands();
            MySqlDataReader reader;

            osetDeptDetails._fields = "";
            osetDeptDetails._tblName = "company";
            osetDeptDetails._condition = "company.com_code='"+_code+"'";

            reader = osetDeptDetails.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _code = reader.GetString(1);
                    _name = reader.GetString(2);
                    _locCode = reader.GetString(3);
                    _navdeptcd = reader.GetString(4);
                    _navdeptyp = reader.GetString(5);
                    _comprccd = reader.GetString(6);
                    _comhead1 = reader.GetString(7);
                    _comhead2 = reader.GetString(8);
                    _comaddr = reader.GetString(9);
                    _comtelno = reader.GetString(10);
                    _comuseg = reader.GetString(11);
                    _comconper = reader.GetString(12);
                    _commobil = reader.GetString(13);
                    _comemail = reader.GetString(14);
                    _comuser = reader.GetString(15);
                    _comdupdat = reader.GetString(16);
                    _comfad = reader.GetString(17);
                }
            }
        }

        public void deptNametoDeptCode()
        {
            cMySQLCommands osetDeptDetails = new cMySQLCommands();
            MySqlDataReader reader;

            osetDeptDetails._fields = "";
            osetDeptDetails._tblName = "company";
            osetDeptDetails._condition = "company.com_name='" + _name + "'";

            reader = osetDeptDetails.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _code = reader.GetString(1);
                }
            }
        }
    }
}