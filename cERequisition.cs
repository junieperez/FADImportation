 using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
namespace FAD_Importation.CLASSES
{
    //A class that manipulates E-Requisition related transaction
    //this includes E-Requisition in dbf and MySQL
    //this also includes E-Requisition Tran and Line

    public class cERequisition
    {
        //E-Requisition Tran properties equivalent/based on dbf file
        public string _ersno { get; set; }
        public string _ersdate { get; set; }
        public string _rrno { get; set; }
        public string _supCd { get; set; }
        public string _supType { get; set; }
        public string _exclusive { get; set; }
        public string _active { get; set; }
        public string _discount1 { get; set; }
        public string _discount2 { get; set; }
        public string _discount3 { get; set; }
        public string _discount4 { get; set; }
        public string _discount5 { get; set; }
        public string _vat { get; set; }
        public string _freight { get; set; }
        public string _netamt { get; set; }
        public string _ship { get; set; }
        public string _printed { get; set; }
        public string _rrstat { get; set; }
        public string _type { get; set; }
        public string _term { get; set; }
        public string _printCount { get; set; }
        public string _uid { get; set; }
        public string _ruid { get; set; }
        public string _datetimestamp { get; set; }
        public string _refno { get; set; }
        public string _remarks { get; set; }
        public string _location { get; set; }
        public string _ret { get; set; }
        public string _department { get; set; }
        public string _approvedBy { get; set; }
        public string _rshstat { get; set; }
        public string _pono { get; set; }
        public string _rsno { get; set; }
        public string _eno { get; set; }
        public string _dateNeeded { get; set; }
        public string _canvassCtrlNo { get; set; }
        public string _frbs { get; set; }
        public string _others { get; set; }
        public string _dateStatUpdated { get; set; }
        public string _lsexflg { get; set; }
        public string _lsex_poref { get; set; }
        public string _departmentName { get; set; }
        public string _locationName { get; set; }
        public string _supplierName { get; set; }

        //properties for listed E-Requisition items and E-Requisiton Currencies
        //it is listed for multiple entries
        public List<cERequisitionLine> cErequisitionItems = new List<cERequisitionLine>();
        public List<cERequisitionCur> cErequisitionCurrencies = new List<cERequisitionCur>();

        //getERSHeader is a method used to update the properties of the class 
        //it retrieves the details included in E-Requisiton Tran MySqltable
        //data is based on the initialized ERS No
        public void getERSHeader()
        {
            // DBConnection dbCon = new DBConnection();
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;
            oMySQLCommand._fields = "";
            oMySQLCommand._tblName = "ers_tran";
            oMySQLCommand._condition = "ers_tran.rsh_no = " + _ersno;

            reader = oMySQLCommand.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _ersno = reader.GetString(1);
                    _ersdate = reader.GetString(2);
                    _rrno = reader.GetString(3);
                    _supCd = reader.GetString(4);
                    _supType = reader.GetString(5);
                    _exclusive = reader.GetString(6);
                    _active = reader.GetString(7);
                    _discount1 = reader.GetString(8);
                    _discount2 = reader.GetString(9);
                    _discount3 = reader.GetString(10);
                    _discount4 = reader.GetString(11);
                    _discount5 = reader.GetString(12);
                    _vat = reader.GetString(13);
                    _freight = reader.GetString(14);
                    _netamt = reader.GetString(15);
                    _ship = reader.GetString(16);
                    _printed = reader.GetString(17);
                    _rrstat = reader.GetString(18);
                    _type = reader.GetString(19);
                    _term = reader.GetString(20);
                    _printCount = reader.GetString(21);
                    _uid = reader.GetString(22);
                    _ruid = reader.GetString(23);
                    _datetimestamp = reader.GetString(24);
                    _refno = reader.GetString(25);
                    _remarks = reader.GetString(26);
                    _location = reader.GetString(27);
                    _ret = reader.GetString(28);
                    _department = reader.GetString(29);
                    _approvedBy = reader.GetString(30);
                    _rshstat = reader.GetString(31);
                    _pono = reader.GetString(32);
                    _rsno = reader.GetString(33);
                    _eno = reader.GetString(34);
                    _dateNeeded = reader.GetString(35);
                    _canvassCtrlNo = reader.GetString(36);
                    _frbs = reader.GetString(37);
                    _others = reader.GetString(38);
                    _dateStatUpdated = reader.GetString(39);

                    //_lsexflg = reader.GetString(42);
                    //_lsex_poref = reader.GetString(43);
                }
            }
            //Calls the methods for retrieving of Location, Company and Supplier Names
            getLocation();
            getCompany();
            getSupplier();
        }

        //getERSLine is a method used to update the properties of the class 
        //it retrieves the details included in E-Requisiton Line MySqltable
        //data is based on the initialized ERS No
        //entry items are listed in the Property List
        public List<cERequisitionLine> getERSLine()
        {
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;

            //oMySQLCommand.addFields("pod_date");
            oMySQLCommand.addFields("rsd_itemcd");
            oMySQLCommand.addFields("item_mst.itm_desc");
            oMySQLCommand.addFields("rsd_qty");
            oMySQLCommand.addFields("rsd_untms");
            oMySQLCommand.addFields("rsd_untcst");
            oMySQLCommand.addFields("rsd_amt");
            oMySQLCommand.addFields("rsd_itemno");
            oMySQLCommand.addFields("rsd_dcount");
            oMySQLCommand.addFields("rsd_netamt");
            oMySQLCommand.addFields("rsd_detl");
            oMySQLCommand.addFields("rsd_rrstat");
            oMySQLCommand.addFields("rsd_balanc");

            oMySQLCommand._tblName = "ers_line";
            oMySQLCommand._innerTableName = "item_mst";

            oMySQLCommand._innerLeftOn = "ers_line.rsd_itemcd";
            oMySQLCommand._innerRightOn = "item_mst.itm_code";

            oMySQLCommand._condition = "ers_line.rsd_no = " + _ersno;

            reader = oMySQLCommand.innerSelectQueryWhere();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cErequisitionItems.Add(new cERequisitionLine
                    {
                        //_poddate = reader.GetString(0),
                        _itemCode = reader.GetString(0),
                        _iDescription = reader.GetString(1),
                        _quantity = reader.GetString(2),
                        _UOM = reader.GetString(3),
                        _unitCost = reader.GetString(4),
                        _amt = reader.GetString(5),
                        _itemno = reader.GetString(6),
                        _discount = reader.GetString(7),
                        _itmnetamt = reader.GetString(8),
                        _details = reader.GetString(9),
                        _rrstat = reader.GetString(10),
                        _balance = reader.GetString(11)
                    });
                }
            }

            return cErequisitionItems;
        }

        //used to create an ers header entry based on the details retrieved from quotation
        //details from ers header will be inserted to .dbf file
        //location selected will be based on the configuration
        public void createERSHeader()
        {
            //initialize variables to be used
            _rshstat = "FOR APPROVAL";
            _datetimestamp = GetTimestamp(DateTime.Now);
            List<setDBFValues> dbfColumnList = new List<setDBFValues>();
            cMySQLCommands cInsertERSQuery = new cMySQLCommands();
            cInsertERSQuery._tblName = "ers_tran";
            cInsertERSQuery._values = "";

            //number based on .dbf sequence add 1 to update value
            _ersno = (Convert.ToDouble(cInsertERSQuery.getDBFSequenceNo("ERS")) + 1).ToString(); 
            _pono = (Convert.ToDouble(cInsertERSQuery.getDBFSequenceNo("SPO")) + 1).ToString();

            //fields are based on the table column names (.dbf)
            cInsertERSQuery.addFields("rsh_no");
            cInsertERSQuery.addFields("rsh_date");
            cInsertERSQuery.addFields("rsh_rrno");
            cInsertERSQuery.addFields("rsh_supcd");
            cInsertERSQuery.addFields("rsh_suptyp");
            cInsertERSQuery.addFields("rsh_excl");
            cInsertERSQuery.addFields("rsh_activ");
            cInsertERSQuery.addFields("rsh_dis1");
            cInsertERSQuery.addFields("rsh_dis2");
            cInsertERSQuery.addFields("rsh_dis3");
            cInsertERSQuery.addFields("rsh_dis4");
            cInsertERSQuery.addFields("rsh_dis5");
            cInsertERSQuery.addFields("rsh_vat");
            cInsertERSQuery.addFields("rsh_freig");
            cInsertERSQuery.addFields("rsh_netamt");
            cInsertERSQuery.addFields("rsh_ship");
            cInsertERSQuery.addFields("rsh_prtflg");
            cInsertERSQuery.addFields("rsh_rrstat");
            cInsertERSQuery.addFields("rsh_type");
            cInsertERSQuery.addFields("rsh_term");
            cInsertERSQuery.addFields("rsh_nprint");
            cInsertERSQuery.addFields("rsh_uid");
            cInsertERSQuery.addFields("rsh_ruid");
            cInsertERSQuery.addFields("rsh_stamp");
            cInsertERSQuery.addFields("rsh_refno");
            cInsertERSQuery.addFields("rsh_rem");
            cInsertERSQuery.addFields("rsh_loc");
            cInsertERSQuery.addFields("rsh_ret");
            cInsertERSQuery.addFields("rsh_depcd");
            cInsertERSQuery.addFields("rsh_appr");
            cInsertERSQuery.addFields("rsh_stat");
            cInsertERSQuery.addFields("rsh_spo");
            cInsertERSQuery.addFields("rsh_rsno");
            cInsertERSQuery.addFields("rsh_eno");
            cInsertERSQuery.addFields("dateneeded");
            cInsertERSQuery.addFields("cnv_ctrlno");
            cInsertERSQuery.addFields("frbs");
            cInsertERSQuery.addFields("others");
            cInsertERSQuery.addFields("dstat_upd");
            cInsertERSQuery.addFields("isex_flg");
            cInsertERSQuery.addFields("isex_poref");

            //add values based on properties
            cInsertERSQuery.addValues(_ersno);
            cInsertERSQuery.addValues(_ersdate);
            cInsertERSQuery.addValues(_rrno);
            cInsertERSQuery.addValues(_supCd);
            cInsertERSQuery.addValues(_supType);
            cInsertERSQuery.addValues(_exclusive);
            cInsertERSQuery.addValues(_active);
            cInsertERSQuery.addValues(_discount1);
            cInsertERSQuery.addValues(_discount2);
            cInsertERSQuery.addValues(_discount3);
            cInsertERSQuery.addValues(_discount4);
            cInsertERSQuery.addValues(_discount5);
            cInsertERSQuery.addValues(_vat);
            cInsertERSQuery.addValues(_freight);
            cInsertERSQuery.addValues(_netamt);
            cInsertERSQuery.addValues(_ship);
            cInsertERSQuery.addValues(_printed);
            cInsertERSQuery.addValues(_rrstat);
            cInsertERSQuery.addValues(_type);
            cInsertERSQuery.addValues(_term);
            cInsertERSQuery.addValues(_printCount);
            cInsertERSQuery.addValues(_uid);
            cInsertERSQuery.addValues(_ruid);
            cInsertERSQuery.addValues(_datetimestamp);
            cInsertERSQuery.addValues(_refno);
            cInsertERSQuery.addValues(_remarks);
            cInsertERSQuery.addValues(_location);
            cInsertERSQuery.addValues(_ret);
            cInsertERSQuery.addValues(_department);
            cInsertERSQuery.addValues(_approvedBy);
            cInsertERSQuery.addValues(_rshstat);
            cInsertERSQuery.addValues(_pono);
            cInsertERSQuery.addValues(_rsno);
            cInsertERSQuery.addValues(_eno);
            cInsertERSQuery.addValues(_dateNeeded);
            cInsertERSQuery.addValues(_canvassCtrlNo);
            cInsertERSQuery.addValues(_frbs);
            cInsertERSQuery.addValues(_others);
            cInsertERSQuery.addValues(_dateStatUpdated);
            cInsertERSQuery.addValues(_lsexflg);
            cInsertERSQuery.addValues(_lsex_poref);

            // commented to uninsert ERS entry
            // cInsertERSQuery.insertQuery();

            //set datatypes for dbf Column
            dbfColumnList.Add(new setDBFValues { valueName = _ersno, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _ersdate, valueType = "DATE" });
            dbfColumnList.Add(new setDBFValues { valueName = _rrno, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _supCd, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _supType, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _exclusive, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _active, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _discount1, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _discount2, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _discount3, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _discount4, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _discount5, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _vat, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _freight, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _netamt, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _ship, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _printed, valueType = "BOOL" });
            dbfColumnList.Add(new setDBFValues { valueName = _rrstat, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _type, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _term, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _printCount, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _uid, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _ruid, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _datetimestamp, valueType = "DATETIME" });
            dbfColumnList.Add(new setDBFValues { valueName = _refno, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _remarks, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _location, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _ret, valueType = "BOOL" });
            dbfColumnList.Add(new setDBFValues { valueName = _department, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _approvedBy, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _rshstat, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _pono, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _rsno, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _eno, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _dateNeeded, valueType = "DATE" });
            dbfColumnList.Add(new setDBFValues { valueName = _canvassCtrlNo, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _frbs, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _others, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _dateStatUpdated, valueType = "DATETIME" });
            dbfColumnList.Add(new setDBFValues { valueName = _lsexflg, valueType = "BOOL" });
            dbfColumnList.Add(new setDBFValues { valueName = _lsex_poref, valueType = "NUMERIC" });

            //insert to .dbf file after setting column types
            cInsertERSQuery.insertDBFQuery(dbfColumnList);
        }

        //used to create an ers detail entry based on retrieved from quotation
        //entries from ers detail will be inserted to .dbf file
        //location selected will be based on the configuration
        public void createERSDetails(List<cERequisitionLine> ersAllItems)
        {
            //each item will be added from ersAllitems List
            foreach (cERequisitionLine ersLineItem in ersAllItems)
            {
                //initialize variables
                List<setDBFValues> dbfColumnList = new List<setDBFValues>();
                cMySQLCommands cInsertERSQuery = new cMySQLCommands();
                cInsertERSQuery._tblName = "ers_line";

                //add column fields based on .dbf file
                cInsertERSQuery.addFields("rsd_no");
                cInsertERSQuery.addFields("rsd_date");
                cInsertERSQuery.addFields("rsd_itemcd");
                cInsertERSQuery.addFields("rsd_qty");
                cInsertERSQuery.addFields("rsd_untcst");
                cInsertERSQuery.addFields("rsd_untms");
                cInsertERSQuery.addFields("rsd_amt");
                cInsertERSQuery.addFields("rsd_itemno");
                cInsertERSQuery.addFields("rsd_dcount");
                cInsertERSQuery.addFields("rsd_dpitem");
                cInsertERSQuery.addFields("rsd_netamt");
                cInsertERSQuery.addFields("rsd_detl");
                cInsertERSQuery.addFields("rsd_rrstat");
                cInsertERSQuery.addFields("rsd_balanc");
                cInsertERSQuery.addFields("rsd_start");
                cInsertERSQuery.addFields("rsd_valid");

                //add values based on ers Line property values
                cInsertERSQuery.addValues(_ersno);
                cInsertERSQuery.addValues(ersLineItem._rsddate);
                cInsertERSQuery.addValues(ersLineItem._itemCode);
                cInsertERSQuery.addValues(ersLineItem._quantity);
                cInsertERSQuery.addValues(ersLineItem._unitCost);
                cInsertERSQuery.addValues(ersLineItem._UOM);
                cInsertERSQuery.addValues(ersLineItem._amt);
                cInsertERSQuery.addValues(ersLineItem._itemno);
                cInsertERSQuery.addValues(ersLineItem._discount);
                cInsertERSQuery.addValues(ersLineItem._dpitem);
                cInsertERSQuery.addValues(ersLineItem._itmnetamt);
                cInsertERSQuery.addValues(ersLineItem._details);
                cInsertERSQuery.addValues(ersLineItem._itmrrstat);
                cInsertERSQuery.addValues(ersLineItem._balance);
                cInsertERSQuery.addValues(ersLineItem._start);
                cInsertERSQuery.addValues(ersLineItem._valid);

                //set datatypes for dbf Column
                dbfColumnList.Add(new setDBFValues { valueName = _ersno, valueType = "NUMERIC" });
                dbfColumnList.Add(new setDBFValues { valueName = ersLineItem._rsddate, valueType = "DATE" });
                dbfColumnList.Add(new setDBFValues { valueName = ersLineItem._itemCode, valueType = "STRING" });
                dbfColumnList.Add(new setDBFValues { valueName = ersLineItem._quantity, valueType = "NUMERIC" });
                dbfColumnList.Add(new setDBFValues { valueName = ersLineItem._unitCost, valueType = "NUMERIC" });
                dbfColumnList.Add(new setDBFValues { valueName = ersLineItem._UOM, valueType = "STRING" });
                dbfColumnList.Add(new setDBFValues { valueName = ersLineItem._amt, valueType = "NUMERIC" });
                dbfColumnList.Add(new setDBFValues { valueName = ersLineItem._itemno, valueType = "NUMERIC" });
                dbfColumnList.Add(new setDBFValues { valueName = ersLineItem._discount, valueType = "NUMERIC" });
                dbfColumnList.Add(new setDBFValues { valueName = ersLineItem._dpitem, valueType = "NUMERIC" });
                dbfColumnList.Add(new setDBFValues { valueName = ersLineItem._itmnetamt, valueType = "NUMERIC" });
                dbfColumnList.Add(new setDBFValues { valueName = ersLineItem._details, valueType = "STRING" });
                dbfColumnList.Add(new setDBFValues { valueName = ersLineItem._itmrrstat, valueType = "STRING" });
                dbfColumnList.Add(new setDBFValues { valueName = ersLineItem._balance, valueType = "NUMERIC" });
                dbfColumnList.Add(new setDBFValues { valueName = ersLineItem._start, valueType = "DATE" });
                dbfColumnList.Add(new setDBFValues { valueName = ersLineItem._valid, valueType = "DATE" });

                //insert to .dbf file in ers_line
                cInsertERSQuery.insertDBFQuery(dbfColumnList);
                //cInsertERSQuery.closeDBFAll();
            }

        }

        //a module that provides location name based on location code
        public void getLocation()
        {
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;

            oMySQLCommand._fields = "location.loc_name";
            oMySQLCommand._tblName = "location";
            oMySQLCommand._condition = "location.loc_code = '" + _location + "'";
            reader = oMySQLCommand.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _locationName = reader.GetString(0);
                }
            }
        }

        //a module that provides company name based on company code
        public void getCompany()
        {
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;

            oMySQLCommand._fields = "company.com_name";
            oMySQLCommand._tblName = "company";
            oMySQLCommand._condition = "company.com_code = '" + _department + "'";

            reader = oMySQLCommand.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _departmentName = reader.GetString(0);
                }
            }
        }

        //a module that provides supplier name based on supplier code
        public void getSupplier()
        {
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;


            oMySQLCommand._fields = "supplier.sup_name";
            oMySQLCommand._tblName = "supplier";
            oMySQLCommand._condition= " supplier.sup_code = " + _supCd;

            reader = oMySQLCommand.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _supplierName = reader.GetString(0);
                }
            }
        }

        //Converts .dbf timestamp to the desired format
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("MM/dd/yy hh:MM:ss tt");
        }

        //Used to increment ERS from sequence .dbf table
        public void incrementERS()
        {
            cMySQLCommands oMySQLSetSequence = new cMySQLCommands();
            oMySQLSetSequence.setDBFSequenceNo("ERS", _ersno);

            oMySQLSetSequence._tblName = "sequence";
            oMySQLSetSequence._fields = "sequence.seq_num";
            oMySQLSetSequence._values = _ersno;
            oMySQLSetSequence._condition = ("sequence.seq_code = 'ERS'");
            oMySQLSetSequence.updateQuery();

        }

        //Used to increment PO from sequence .dbf table
        public void incrementPO()
        {
            cMySQLCommands oMySQLSetSequence = new cMySQLCommands();
            oMySQLSetSequence.setDBFSequenceNo("SPO", _pono);

            oMySQLSetSequence._tblName = "sequence";
            oMySQLSetSequence._fields = "sequence.seq_num";
            oMySQLSetSequence._values = _pono;
            oMySQLSetSequence._condition = ("sequence.seq_code = 'SPO'");
            oMySQLSetSequence.updateQuery();
        }

        //Used to add entry in ERS Currency table .dbf file in system
        public void createERSCurrency(List<cERequisitionCur> eRequisitionCurList)
        {
            //for each value in listed currencies
            foreach (cERequisitionCur oERequisitionCur in eRequisitionCurList)
            {
                //intialized variables 
                List<setDBFValues> dbfColumnList = new List<setDBFValues>();
                cMySQLCommands cInsertERSQuery = new cMySQLCommands();
                cInsertERSQuery._tblName = "ers_currency";
                cInsertERSQuery._values = "";

                //get ers number +1
                _eno = (Convert.ToDouble(cInsertERSQuery.getDBFSequenceNo("ERS")) + 1).ToString();

                //currency columns 
                //selected value is only 1 per entry list
                cInsertERSQuery.addFields("ers_no");
                cInsertERSQuery.addFields("cur_code");
                cInsertERSQuery.addFields("cur_name");
                cInsertERSQuery.addFields("cur_val");
                cInsertERSQuery.addFields("selected");

                //ers currency entry per list will be added based on value
                cInsertERSQuery.addValues(_eno);
                cInsertERSQuery.addValues(oERequisitionCur._rscCode);
                cInsertERSQuery.addValues(oERequisitionCur._rscName);
                cInsertERSQuery.addValues(oERequisitionCur._rscValue);
                cInsertERSQuery.addValues(oERequisitionCur._rscSelected);

                //commented of uninsert ers_line
                // cInsertERSQuery.insertQuery();

                //set datatypes for dbf Column
                dbfColumnList.Add(new setDBFValues { valueName = _ersno, valueType = "NUMERIC" });
                dbfColumnList.Add(new setDBFValues { valueName = oERequisitionCur._rscCode, valueType = "STRING" });
                dbfColumnList.Add(new setDBFValues { valueName = oERequisitionCur._rscName, valueType = "STRING" });
                dbfColumnList.Add(new setDBFValues { valueName = oERequisitionCur._rscValue, valueType = "NUMERIC" });
                dbfColumnList.Add(new setDBFValues { valueName = oERequisitionCur._rscSelected, valueType = "BOOL" });

                cInsertERSQuery.insertDBFQuery(dbfColumnList);
                //  cInsertERSQuery.closeDBFAll();}
            }
        }
    }

    //sub-class of E-Requisition
    //inherited from E-requisition class
    //serves as ers items class and corrensponds 
    //properties with columns 
    public class cERequisitionLine : cERequisition
    {
        public string _rsdno, _rsddate, _itemCode, _iDescription, _quantity;
        public string _unitCost, _UOM, _amt, _itemno, _discount;
        public string _dpitem, _itmnetamt, _details, _itmrrstat;
        public string _balance, _start, _valid;
    }


    //sub-class of E-Requisition
    //inherited from E-requisition class
    //serves as ers currency class and
    //corresponds properties with columns
    public class cERequisitionCur : cERequisition
    {
        public string _rscNo, _rscCode, _rscName, _rscValue, _rscSelected;

        //method used in retrieving ers currency
        public void getErsCurrency()
        {
            // DBConnection dbCon = new DBConnection();
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;
            
            oMySQLCommand.addFields("quote_currency.cur_isoCode");
            oMySQLCommand.addFields("quote_currency.cur_value");
            oMySQLCommand._tblName = "quote_currency";
            oMySQLCommand._innerTableName = "ers_tran";
            oMySQLCommand._innerLeftOn = "quote_currency.quote_no";
            oMySQLCommand._innerRightOn = "ers_tran.cnv_ctrlno";
            oMySQLCommand._condition = " quote_currency.quote_no = '"+ _canvassCtrlNo + "'";

            reader = oMySQLCommand.innerSelectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _rscCode = reader.GetString(0);
                    _rscValue = reader.GetString(1);
                }
            }
        }
    }

    //sub-class of E-Requisition
    //inherited from E-requisition class
    //serves as ers tagged class and
    //corresponds properties with columns
    public class cErequisitonTagged : cERequisition
    {
        public string _rstNo, _rstStat, _rstLocCd;

        //set ers columns based on object's values
        public void setStatus()
        {
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            //cMySQLCommands oDeleteOldEntry = new cMySQLCommands();

            //oDeleteOldEntry._values = "";
            cMySQLCom._values = "";

            //oDeleteOldEntry._tblName = "ers_tagged";
            cMySQLCom._tblName = "ers_tagged";

            //oDeleteOldEntry._condition = "ers_tagged.rst_no = '"+ _rstNo +
                                         //"' and ers_tagged.tag_status = '' and ers_tagged.rst_loccd='"+ _rstLocCd + "'";

            cMySQLCom.addFields("tag_status");
            cMySQLCom.addFields("rst_no");
            cMySQLCom.addFields("rst_locCd");

            cMySQLCom.addValues(_rstStat);
            cMySQLCom.addValues(_rstNo);
            cMySQLCom.addValues(_rstLocCd);
            
            //oDeleteOldEntry.deleteQuery();
            cMySQLCom.insertQuery();
        }

        //updates ers tagging
        public void setUntagged()
        {
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            cMySQLCom._values = "";

            cMySQLCom._tblName = "ers_tagged";
            cMySQLCom.addFields("tag_status");
            cMySQLCom.addValues(_rstStat);
            cMySQLCom._condition = "rst_no = '" + _rstNo+ "' and rst_loccd = '"+ _rstLocCd  + "'";

            cMySQLCom.updateQuery();
        }
    }
}
