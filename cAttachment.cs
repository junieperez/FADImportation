using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace FAD_Importation.CLASSES
{
    //a class that handles all attachments to be included in the system
    public class cAttachment
    {
        //class attachment properties
        public string _Code { get; set; }
        public string _attachmentName { get; set; }
        public string _transno { get; set; }
        public string _transtype { get; set; }
        public string _supcd { get; set; }
        public string _loccd { get; set; }
        public string selLocCode = Config.Default.SELLOCCODE;

        public cAttachment()
        {

        }
        
        //used to save  the class attachments to table
        //transno as the transaction number when the attachment was included
        //transtype is the transaction type where the attachment is included e.g. "Quotation", "E-Requisition" etc.
        //List<cAttachment> a list made to receive multiple attachments in saving
        public void savetotable(string transno, string transtype, List<cAttachment> cAttachList)
        {

            cMySQLCommands mySqlCom = new cMySQLCommands();
            mySqlCom._tblName = "attachments";
            mySqlCom.addFields("transno");
            mySqlCom.addFields("transtype");
            mySqlCom.addFields("sup_code");
            mySqlCom.addFields("filename");
            mySqlCom.addFields("loc_code");

            foreach (cAttachment cAttach in cAttachList)
            {
                mySqlCom._values = "";
                mySqlCom.addValues(transno);
                mySqlCom.addValues(transtype);
                mySqlCom.addValues(cAttach._supcd);
                mySqlCom.addValues(Path.GetFileName(cAttach._attachmentName));
                mySqlCom.addValues(cAttach._loccd);
                copyToCNVPICT(cAttach._attachmentName);
                mySqlCom.insertQuery();
            }
        }

        //same as savetotable function but the only difference is the query
        //removes all existing attachments related to the transaction no and transaction type
        //and adds all attachments included
        public void edittotable(string transno, string transtype, List<cAttachment> cAttachList)
        {
            //remove all transaction entries first
            cMySQLCommands mySqlCom = new cMySQLCommands();
            cMySQLCommands mySqlDel = new cMySQLCommands();
            mySqlDel._tblName = "attachments";
            mySqlDel._condition = " transno = '" + transno + "' and transtype = '" + transtype + "' and loc_code = '"+ selLocCode +"'";
            mySqlDel.deleteQuery();

            mySqlCom._tblName = "attachments";
            mySqlCom.addFields("transno");
            mySqlCom.addFields("transtype");
            mySqlCom.addFields("sup_code");
            mySqlCom.addFields("filename");
            mySqlCom.addFields("loc_code");

            //adds attachment values per entry in list
            foreach (cAttachment cAttach in cAttachList)
            {
                mySqlCom._values = "";
                mySqlCom.addValues(transno);
                mySqlCom.addValues(transtype);
                mySqlCom.addValues(cAttach._supcd);
                mySqlCom.addValues(Path.GetFileName(cAttach._attachmentName));
                mySqlCom.addValues(cAttach._loccd);
                copyToCNVPICT(cAttach._attachmentName);
                mySqlCom.insertQuery();
            }
        }

        //used to return attachments included from the supplier
        //in the quotation transaction. Query is based on
        //transaction number and supplier
        public List<cAttachment> loadSupQuoteAttachment(string transno, string supcd)
        {
            List<cAttachment> cAttachGetList = new List<cAttachment>();
            cMySQLCommands mySqlCom = new cMySQLCommands();
            MySqlDataReader reader;
            mySqlCom._tblName = "attachments"; mySqlCom._tblName = "attachments";
            _transtype = "Quotation";
            _transno = transno;
            _supcd = supcd;

            mySqlCom.addFields("transno");
            mySqlCom.addFields("sup_code");
            mySqlCom.addFields("filename");
            mySqlCom._condition = "transno = '" + transno + "' and sup_code ='" + supcd + "' and loc_code = '" + _loccd + "'";

            reader = mySqlCom.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cAttachGetList.Add(new cAttachment
                    {
                        _Code = reader.GetString(0),
                        _supcd = reader.GetString(1),
                        _attachmentName = reader.GetString(2),
                    });
                }
            }
            return cAttachGetList;
        }



        //used to return attachments included from the supplier
        //in the quotation transaction. Query is based on
        //transaction number and supplier
        public List<cAttachment> loadSupQuoteAttachment(string transno)
        {
            List<cAttachment> cAttachGetList = new List<cAttachment>();
            cMySQLCommands mySqlCom = new cMySQLCommands();
            MySqlDataReader reader;
            _transtype = "Quotation";
            _transno = transno;

            mySqlCom._tblName = "attachments";
            mySqlCom.addFields("transno");
            mySqlCom.addFields("sup_code");
            mySqlCom.addFields("filename");
            mySqlCom._condition = "transno = '" + transno + "' and loc_code = '" + _loccd + "' and transtype = '"+ _transtype + "'";

            reader = mySqlCom.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cAttachGetList.Add(new cAttachment
                    {
                        _Code = reader.GetString(0),
                        _supcd = reader.GetString(1),
                        _attachmentName = reader.GetString(2),
                    });
                }
            }
            return cAttachGetList;
        }

        //used to return attachments included from the supplier
        //in the quotation transaction. Query is based on
        //transaction number and supplier
        public List<cAttachment> loadSupQuoteAttachment(string transno, string supcd, string loccd)
        {
            List<cAttachment> cAttachGetList = new List<cAttachment>();
            cMySQLCommands mySqlCom = new cMySQLCommands();
            MySqlDataReader reader;
            mySqlCom._tblName = "attachments";
            _transtype = "Quotation";
            _transno = transno;
            _supcd = supcd;
            _loccd = loccd;

            mySqlCom.addFields("transno");
            mySqlCom.addFields("sup_code");
            mySqlCom.addFields("filename");
            mySqlCom._condition = "transno = '" + transno + "' and sup_code ='" + supcd + "' and loc_code = '" + _loccd + "'";

            reader = mySqlCom.selectQueryWhere();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cAttachGetList.Add(new cAttachment
                    {
                        _Code = reader.GetString(0),
                        _supcd = reader.GetString(1),
                        _attachmentName = reader.GetString(2),
                    });
                }
            }
            return cAttachGetList;
        }

        //verify quotation attachments if included
        //returns true if statement is true else false
        public bool checkQuoteAttachments(string transno, string supcd)
        {
            cMySQLCommands mySqlCom = new cMySQLCommands();
            MySqlDataReader reader;
            mySqlCom._tblName = "attachments"; mySqlCom._tblName = "attachments";
            mySqlCom._fields = "";
            _transtype = "Quotation";
            _transno = transno;
            _supcd = supcd;

            mySqlCom._condition = "transno = '" + _transno + "' and sup_code ='" + _supcd + "' and transtype = '" + _transtype + "' and loc_code = '" + _loccd + "'";

            reader = mySqlCom.selectQueryWhere();
            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //verify quotation attachments if included
        //returns true if statement is true else false
        public bool checkQuoteAttachments(string transno, string supcd, string loccd)
        {
            cMySQLCommands mySqlCom = new cMySQLCommands();
            MySqlDataReader reader;
            mySqlCom._tblName = "attachments"; mySqlCom._tblName = "attachments";
            mySqlCom._fields = "";
            _transtype = "Quotation";
            _transno = transno;
            _supcd = supcd;
            _loccd = loccd;

            mySqlCom._condition = "transno = '" + _transno + "' and sup_code ='" + _supcd + "' and transtype = '" + _transtype + "' and loc_code = '" + _loccd + "'";

            reader = mySqlCom.selectQueryWhere();
            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //used to copy the file from file directory to the desired location/server
        public void copyToCNVPICT(string fileDir)
        {
            //initializes configs based on the default values in settings
            string dbUser = Config.Default.DBFUSERNAME;
            string dbDomain = Config.Default.DBFDOMAIN;
            string dbPass = Config.Default.DBFPASSWORD;

            using (new ImpersonateUser(dbUser, dbDomain, dbPass))
            {
                
                string fileToCopy = fileDir; //files current directory
                string destinationDirectory = Config.Default.CNVPICT; //server's document file folder 
                string findFile =  destinationDirectory + Path.GetFileName(fileToCopy); //verify if file exists

                //if file exists removes the current file and replaces it with 
                //the newly uploaded attachment
                if (File.Exists(@findFile) == false)
                {
                    //File.Delete(destinationDirectory + Path.GetFileName(fileToCopy));
                    //File.Copy(fileToCopy, destinationDirectory + Path.GetFileName(fileToCopy));
                    if (File.Exists(fileToCopy) == true)
                    {
                        File.Copy(fileToCopy, destinationDirectory + Path.GetFileName(fileToCopy));
                    }
                }
            }
        }       
    }
}

