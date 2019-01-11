using MySql.Data.MySqlClient;
using System.Collections.Generic;


namespace FAD_Importation.CLASSES
{                                                                     
    public class cSERS
    {
        public string _sersno { get; set; }
        public string _dateRequested { get; set; }
        public string _dateNeeded { get; set; }
        public string _loccode { get; set; }
        public string _locname { get; set; }
        public string _deptcode { get; set; }
        public string _deptname  { get; set; }
        public string _datestamp { get; set; }
        public string _status { get; set; }
        public string _uid { get; set; }
        public string _remarks { get; set; }
        public string _sersType { get; set; }
        public string _noOfCopies { get; set; }
        public string _requestType { get; set; }
        public string _approvedBy { get; set; }
        public List<cSERSLine> cSERSitemList = new List<cSERSLine>();

        public void getSERSHeader()
        {
            DBConnection dbCon = new DBConnection();
            cMySQLCommands oMySQLCommand = new cMySQLCommands();


            MySqlDataReader reader;
            oMySQLCommand._fields = "";
            oMySQLCommand._tblName = " sers_tran ";
            oMySQLCommand._condition = " sers_tran.rsh_no = '" + _sersno +"'";

            reader = oMySQLCommand.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _dateRequested = reader.GetString(3);
                    _dateNeeded = reader.GetString(4);
                    _loccode = reader.GetString(5);
                    _deptcode = reader.GetString(6);
                    _datestamp = reader.GetString(7);
                    _status = reader.GetString(8);
                    _uid = reader.GetString(9);
                    _remarks = reader.GetString(10);
                    _sersType = reader.GetString(22);
                    _noOfCopies = reader.GetString(25);
                    _approvedBy = reader.GetString(35);
                }
            }
        }

        public List<cSERSLine> getSERSLine()
        {
            DBConnection dbCon = new DBConnection();
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;
            
            oMySQLCommand.addFields("sers_line.rsd_date");
            oMySQLCommand.addFields("sers_line.rsd_itemcd");
            oMySQLCommand.addFields("item_mst.itm_desc");
            oMySQLCommand.addFields("sers_line.rsd_qty");
            oMySQLCommand.addFields("sers_line.rsd_untms");
            //oMySQLCommand.addFields("sers_line.rsd_detl");
            oMySQLCommand.addFields("item_mst.itm_specs");
            
             
            oMySQLCommand._tblName = "sers_line";
            oMySQLCommand._innerTableName = "item_mst";

            oMySQLCommand._innerLeftOn = "sers_line.rsd_itemcd";
            oMySQLCommand._innerRightOn = "item_mst.itm_code";

            oMySQLCommand._condition = "sers_line.rsd_no ='" + _sersno + "'";

            reader = oMySQLCommand.innerSelectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cSERSitemList.Add(new cSERSLine
                    {
                        _sersdate = reader.GetString(0),
                        _itemCode = reader.GetString(1),
                        _description = reader.GetString(2),
                        _quantity = reader.GetString(3),
                        _unitOfMeasure = reader.GetString(4),
                        _specifications = reader.GetString(5)
                    });
                }
            }
            return cSERSitemList;
        }

        public void getLocation()
        {
            DBConnection dbCon = new DBConnection();
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;
           
            oMySQLCommand._fields="location.loc_name";
            oMySQLCommand._tblName = "sers_tran";
            oMySQLCommand._innerTableName = "location";
            oMySQLCommand._innerLeftOn = "sers_tran.rsh_loccd";
            oMySQLCommand._innerRightOn = "location.loc_code";
            oMySQLCommand._condition = " location.loc_code = '" + _loccode.TrimEnd() + "'";
            reader = oMySQLCommand.innerSelectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _locname = reader.GetString(0);
                }
            }
        }

        public void getCompany()
        {
            DBConnection dbCon = new DBConnection();
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;

            oMySQLCommand.addFields("company.com_name");
            oMySQLCommand._tblName = "sers_tran";
            oMySQLCommand._innerTableName = "company";
            oMySQLCommand._innerLeftOn = "sers_tran.rsh_depcd";
            oMySQLCommand._innerRightOn = "company.com_code";
            oMySQLCommand._condition = " company.com_code = '" + _deptcode.TrimEnd() + "'";
            reader = oMySQLCommand.innerSelectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _deptname = reader.GetString(0);
                }
            }
        }

        public void getSERSHeader(string selSERSNo)
        {
            DBConnection dbCon = new DBConnection();
            cLocation oLocation = new cLocation();
            cCompany oCompany = new cCompany();
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;
            oMySQLCommand.addFields("sers_tran.rsh_dreq");
            oMySQLCommand.addFields("sers_tran.rsh_dneed");
            oMySQLCommand.addFields("sers_tran.rsh_loccd");
            oMySQLCommand.addFields("sers_tran.rsh_depcd");
            oMySQLCommand.addFields("sers_tran.rsh_dstamp");
            oMySQLCommand.addFields("sers_tran.rsh_stat");
            oMySQLCommand.addFields("usertab.usr_name");
            oMySQLCommand.addFields("sers_tran.rsh_rem");
            oMySQLCommand.addFields("sers_tran.rsh_type");
            oMySQLCommand.addFields("sers_tran.rsh_apprby");


            oMySQLCommand._tblName = "sers_tran";
            oMySQLCommand._innerTableName = "usertab";
            oMySQLCommand._innerLeftOn = "sers_tran.rsh_uid";
            oMySQLCommand._innerRightOn = "usertab.usr_login";
            oMySQLCommand._condition = "sers_tran.rsh_no = '" + selSERSNo + "'";

            reader = oMySQLCommand.innerSelectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _dateRequested = reader.GetString(0);
                    _dateNeeded = reader.GetString(1);
                    _loccode = reader.GetString(2);
                    _deptcode = reader.GetString(3);
                    _datestamp = reader.GetString(4);
                    _status = reader.GetString(5);
                    _uid = reader.GetString(6);
                    _remarks = reader.GetString(7);
                    _sersType = reader.GetString(8);
                    //_noOfCopies = reader.GetString(25);
                    _approvedBy = reader.GetString(9);
                }
            }

            ////getLocation(); 
            //getCompany();
        }
    }
    public class cSERSLine : cSERS
    {
        public string _sersdate;
        public string _itemCode;
        public string _description;
        public string _quantity;
        public string _unitCost;
        public string _unitOfMeasure;
        public string _specifications;

    }


}
