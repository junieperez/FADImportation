using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace FAD_Importation.CLASSES
{
    public class cPurchaseOrder
    {
        public string _pohno { get; set; }
        public string _pohdate { get; set; }
        public string _rrno { get; set; }
        public string _supplierCode { get; set; }
        public string _supplierName { get; set; }
        public string _discount1 { get; set; }
        public string _discount2 { get; set; }
        public string _discount3 { get; set; }
        public string _discount4 { get; set; }
        public string _discount5 { get; set; }
        public string _vat { get; set; }
        public string _freight { get; set; }
        public string _netamt { get; set; }
        public string _ship { get; set; }
        public string _prtflg { get; set; }
        public string _rrstat { get; set; }
        public string _potype { get; set; }
        public string _term { get; set; }
        public string _nprint { get; set; }
        public string _uid { get; set; }
        public string _ruid { get; set; }
        public string _datestamp { get; set; }
        public string _refno { get; set; }
        public string _remarks { get; set; }
        public string _locationcode { get; set; }
        public string _locationName { get; set; }
        public string _departmentcode { get; set; }
        public string _departmentName { get; set; }
        public string _ret { get; set; }
        public string _appr { get; set; }
        public string _rspo { get; set; }
        public string _sugd { get; set; }
        public string _dateRequested { get; set; }
        public string _daprv { get; set; }
        public string _dateneeded { get; set; }
        public string _sstatus { get; set; }
        public string _menuused { get; set; }
        public string _currency { get; set; }
        public string _checkedby { get; set; }
        public string _nav_text { get; set; }
        public string _poh_frbs { get; set; }
        public string _poh_othr { get; set; }
        public string _ex_flg { get; set; }
        public string _ex_poref { get; set; }
        public string _ex_rem { get; set; }
        public string _modeOfPayment { get; set; }
        public string _modeOfPaymentN { get; set; }

        public List<cPurchaseOrderItem> cPOItemsList = new List<cPurchaseOrderItem>();

        public void getPOHeader()
        {
            DBConnection dbCon = new DBConnection();
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;
            oMySQLCommand._fields = "";
            oMySQLCommand._tblName = "spo_tran";
            oMySQLCommand._condition = "spo_tran.poh_no = " + _pohno;

            reader = oMySQLCommand.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _pohdate = reader.GetString(2);
                    _rrno = reader.GetString(3);
                    _supplierCode = reader.GetString(4);
                    _discount1 = reader.GetString(5);
                    _discount2 = reader.GetString(6);
                    _discount3 = reader.GetString(7);
                    _discount4 = reader.GetString(8);
                    _discount5 = reader.GetString(9);
                    _vat = reader.GetString(10);
                    _freight = reader.GetString(11);
                    _netamt = reader.GetString(12);
                    _ship = reader.GetString(13);
                    _prtflg = reader.GetString(14);
                    _rrstat = reader.GetString(15);
                    _potype = reader.GetString(16);
                    _term = reader.GetString(17);
                    _nprint = reader.GetString(18);
                    _uid = reader.GetString(19);
                    _ruid = reader.GetString(20);
                    _datestamp = reader.GetString(21);
                    _refno = reader.GetString(22);
                    _remarks = reader.GetString(23);
                    _locationcode = reader.GetString(24);
                    _departmentcode = reader.GetString(25);
                    _ret = reader.GetString(26);
                    _appr = reader.GetString(27);
                    _rspo = reader.GetString(28);
                    _sugd = reader.GetString(29);
                    _dateRequested = reader.GetString(30);
                    _daprv = reader.GetString(31);
                    _dateneeded = reader.GetString(32);
                    _sstatus = reader.GetString(33);
                    _menuused = reader.GetString(34);
                    _currency = reader.GetString(35);
                    _checkedby=reader.GetString(36);
                    _nav_text=reader.GetString(37);
                    _poh_frbs=reader.GetString(38);
                    _poh_othr=reader.GetString(39);
                    _ex_flg=reader.GetString(40);
                    _ex_poref=reader.GetString(41);
                    _ex_rem=reader.GetString(42);
                    _modeOfPayment = reader.GetString(43);
                }
            }
            getLocation();
            getCompany();
            getSupplier();
            getModOfPayment();
        }

        public List<cPurchaseOrderItem> getPOLine()
        {
            DBConnection dbCon = new DBConnection();
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;

            oMySQLCommand.addFields("pod_date");
            oMySQLCommand.addFields("pod_itemcd");
            oMySQLCommand.addFields("item_mst.itm_desc");
            oMySQLCommand.addFields("pod_qty");
            oMySQLCommand.addFields("pod_untms");
            oMySQLCommand.addFields("pod_untcst");
            oMySQLCommand.addFields("pod_amt");
            oMySQLCommand.addFields("pod_itemno");
            oMySQLCommand.addFields("pod_dcount");
            oMySQLCommand.addFields("pod_netamt");
            oMySQLCommand.addFields("pod_detl");
            oMySQLCommand.addFields("pod_rrstat");
            oMySQLCommand.addFields("pod_balanc");

            oMySQLCommand._tblName = "spo_line";
            oMySQLCommand._innerTableName = "item_mst";

            oMySQLCommand._innerLeftOn = "spo_line.pod_itemcd";
            oMySQLCommand._innerRightOn = "item_mst.itm_code";

            oMySQLCommand._condition = "spo_line.pod_no = " + _pohno;

            reader = oMySQLCommand.innerSelectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cPOItemsList.Add(new cPurchaseOrderItem
                    {
                        _poddate = reader.GetString(0),
                        _itemCode = reader.GetString(1),
                        _description = reader.GetString(2),
                        _quantity = reader.GetString(3),
                        _uom = reader.GetString(4),
                        _unitcost = reader.GetString(5),
                        _amount = reader.GetString(6),
                        _itemno = reader.GetString(7),
                        _discount = reader.GetString(8),
                        _netamount = reader.GetString(9),
                        _detail = reader.GetString(10),
                        _rrstat = reader.GetString(11),
                        _balance = reader.GetString(12)
                    });
                }
            }

            return cPOItemsList;
        }

        public void getLocation()
        {
            DBConnection dbCon = new DBConnection();
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;

            oMySQLCommand._fields = "location.loc_name";
            oMySQLCommand._tblName = "spo_tran";
            oMySQLCommand._innerTableName = "location";
            oMySQLCommand._innerLeftOn = "spo_tran.	poh_loc";
            oMySQLCommand._innerRightOn = "location.loc_code";

            reader = oMySQLCommand.innerSelectQuery();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _locationName = reader.GetString(0);
                }
            }
        }

        public void getCompany()
        {
            DBConnection dbCon = new DBConnection();
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;

            oMySQLCommand._fields = "company.com_name";
            oMySQLCommand._tblName = "spo_tran";
            oMySQLCommand._innerTableName = "company";
            oMySQLCommand._innerLeftOn = "spo_tran.poh_depcd";
            oMySQLCommand._innerRightOn = "company.com_code";

            reader = oMySQLCommand.innerSelectQuery();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _departmentName = reader.GetString(0);
                }
            }
        }

        public void getSupplier()
        {
            DBConnection dbCon = new DBConnection();
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;


            oMySQLCommand._fields = "supplier.sup_name";
            oMySQLCommand._tblName = "spo_tran";
            oMySQLCommand._innerTableName = "supplier";
            oMySQLCommand._innerLeftOn = "spo_tran.poh_supcd";
            oMySQLCommand._innerRightOn = "supplier.sup_code";

            reader = oMySQLCommand.innerSelectQuery();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _supplierName = reader.GetString(0);
                }
            }
        }

        public void getModOfPayment()
        {
            cModOfPayment oModOfPayment = new cModOfPayment();
            oModOfPayment._pcode = _modeOfPayment;
            oModOfPayment.getPMasterDetails();
            _modeOfPaymentN = oModOfPayment._pname;
        }


    }

    public class cPurchaseOrderItem : cPurchaseOrder
    {
        public string _podno { get; set; }
        public string _poddate { get; set; }
        public string _itemCode { get; set; }
        public string _description { get; set; }
        public string _quantity { get; set; }
        public string _unitcost { get; set; }
        public string _uom { get; set; }
        public string _amount { get; set; }
        public string _itemno { get; set; }
        public string _discount { get; set; }
        public string _netamount { get; set; }
        public string _detail { get; set; }
        public string _itmRRStat { get; set; }
        public string _balance { get; set; }
    }

    public class cPOCurrency : cPurchaseOrder
    {
        public string _pocno { get; set; }
        public string _pocersno { get; set; }
        public string _pocdate { get; set; }
        public string _curCode { get; set; }
        public string _curname { get; set; }
        public string _curVal { get; set; }
        public string _pocremarks { get; set; }
        public string _pocPrepBy { get; set; }
        public string _pocConfirm { get; set; }
        public string _pocLocCode{ get; set; }

        public void savePOCurrency()
        {
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            cMySQLCom._tblName = "spo_currency";

            cMySQLCom.addFields("poc_no");
            cMySQLCom.addFields("ers_no");
            cMySQLCom.addFields("poc_date");
            cMySQLCom.addFields("cur_code");
            cMySQLCom.addFields("cur_name");
            cMySQLCom.addFields("cur_val");
            cMySQLCom.addFields("remarks");
            cMySQLCom.addFields("prepby");
            cMySQLCom.addFields("confirmed");
            cMySQLCom.addFields("loc_code");

            cMySQLCom._values = "";
            cMySQLCom.addValues(_pocno);
            cMySQLCom.addValues(_pocersno);
            cMySQLCom.addValues(_pocdate);
            cMySQLCom.addValues(_curCode);
            cMySQLCom.addValues(_curname);
            cMySQLCom.addValues(_curVal);
            cMySQLCom.addValues(_pocremarks);
            cMySQLCom.addValues(_pocPrepBy);
            cMySQLCom.addValues(_pocConfirm);
            cMySQLCom.addValues(_pocLocCode);

            cMySQLCom.insertQuery();
        }

        public void createSPOCurrency()
        {
            List<setDBFValues> dbfColumnList = new List<setDBFValues>();
            cMySQLCommands cInsertPOCurQuery = new cMySQLCommands();
            cInsertPOCurQuery._tblName = "spo_currency";
            cInsertPOCurQuery._values = "";

            cInsertPOCurQuery.addFields("poc_no");
            cInsertPOCurQuery.addFields("ers_no");
            cInsertPOCurQuery.addFields("poc_date");
            cInsertPOCurQuery.addFields("cur_code");
            cInsertPOCurQuery.addFields("cur_name");
            cInsertPOCurQuery.addFields("cur_val");
            cInsertPOCurQuery.addFields("remarks");
            cInsertPOCurQuery.addFields("prepby");
            cInsertPOCurQuery.addFields("confirmed");
            cInsertPOCurQuery.addFields("loc_code");

            cInsertPOCurQuery.addValues(_pocno);
            cInsertPOCurQuery.addValues(_pocersno);
            cInsertPOCurQuery.addValues(_pocdate);
            cInsertPOCurQuery.addValues(_curCode);
            cInsertPOCurQuery.addValues(_curname);
            cInsertPOCurQuery.addValues(_curVal);
            cInsertPOCurQuery.addValues(_pocremarks);
            cInsertPOCurQuery.addValues(_pocPrepBy);
            cInsertPOCurQuery.addValues(_pocConfirm);
            cInsertPOCurQuery.addValues(_pocLocCode);

            dbfColumnList.Add(new setDBFValues { valueName = _pocno, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _pocersno, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _pocdate, valueType = "DATE" });
            dbfColumnList.Add(new setDBFValues { valueName = _curCode, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _curname, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _curVal, valueType = "NUMERIC" });
            dbfColumnList.Add(new setDBFValues { valueName = _pocremarks, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _pocPrepBy, valueType = "STRING" });
            dbfColumnList.Add(new setDBFValues { valueName = _pocConfirm, valueType = "BOOL" });
            dbfColumnList.Add(new setDBFValues { valueName = _pocLocCode, valueType = "STRING" });

            cInsertPOCurQuery.insertDBFQuery(dbfColumnList);
        }

        public void getPOCurrency()
        {
            DBConnection dbCon = new DBConnection( );
            cMySQLCommands oMySQLCommand = new cMySQLCommands();
            MySqlDataReader reader;
            oMySQLCommand._fields = "";
            oMySQLCommand._tblName = "spo_currency";
            //oMySQLCommand._condition = " spo_currency.poc_no = " + _pocno;
            oMySQLCommand._condition = " spo_currency.ers_no = '" + _pocersno + "' and spo_currency.loc_code = '"+ _pocLocCode + "'";

            reader = oMySQLCommand.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _pocdate = reader.GetString(3);
                    _curCode = reader.GetString(4);
                    _curname = reader.GetString(5);
                    _curVal = reader.GetString(6);
                    _remarks = reader.GetString(7);
                    _pocPrepBy = reader.GetString(8);
                    _pocConfirm = reader.GetString(9);
                    _pocLocCode = reader.GetString(10);
                }
            }
        }   
    }
}
