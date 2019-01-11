using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FAD_Importation.CLASSES;
using MySql.Data.MySqlClient;

namespace FAD_Importation.CLASSES
{
    public class cQuotePayment : cQuotation
    {
        public string _quotepayno { get;set;}
        public string _supCode { get; set; }
        public string _supName { get; set; }
        public string _netAmt { get; set; }
        public string _formOfPayment { get; set; }
        public string _dollarAccount { get; set; }
        public string _paymentAmount { get; set; }

        public void loadQuoteSupplier()
        {
            MySqlDataReader reader;
            cMySQLCommands oMySqlCommands = new cMySQLCommands();
            oMySqlCommands.addFields("quote_supplier.sup_code");
            oMySqlCommands.addFields("supplier.sup_name");
            oMySqlCommands.addFields("quote_supplier.netamt");
            oMySqlCommands._tblName = "quote_supplier";
            oMySqlCommands._innerTableName = "supplier";
            oMySqlCommands._innerLeftOn = "supplier.sup_code";
            oMySqlCommands._innerRightOn = "quote_supplier.sup_code";
            oMySqlCommands._condition = "quote_supplier.quote_no = '" + _quoteno + "' and selected = 1";
            reader = oMySqlCommands.innerSelectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    _supCode = reader.GetString(0);
                    _supName = reader.GetString(1);
                    _netAmt = reader.GetString(2);
                }
            }
        }

        public void saveQuotePaymentEntry()
        {
            cMySQLCommands oMySqlCommands = new cMySQLCommands();
            oMySqlCommands._tblName = "quote_payment";
            oMySqlCommands.addFields("quote_no");
            oMySqlCommands.addFields("sup_code");
            oMySqlCommands.addFields("netamt");
            oMySqlCommands.addFields("qp_frmPayment");
            oMySqlCommands.addFields("qp_dolAccount");
            oMySqlCommands.addFields("qp_payAmount");


            oMySqlCommands.addValues(_quoteno);
            oMySqlCommands.addValues(_supCode);
            oMySqlCommands.addValues(_netAmt);
            oMySqlCommands.addValues(_formOfPayment);
            oMySqlCommands.addValues(_dollarAccount);
            oMySqlCommands.addValues(_paymentAmount);

            oMySqlCommands.insertQuery();

        }
    }

}
