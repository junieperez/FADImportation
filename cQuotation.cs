using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace FAD_Importation.CLASSES
{
    public class cQuotation
    {
        public List<cQuoteSuppliers> cQuoteSupplierList = new List<cQuoteSuppliers>();
        public List<cCurrencyValue> cCurrencyList = new List<cCurrencyValue>();

        public string _quoteno;
        public string _sersno;
        public string _quoteDate;
        public string _remarks;
        public string _prepby;
        public string _locCode;
        public string _comCode;
        public string _status="Draft";

        public string dbUser = Config.Default.DBFUSERNAME; //ClassConfig.Instance.Configs[Config.Default.DBFUSERNAME];
        public string dbDomain = Config.Default.DBFDOMAIN; //ClassConfig.Instance.Configs[Config.Default.DBFDOMAIN];
        public string dbPass = Config.Default.DBFPASSWORD;

       

        public void saveHeader()
        {
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            cMySQLCom._tblName = "quote_tran";
            cMySQLCom.addFields("quote_no");
            cMySQLCom.addFields("sersno");
            cMySQLCom.addFields("quotedate");
            cMySQLCom.addFields("remarks");
            cMySQLCom.addFields("prepby");
            cMySQLCom.addFields("quote_stat");
            cMySQLCom.addFields("quote_loccd");

            cMySQLCom._values = "";
            cMySQLCom.addValues(_quoteno);
            cMySQLCom.addValues(_sersno);
            cMySQLCom.addValues(_quoteDate);
            cMySQLCom.addValues(_remarks);
            cMySQLCom.addValues(_prepby);
            cMySQLCom.addValues(_status);
            cMySQLCom.addValues(_locCode);
            cMySQLCom.insertQuery();
        }
        public void editHeader()
        {
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            cMySQLCom._tblName = "quote_tran";
            cMySQLCom.addFields("sersno");
            cMySQLCom.addFields("quotedate");
            cMySQLCom.addFields("remarks");
            cMySQLCom.addFields("prepby");
            cMySQLCom.addFields("quote_stat");

            cMySQLCom._values = "";
            cMySQLCom.addValues(_sersno);
            cMySQLCom.addValues(_quoteDate);
            cMySQLCom.addValues(_remarks);
            cMySQLCom.addValues(_prepby);
            cMySQLCom.addValues(_status);
            cMySQLCom._condition = "quote_tran.quote_no = '" + _quoteno + "'";
            cMySQLCom.updateQueries();
        }


        public void saveDetails(List<cSupplierPerCost> cSupplierCostList,  string selQuoteSupplier)
        {
            cMySQLCommands cMySQLComDel = new cMySQLCommands();
            cMySQLCommands cMySQLCom = new cMySQLCommands();

            cMySQLCom._tblName = "quote_line";
            cMySQLCom.addFields("quote_no");
            cMySQLCom.addFields("itm_code");
            cMySQLCom.addFields("itm_qty");
            cMySQLCom.addFields("itm_uom");
            cMySQLCom.addFields("itm_cost");
            cMySQLCom.addFields("itm_disc");
            cMySQLCom.addFields("itm_freight");
            cMySQLCom.addFields("itm_vat");
            cMySQLCom.addFields("itm_net");
            cMySQLCom.addFields("sup_code");
            cMySQLCom.addFields("quote_loccd");
            cMySQLCom.addFields("selected");

            if (cSupplierCostList.Count > 0)
            {

                foreach (cSupplierPerCost cSupPerCost in cSupplierCostList)
                {
                    cMySQLCom._values = "";
                    cMySQLCom.addValues(_quoteno);
                    cMySQLCom.addValues(cSupPerCost._itmCode);
                    cMySQLCom.addValues(Convert.ToString(cSupPerCost._quantity));
                    cMySQLCom.addValues(cSupPerCost._uom);
                    cMySQLCom.addValues(Convert.ToString(cSupPerCost._unitCost));
                    cMySQLCom.addValues(Convert.ToString(cSupPerCost._discount));
                    cMySQLCom.addValues(Convert.ToString(cSupPerCost._freight));
                    cMySQLCom.addValues(Convert.ToString(cSupPerCost._vat));
                    cMySQLCom.addValues(Convert.ToString(cSupPerCost._netTotal).Replace(",", ""));
                    cMySQLCom.addValues(Convert.ToString(cSupPerCost._supplierCode));
                    cMySQLCom.addValues(_locCode);
                    if (cSupPerCost._supplierCode == selQuoteSupplier)
                    {
                        cMySQLCom.addValues("True");
                    }
                    else
                    {
                        cMySQLCom.addValues("False");
                    }
                    cMySQLCom.insertQuery();
                }
            }

        }
        public void editDetails(List<cSupplierPerCost> cSupplierCostList,  string selQuoteSupplier)
        { 
        

            cMySQLCommands cMySQLComDel = new cMySQLCommands();
            cMySQLCommands cMySQLCom = new cMySQLCommands();

            cMySQLCom._tblName = "quote_line";
            cMySQLCom.addFields("quote_no");
            cMySQLCom.addFields("itm_code");
            cMySQLCom.addFields("itm_qty");
            cMySQLCom.addFields("itm_uom");
            cMySQLCom.addFields("itm_cost");
            cMySQLCom.addFields("itm_disc");
            cMySQLCom.addFields("itm_freight");
            cMySQLCom.addFields("itm_vat");
            cMySQLCom.addFields("itm_net");
            cMySQLCom.addFields("sup_code");
            cMySQLCom.addFields("quote_loccd");
            cMySQLCom.addFields("selected");

            if (cSupplierCostList.Count > 0)
            {
                foreach (cSupplierPerCost cSupPerCost in cSupplierCostList)
                {
                    cMySQLComDel._fields = "";
                    cMySQLComDel._tblName = "quote_line";
                    cMySQLComDel._condition = "quote_no = '" + _quoteno + "' and quote_loccd = '" + _locCode 
                                            + "' and sup_code = '"+ cSupPerCost._supplierCode +"' and itm_code ='"+ cSupPerCost._itmCode + "'";
                    cMySQLComDel.deleteQuery();

                    cMySQLCom._values = "";
                    cMySQLCom.addValues(_quoteno);
                    cMySQLCom.addValues(cSupPerCost._itmCode);
                    cMySQLCom.addValues(Convert.ToString(cSupPerCost._quantity));
                    cMySQLCom.addValues(cSupPerCost._uom);
                    cMySQLCom.addValues(Convert.ToString(cSupPerCost._unitCost));
                    cMySQLCom.addValues(Convert.ToString(cSupPerCost._discount));
                    cMySQLCom.addValues(Convert.ToString(cSupPerCost._freight));
                    cMySQLCom.addValues(Convert.ToString(cSupPerCost._vat));
                    cMySQLCom.addValues(Convert.ToString(cSupPerCost._netTotal).Replace(",", ""));
                    cMySQLCom.addValues(Convert.ToString(cSupPerCost._supplierCode));
                    cMySQLCom.addValues(_locCode);
                    if (cSupPerCost._supplierCode == selQuoteSupplier)
                    {
                        cMySQLCom.addValues("True");
                    }
                    else
                    {
                        cMySQLCom.addValues("False");
                    }
                    cMySQLCom.insertQuery();
                }
            }

        }

        public void saveCurrencies(List<cCurrencyValue> cCurrencyValueList, string selCurCode,bool editMode)
        {
            string selectedCurrCode = selCurCode;
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            cMySQLCommands cMySQLComDel = new cMySQLCommands();

            cMySQLCom._tblName = "quote_currency";
            cMySQLCom.addFields("quote_no");
            cMySQLCom.addFields("quote_loccd");
            cMySQLCom.addFields("cur_isoCode");
            cMySQLCom.addFields("cur_value");
            cMySQLCom.addFields("selected");

            if (editMode == true)
            {
                cMySQLComDel._fields = "";
                cMySQLComDel._tblName = "quote_currency";
                cMySQLComDel._condition = "quote_no = '" + _quoteno + "'";
                cMySQLComDel.deleteQuery();
            }

            if (cCurrencyValueList.Count > 0)
            {
                foreach (cCurrencyValue cCurrencyValue in cCurrencyValueList)
                {

                    cMySQLCom._values = "";
                    cMySQLCom.addValues(_quoteno);
                    cMySQLCom.addValues(_locCode);
                    cMySQLCom.addValues(cCurrencyValue._currIsoCode);
                    cMySQLCom.addValues(Convert.ToString(cCurrencyValue._currExchange));
                    if (cCurrencyValue._currIsoCode == selectedCurrCode)
                    {

                        cMySQLCom.addValues("True");
                    }
                    else
                    {
                        cMySQLCom.addValues("False");
                    }
                    cMySQLCom.insertQuery();
                }
            }
        }
        
        public void saveSuppliers(List<cQuoteSuppliers> saveSupplierList, string selectedSupCode,bool editMode)
        {
            cMySQLCommands cMySQLComDel = new cMySQLCommands();
            cMySQLCommands cMySQLCom = new cMySQLCommands();

            cMySQLCom._tblName = "quote_supplier";

            cMySQLCom.addFields("quote_no");
            cMySQLCom.addFields("quote_loccd");
            cMySQLCom.addFields("sup_code");

            cMySQLCom.addFields("discount1");
            cMySQLCom.addFields("d1percent");

            cMySQLCom.addFields("discount2");
            cMySQLCom.addFields("d2percent");

            cMySQLCom.addFields("discount3");
            cMySQLCom.addFields("d3percent");

            cMySQLCom.addFields("discount4");
            cMySQLCom.addFields("d4percent");

            cMySQLCom.addFields("special");
            cMySQLCom.addFields("sppercent");

            cMySQLCom.addFields("othercharges");
            cMySQLCom.addFields("othpercent");

            cMySQLCom.addFields("freight");
            cMySQLCom.addFields("freipercent");

            cMySQLCom.addFields("costanddel");
            cMySQLCom.addFields("cadpercent");

            cMySQLCom.addFields("vat");
            cMySQLCom.addFields("vatpercent");

            cMySQLCom.addFields("netamt");
            cMySQLCom.addFields("selected");




            foreach (cQuoteSuppliers supplier in saveSupplierList)
            {
                if (editMode == true)
                {
                    cMySQLComDel._fields = "";
                    cMySQLComDel._tblName = "quote_supplier";
                    cMySQLComDel._condition = " quote_no = '" + _quoteno + "' and quote_loccd = '" + _locCode + "' and sup_code = '"+ supplier._supCode +"'";
                    cMySQLComDel.deleteQuery();
                }

                cMySQLCom._values = "";

                cMySQLCom.addValues(_quoteno);
                cMySQLCom.addValues(_locCode);
                cMySQLCom.addValues(supplier._supCode);

                cMySQLCom.addValues(supplier._discount1);
                cMySQLCom.addValues(supplier._d1Percent);

                cMySQLCom.addValues(supplier._discount2);
                cMySQLCom.addValues(supplier._d2Percent);

                cMySQLCom.addValues(supplier._discount3);
                cMySQLCom.addValues(supplier._d3Percent);

                cMySQLCom.addValues(supplier._discount4);
                cMySQLCom.addValues(supplier._d4Percent);

                cMySQLCom.addValues(supplier._specialDiscount);
                cMySQLCom.addValues(supplier._spePercent);

                cMySQLCom.addValues(supplier._otherCharges);
                cMySQLCom.addValues(supplier._othPercent);

                cMySQLCom.addValues(supplier._freight);
                cMySQLCom.addValues(supplier._freiPercent);

                cMySQLCom.addValues(supplier._costAndDelivery);
                cMySQLCom.addValues(supplier._cadPercent);

                cMySQLCom.addValues(supplier._vat);
                cMySQLCom.addValues(supplier._vatPercent);

                cMySQLCom.addValues(supplier._netAmount);

                if (selectedSupCode == supplier._supCode)
                {
                    cMySQLCom.addValues("1");
                }
                else
                {
                    cMySQLCom.addValues("0");
                }
                cMySQLCom.insertQuery();
            }
        }
        public int getQuoteNo()
        {
            //changed for per location #
            int newSequenceNo = 0;
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            string curnum = "";
            //cMySQLCom._tblName = "quote_tran";
            //cMySQLCom._idName = "quote_no";
            curnum = cMySQLCom.getDBFSequenceNo("QTN");
            newSequenceNo = Convert.ToInt32(curnum);//Convert.ToInt32(cMySQLCom.getSequenceNo());
            //System.Diagnostics.Trace.WriteLine(newSequenceNo);
            return newSequenceNo;
        }

        public void incrementQTN()
        {
            cMySQLCommands oMySQLSetSequence = new cMySQLCommands();
            oMySQLSetSequence.setDBFSequenceNo("QTN", _quoteno);

            oMySQLSetSequence._tblName = "sequence";
            oMySQLSetSequence._fields = "sequence.seq_num";
            oMySQLSetSequence._values = _quoteno;
            oMySQLSetSequence._condition = ("sequence.seq_code = 'QTN'");
            oMySQLSetSequence.updateQuery();
        }

        public void setStatus()
        {
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            cMySQLCom._values = "";

            cMySQLCom._tblName = "quote_tran";

            cMySQLCom.addFields("quote_stat");
            cMySQLCom.addValues(_status);

            cMySQLCom._condition = " quote_no = '" + _quoteno + "' and quote_loccd = '"+ _locCode + "'";

            cMySQLCom.updateQuery();
        }


        public MySqlDataReader getQuoteLineERS()
        {
            MySqlDataReader reader;
            //DBConnection dbcon = new DBConnection();
            //edited to fix bugs unselected items included in ers
            cMySQLCommands cMySQLCom = new cMySQLCommands();


            cMySQLCom.addFields("quote_line.itm_code");
            cMySQLCom.addFields("quote_line.itm_qty");
            cMySQLCom.addFields("quote_line.itm_uom");
            cMySQLCom.addFields("quote_line.itm_cost");
            cMySQLCom.addFields("(quote_line.itm_cost * quote_line.itm_qty) AS itm_amt");
            cMySQLCom.addFields("quote_line.itm_disc");
            cMySQLCom.addFields("quote_line.itm_net");
            cMySQLCom.addFields("item_mst.itm_specs");


            cMySQLCom._tblName = "quote_line";
            cMySQLCom._innerTableName = "item_mst";
            cMySQLCom._innerLeftOn = "quote_line.itm_code";
            cMySQLCom._innerRightOn = "item_mst.itm_code";

            //added quote _quote no due to varchar
            cMySQLCom._condition = " quote_line.quote_no = '" + _quoteno +"' and quote_line.selected = 'True' and " +
                "quote_line.quote_loccd = '" + _locCode + "'";

            reader = cMySQLCom.innerSelectQueryWhere();
            return reader; 
        }

        public MySqlDataReader getQuoteCurrency()
        {
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            MySqlDataReader reader;

            cMySQLCom.addFields("quote_currency.quote_no");
            cMySQLCom.addFields("quote_currency.cur_isoCode");
            cMySQLCom.addFields("currency.cur_name");
            cMySQLCom.addFields("quote_currency.cur_Value");
            cMySQLCom.addFields("quote_currency.selected");

            cMySQLCom._tblName = "quote_currency";
            cMySQLCom._innerTableName = "currency";
            cMySQLCom._innerLeftOn = "quote_currency.cur_isoCode";
            cMySQLCom._innerRightOn = "currency.cur_isoCode";

            cMySQLCom._condition = " quote_currency.quote_no = '" + _quoteno + "' and quote_currency.quote_loccd = '"+ _locCode + "'";

            reader = cMySQLCom.innerSelectQueryWhere();
            return reader;
        }


        public MySqlDataReader getSERSHeader()
        {
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            MySqlDataReader reader;

            cMySQLCom.addFields("sers_tran.rsh_loccd");
            cMySQLCom.addFields("sers_tran.rsh_depcd");
            cMySQLCom.addFields("sers_tran.rsh_dneed");

            cMySQLCom._tblName = "quote_tran";
            cMySQLCom._innerTableName = "sers_tran";

            cMySQLCom._innerLeftOn = "quote_tran.sersno";
            cMySQLCom._innerRightOn = "sers_tran.rsh_no";

            //added quote due to var char
            cMySQLCom._condition = "sers_tran.rsh_no = '" + _sersno + "'";

            reader = cMySQLCom.innerSelectQueryWhere();
            return reader;
        }

        public MySqlDataReader getQuoteSupplierERS()
        {
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            MySqlDataReader reader;

            cMySQLCom.addFields("quote_supplier.sup_code");
            cMySQLCom.addFields("supplier.sup_extype");
            cMySQLCom.addFields("supplier.sup_active");
            cMySQLCom.addFields("quote_supplier.discount1");
            cMySQLCom.addFields("quote_supplier.discount2");
            cMySQLCom.addFields("quote_supplier.discount3");
            cMySQLCom.addFields("quote_supplier.discount4");
            cMySQLCom.addFields("quote_supplier.special");
            cMySQLCom.addFields("quote_supplier.vat");
            cMySQLCom.addFields("quote_supplier.freight");
            cMySQLCom.addFields("quote_supplier.othercharges");
            cMySQLCom.addFields("quote_supplier.netamt");

            cMySQLCom._tblName = " quote_supplier ";
            cMySQLCom._innerTableName = " supplier ";
            cMySQLCom._innerLeftOn = " quote_supplier.sup_code ";
            cMySQLCom._innerRightOn = " supplier.sup_code ";


            cMySQLCom._condition = " quote_supplier.quote_no = '" + _quoteno + "' and quote_supplier.selected = True and quote_supplier.quote_loccd = '"+ _locCode + "'";

            reader = cMySQLCom.innerSelectQueryWhere();
            return reader;
        }

        public void createERequisition(string apprBy)
        {
            using (new ImpersonateUser(dbUser, dbDomain, dbPass))
            {
                MySqlDataReader readerQuoteItems, readerSupplier, readerLocDepartment, readerQuoteCurrency;
                cERequisition oeRequisition = new cERequisition();
                cSERS oSERSgetPrepBy = new cSERS();
                oSERSgetPrepBy._sersno = _sersno;
                oSERSgetPrepBy.getSERSHeader();

                //used to set TRAN info to ERS
                oeRequisition._type = "IMP";
                oeRequisition._canvassCtrlNo = _quoteno;
                oeRequisition._eno = _sersno;
                oeRequisition._ersdate = _quoteDate;
                oeRequisition._remarks = _remarks;
                oeRequisition._uid = oSERSgetPrepBy._uid; //_prepby; changed to sers prep by
                oeRequisition._approvedBy = " "; //used for blank approver

                //get Quotation Selected Supplier Details
                readerSupplier = getQuoteSupplierERS();

                //set selected supplier details
                if (readerSupplier.HasRows)
                {
                    while (readerSupplier.Read())
                    {
                        oeRequisition._supCd = readerSupplier.GetString(0);
                        oeRequisition._supType = readerSupplier.GetString(1);
                        oeRequisition._active = readerSupplier.GetString(2);
                        oeRequisition._discount1 = readerSupplier.GetString(3);
                        oeRequisition._discount2 = readerSupplier.GetString(4);
                        oeRequisition._discount3 = readerSupplier.GetString(5);
                        oeRequisition._discount4 = readerSupplier.GetString(6);
                        oeRequisition._discount5 = readerSupplier.GetString(7);
                        oeRequisition._vat = readerSupplier.GetString(8);
                        oeRequisition._freight = readerSupplier.GetString(9);
                        oeRequisition._ship = readerSupplier.GetString(10);
                        oeRequisition._netamt = readerSupplier.GetString(11).Replace(",", "");
                    }
                }

                //get location and department
                readerLocDepartment = getSERSHeader();
                if (readerLocDepartment.HasRows)
                {
                    while (readerLocDepartment.Read())
                    {
                        oeRequisition._location = readerLocDepartment.GetString(0);
                        oeRequisition._department = readerLocDepartment.GetString(1);
                        oeRequisition._dateNeeded = readerLocDepartment.GetString(2);
                    }
                }

                //get Quotation Items
                readerQuoteItems = getQuoteLineERS();

                //add items to ers_line
                if (readerQuoteItems.HasRows)
                {
                    while (readerQuoteItems.Read())
                    {
                        oeRequisition.cErequisitionItems.Add(new cERequisitionLine
                        {
                            _rsdno = oeRequisition._eno,
                            _rsddate = oeRequisition._ersdate,
                            _itemCode = readerQuoteItems.GetString(0),
                            _quantity = readerQuoteItems.GetString(1),
                            _UOM = readerQuoteItems.GetString(2),
                            _unitCost = readerQuoteItems.GetString(3),
                            _amt = readerQuoteItems.GetString(4),
                            _discount = readerQuoteItems.GetString(5),
                            _itmnetamt = readerQuoteItems.GetString(6),
                            _details = readerQuoteItems.GetString(7)
                        });
                        
                    }
                }

                //get Quotation Items
                readerQuoteCurrency = getQuoteCurrency();
                if (readerQuoteCurrency.HasRows)
                {
                    while (readerQuoteCurrency.Read())
                    {
                        oeRequisition.cErequisitionCurrencies.Add(new cERequisitionCur
                        {
                            _rscNo = readerQuoteCurrency.GetString(0),
                            _rscCode = readerQuoteCurrency.GetString(1),
                            _rscName = readerQuoteCurrency.GetString(2),
                            _rscValue = readerQuoteCurrency.GetString(3),
                            _rscSelected = readerQuoteCurrency.GetString(4),
                        });
                    }
                }

                oeRequisition.createERSHeader();
                oeRequisition.createERSDetails(oeRequisition.cErequisitionItems);
                oeRequisition.createERSCurrency(oeRequisition.cErequisitionCurrencies);
                oeRequisition.incrementERS();
                oeRequisition.incrementPO();
            }
        }

        public void getQuoteTran()
        {
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            MySqlDataReader reader;

            cMySQLCom._fields = "";
            cMySQLCom._tblName = "quote_tran";
            cMySQLCom._condition = " quote_tran.quote_no = '" + _quoteno + "' and quote_tran.quote_loccd = '"+ _locCode + "'";
            reader = cMySQLCom.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _sersno = reader.GetString(2);
                    _quoteDate = reader.GetString(3);
                    _remarks = reader.GetString(4);
                    _prepby = reader.GetString(5);
                    _status = reader.GetString(7);
                }
            }
        }

        public MySqlDataReader getQuoteLine()
        {
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            MySqlDataReader reader;

            cMySQLCom.addFields("quote_line.itm_code");
            cMySQLCom.addFields("quote_line.itm_qty");
            cMySQLCom.addFields("quote_line.itm_uom");
            cMySQLCom.addFields("quote_line.itm_cost");
            cMySQLCom.addFields("quote_line.itm_disc");
            cMySQLCom.addFields("quote_line.itm_freight");
            cMySQLCom.addFields("quote_line.itm_vat");
            cMySQLCom.addFields("quote_line.itm_net");

            cMySQLCom._tblName = "quote_line";
            cMySQLCom._innerTableName = "item_mst";

            cMySQLCom._innerLeftOn = "quote_line.itm_code";
            cMySQLCom._innerRightOn = "item_mst.itm_code";

            cMySQLCom._condition = " quote_line.quote_no = '" + _quoteno + "'";

            reader = cMySQLCom.innerSelectQueryWhere();
            return reader;
        }


        public List<cCurrencyValue> getCurrencyList()
        {
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            List<cCurrencyValue> oCurrencyList = new List<cCurrencyValue>();
            MySqlDataReader reader;

            cMySQLCom.addFields("quote_currency.cur_isoCode");
            cMySQLCom.addFields("currency.cur_name");
            cMySQLCom.addFields("quote_currency.cur_value");
            cMySQLCom.addFields("quote_currency.selected");

            cMySQLCom._tblName = "quote_currency";
            cMySQLCom._innerTableName = "currency";

            cMySQLCom._innerLeftOn = "quote_currency.cur_isoCode";
            cMySQLCom._innerRightOn = "currency.cur_isocode";

            reader = cMySQLCom.innerSelectQuery();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    oCurrencyList.Add(new cCurrencyValue
                    {
                        _currIsoCode = reader.GetString(0),
                        _currName = reader.GetString(1),
                        _currExchange = reader.GetDouble(2),
                        _selected = reader.GetString(3)
                    });
                }
            }
            return oCurrencyList;
        }
        public List<cCurrencyValue> getQuoteCurrencyList()
        {
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            List<cCurrencyValue> oCurrencyList = new List<cCurrencyValue>();
            MySqlDataReader reader;

            cMySQLCom.addFields("quote_currency.cur_isoCode");
            cMySQLCom.addFields("currency.cur_name");
            cMySQLCom.addFields("quote_currency.cur_value");
            cMySQLCom.addFields("quote_currency.selected");

            cMySQLCom._tblName = "quote_currency";
            cMySQLCom._innerTableName = "currency";

            cMySQLCom._innerLeftOn = "quote_currency.cur_isoCode";
            cMySQLCom._innerRightOn = "currency.cur_isocode";

            cMySQLCom._condition = "quote_currency.quote_no = '" + _quoteno + "' and quote_currency.quote_loccd= '"+ _locCode +"'" ;

            reader = cMySQLCom.innerSelectQueryWhere();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    oCurrencyList.Add(new cCurrencyValue
                    {
                        _currIsoCode = reader.GetString(0),
                        _currName = reader.GetString(1),
                        _currExchange = reader.GetDouble(2),
                        _selected = reader.GetString(3)
                    });
                }
            }
            return oCurrencyList;
        }

    
  }

    public class cSupplierPerCost : cQuotation
    {

        public List<cSupplierPerCost> cSupplierItemPerCostList = new List<cSupplierPerCost>();
        public string _supplierCode { get; set; }
        public string _itmCode { get; set; }
        public string _description { get; set; }
        public string _details { get; set; }
        public double _unitCost { get; set; }
        public double _discount { get; set; }
        public string _uom { get; set; }
        public double _vat { get; set; }
        public double _freight { get; set; }
        public double _quantity { get; set; }
        public double _netTotal { get; set; }
        public bool _itmselected { get; set; }

        public cSupplierPerCost()
        {
            _unitCost = 0.00000;
            _discount = 0.00000;
            _uom = "";
            _vat = 0.00000;
            _freight = 0.00000;
            _quantity = 0.00000;
            _itmselected = false;
        }



        public List<cSupplierPerCost> getitemSupplierCost()
        {
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            MySqlDataReader reader;

            cMySQLCom.addFields("quote_line.sup_code");
            cMySQLCom.addFields("quote_line.itm_code");
            cMySQLCom.addFields("item_mst.itm_desc");
            cMySQLCom.addFields("item_mst.itm_specs");
            cMySQLCom.addFields("quote_line.itm_qty");
            cMySQLCom.addFields("quote_line.itm_uom");
            cMySQLCom.addFields("quote_line.itm_cost");
            cMySQLCom.addFields("quote_line.itm_disc");
            cMySQLCom.addFields("quote_line.itm_freight");
            cMySQLCom.addFields("quote_line.itm_vat");
            cMySQLCom.addFields("quote_line.itm_net");

            cMySQLCom._tblName = "quote_line";
            cMySQLCom._innerTableName = "item_mst";

            cMySQLCom._innerLeftOn = "quote_line.itm_code";
            cMySQLCom._innerRightOn = "item_mst.itm_code";

            cMySQLCom._condition = " quote_line.quote_no = '" + _quoteno + "' and quote_line.quote_loccd = '" + _locCode +"'";

            reader = cMySQLCom.innerSelectQueryWhere();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cSupplierItemPerCostList.Add(new cSupplierPerCost
                    {
                        _supplierCode = reader.GetString(0),
                        _itmCode = reader.GetString(1),
                        _description = reader.GetString(2),
                        _details = reader.GetString(3),
                        _quantity = reader.GetDouble(4),
                        _uom = reader.GetString(5),
                        _unitCost = reader.GetDouble(6),
                        _discount = reader.GetDouble(7),
                        _freight = reader.GetDouble(8),
                        _vat = reader.GetDouble(9),
                        _netTotal = reader.GetDouble(10),
                    });
                }
            }
            return cSupplierItemPerCostList;
        }
    }

    public class cQuoteSuppliers : cQuotation
    {

        public cQuoteSuppliers()
        {
        }
        public string _quotesupno { get; set; }
        public string _supCode { get; set; }
        public string _supName { get; set; }
        public string _discount1 { get; set; }
        public string _d1Percent { get; set; }
        public string _discount2 { get; set; }
        public string _d2Percent { get; set; }
        public string _discount3 { get; set; }
        public string _d3Percent { get; set; }
        public string _discount4 { get; set; }
        public string _d4Percent { get; set; }
        public string _specialDiscount { get; set; }
        public string _spePercent { get; set; }
        public string _otherCharges { get; set; }
        public string _othPercent { get; set; }
        public string _vat { get; set; }
        public string _vatPercent { get; set; }
        public string _freight { get; set; }
        public string _freiPercent { get; set; }
        public string _costAndDelivery { get; set; }
        public string _cadPercent { get; set; }
        public string _totalDisc { get; set; }
        public string _totalAddOns { get; set; }
        public string _grosstotal { get; set; }
        public string _netAmount { get; set; }
        public string _selected { get; set; }
        public bool _setByValue { get; set; }

        public void getTotalDisc()
        {
            _totalDisc = (Convert.ToDouble(_discount1) + Convert.ToDouble(_discount2) +
                Convert.ToDouble(_discount3) + Convert.ToDouble(_discount4) + Convert.ToDouble(_specialDiscount)).ToString("N5");

        }

        public void getTotalAddOns()
        {
            _totalAddOns = (Convert.ToDouble(_otherCharges) + Convert.ToDouble(_vat) +
                Convert.ToDouble(_freight) + Convert.ToDouble(_costAndDelivery)).ToString("N5");
        }

        public string[] getListViewAdd()
        {
            string[] _listviewAdd = {_supCode,_supName,_discount1,_discount2,_discount3,_discount4,_specialDiscount,
                                      _otherCharges,_freight,_costAndDelivery,_vat,_netAmount, _d1Percent};
            return _listviewAdd;
        }


        public List<cQuoteSuppliers> getQuoteSupplierList()
        {
            cMySQLCommands cMySQLCom = new cMySQLCommands();
            MySqlDataReader reader;

            cMySQLCom.addFields("quote_supplier.sup_code");
            cMySQLCom.addFields("supplier.sup_name");
            cMySQLCom.addFields("quote_supplier.discount1");
            cMySQLCom.addFields("quote_supplier.d1percent");
            cMySQLCom.addFields("quote_supplier.discount2");
            cMySQLCom.addFields("quote_supplier.d2percent");
            cMySQLCom.addFields("quote_supplier.discount3");
            cMySQLCom.addFields("quote_supplier.d3percent");
            cMySQLCom.addFields("quote_supplier.discount4");
            cMySQLCom.addFields("quote_supplier.d4percent");
            cMySQLCom.addFields("quote_supplier.special");
            cMySQLCom.addFields("quote_supplier.sppercent");
            cMySQLCom.addFields("quote_supplier.othercharges");
            cMySQLCom.addFields("quote_supplier.othpercent");
            cMySQLCom.addFields("quote_supplier.freight");
            cMySQLCom.addFields("quote_supplier.freipercent");
            cMySQLCom.addFields("quote_supplier.costanddel");
            cMySQLCom.addFields("quote_supplier.cadpercent");
            cMySQLCom.addFields("quote_supplier.vat");
            cMySQLCom.addFields("quote_supplier.vatpercent");
            cMySQLCom.addFields("quote_supplier.netamt");
            cMySQLCom.addFields("quote_supplier.selected");

            cMySQLCom._tblName = "quote_supplier";
            cMySQLCom._innerTableName = "supplier";

            cMySQLCom._innerLeftOn = "quote_supplier.sup_code";
            cMySQLCom._innerRightOn = "supplier.sup_code";

            cMySQLCom._condition = " quote_supplier.quote_no = '" + _quotesupno + "' and quote_supplier.quote_loccd = '"+ _locCode +"'";

            reader = cMySQLCom.innerSelectQueryWhere();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cQuoteSupplierList.Add(new cQuoteSuppliers
                    {
                        _supCode = reader.GetString(0),
                        _supName = reader.GetString(1),
                        _discount1 = reader.GetString(2),
                        _d1Percent = reader.GetString(3),
                        _discount2 = reader.GetString(4),
                        _d2Percent=reader.GetString(5),
                        _discount3 = reader.GetString(6),
                        _d3Percent = reader.GetString(7),
                        _discount4 = reader.GetString(8),
                        _d4Percent=reader.GetString(9),
                        _specialDiscount = reader.GetString(10),
                        _spePercent=reader.GetString(11),
                        _otherCharges = reader.GetString(12),
                        _othPercent=reader.GetString(13),
                        _freight = reader.GetString(14),
                        _freiPercent=reader.GetString(15),
                        _costAndDelivery = reader.GetString(16),
                        _cadPercent=reader.GetString(17),
                        _vat = reader.GetString(18),
                        _vatPercent=reader.GetString(19),
                        _netAmount = reader.GetString(20),
                        _selected = reader.GetString(21)
                    });
                    getTotalDisc();
                    getTotalAddOns();
                }
            }
            return cQuoteSupplierList;
        }

  
    }
}

