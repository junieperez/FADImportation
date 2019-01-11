using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FAD_Importation.CLASSES
{
    using MySql.Data.MySqlClient;

    public class cMySQLCommands
    {
        public string _tblName { get; set; }
        public string _fields { get; set; }
        public string _values { get; set; }
        public int _columnSize { get; set; }
        public string _condition { get; set; }
        public string _orderby { get; set; }
        public string _idName { get; set; }
        public string _innerTableName { get; set; }
        public string _innerLeftOn { get; set; }
        public string _innerRightOn { get; set; }
        public string _status { get; set; }

        DBConnection dbcon = new DBConnection();

        //public cMySQLCommands()
        //{
        //    _tblName = "";
        //}


        public void addFields(string str)
        {
            if (_fields == null)
            {
                _fields = str;
            }
            else
            {
                _fields = _fields + "," + str;
            }
        }

        public void addTable(string str)
        {
            if (_tblName == null)
            {
                _tblName = str;
            }
            else
            {
                _tblName = _tblName + "," + str;
            }
        }

        public void addValues(string str)
        {
            if (_values == "")
            {
                _values = "'" + str + "'";
            }
            else
            {
                _values = _values + ",'" + str + "'";
            }
        }

        public void insertQuery()
        {
            string insertSQL = "";
            insertSQL = "INSERT INTO " + _tblName + " (" + _fields + ") VALUES(" + _values + ")";
            dbcon.commandNonQuery(insertSQL);
        }

        public void updateQueries()
        {
            string updateSQL = "";
            string[] editCol = _fields.Split(',');
            string[] editVal = _values.Split(',');

            updateSQL = "UPDATE " + _tblName + " SET ";

            for (int x = 0; x < editCol.Count(); x++)
            {
                if (updateSQL != "UPDATE " + _tblName + " SET ")//used to get first col & value
                {
                    updateSQL = updateSQL + " , ";
                }
                updateSQL = updateSQL + editCol.ElementAt(x) + " = " + editVal.ElementAt(x);
            }

            updateSQL = updateSQL +" WHERE "+ _condition;
            dbcon.commandNonQuery(updateSQL);
        }

        public void insertDBFQuery(List<setDBFValues> setColumnValues)
        {
            cOleDBCommand oleDbCommand = new cOleDBCommand();
            string insertSQL = "";
            int countColumns = 1; 
            oleDbCommand.setConStr();
            oleDbCommand.setOleDbfValues(ref setColumnValues);

            insertSQL = "INSERT INTO " + _tblName + " (" + _fields + ") VALUES(";  //+ _values + ")";
            foreach (setDBFValues oDbfValues in setColumnValues)
            {
                if (countColumns > 1)
                {
                    insertSQL = insertSQL + " , ";
                }
                insertSQL = insertSQL + oDbfValues.valueName;
                countColumns++;
            }

            insertSQL = insertSQL.Replace('\r',' ').Replace('\n',' ') + ")";

            //MessageBox.Show(insertSQL);

            try {
                oleDbCommand.oleDBQueryNonReturn(insertSQL);
            }
            catch (Exception ex) {
               // MessageBox.Show(insertSQL);
                MessageBox.Show(ex.ToString());
            }
        }

        public void updateDBFQuery(List<setDBFValues> setColumnValues)
        {
            cOleDBCommand oleDbCommand = new cOleDBCommand();
            string updateSQL = "";
            string[] editCol = _fields.Split(',');
            string[] editVal = _values.Split(',');

            updateSQL = "UPDATE " + _tblName + " SET ";

            oleDbCommand.setConStr();
            oleDbCommand.setOleDbfValues(ref setColumnValues);

            for (int x = 0; x < editCol.Count(); x++)
            {
                if (updateSQL != "UPDATE " + _tblName + " SET ")//used to get first col & value
                {
                    updateSQL = updateSQL + " , ";
                }
                updateSQL = updateSQL + editCol.ElementAt(x) + " = " + editVal.ElementAt(x);
            }

            updateSQL = updateSQL + " WHERE " + _condition;

            oleDbCommand.oleDBQueryNonReturn(updateSQL);
        }

        public MySqlDataReader selectQuery()
        {
            string selectSQL = "";
            if (_fields == null)
            {
                _fields = "*";
            }
            MySqlDataReader selectReturn;
            selectSQL = "SELECT " + _fields + " FROM " + _tblName;
            selectReturn = dbcon.commandQuery(selectSQL);
            return selectReturn;
        }

        public MySqlDataReader selectQueryOrderBy()
        {
            string selectSQL = "";
            if (_fields == null)
            {
                _fields = "*";
            }
            MySqlDataReader selectReturn;
            selectSQL = "SELECT " + _fields + " FROM " + _tblName + " ORDER BY " + _orderby ;
            selectReturn = dbcon.commandQuery(selectSQL);
            return selectReturn;
        }

        public MySqlDataReader innerSelectQuery()
        {
            string selectSQL = "";
            if (_fields == null)
            {
                _fields = "*";
            }
            MySqlDataReader selectReturn;
            selectSQL = "SELECT " + _fields + " FROM " + _tblName + " INNER JOIN " + _innerTableName + " ON " + _innerLeftOn + " = " + _innerRightOn;
            selectReturn = dbcon.commandQuery(selectSQL);
            return selectReturn;
        }

        public MySqlDataReader innerSelectQueryWhere()
        {
            string selectSQL = "";
            if (_fields == null)
            {
                _fields = "*";
            }
            MySqlDataReader selectReturn;
            selectSQL = "SELECT " + _fields + " FROM " + _tblName + " INNER JOIN " + _innerTableName + " ON " + _innerLeftOn + " = " + _innerRightOn + " WHERE " + _condition;
            selectReturn = dbcon.commandQuery(selectSQL);
            return selectReturn;
        }

        public MySqlDataReader selectQueryWhere()
        {
            string selectSQL = "";
            MySqlDataReader selectReturn;
            if (_fields == ""){ _fields = "*"; }
            selectSQL = "SELECT " + _fields + " FROM " + _tblName + " WHERE "+ _condition + "";
            selectReturn = dbcon.commandQuery(selectSQL);
            return selectReturn;
        }

        public int getSequenceNo()
        {
            int currentSequenceNo = 0;
            string selectSQL = "";
            MySqlDataReader reader;
            selectSQL = "SELECT MAX(" + _idName + ") FROM " + _tblName;
            reader = dbcon.commandQuery(selectSQL);
            while (reader.Read())
            {
                if (reader.IsDBNull(0))
                {
                    currentSequenceNo = 0;
                }
                else
                {
                    currentSequenceNo = reader.GetInt32(0);
                }
            }
            return currentSequenceNo;
        }

        public int getMaxCasted()
        {
            int currentSequenceNo = 0;
            string selectSQL = "";
            MySqlDataReader reader;
            selectSQL = "SELECT MAX(CAST(" + _idName + " AS UNSIGNED)) FROM " + _tblName;
            reader = dbcon.commandQuery(selectSQL);
            while (reader.Read())
            {
                if (reader.IsDBNull(0))
                {
                    currentSequenceNo = 0;
                }
                else
                {
                    currentSequenceNo = reader.GetInt32(0);
                }
            }
            return currentSequenceNo;
        }

        public void updateQuery()
        {
            string selectSQL = "";
            selectSQL = "UPDATE " + _tblName + " SET  " + _fields + "  =  " + _values + " WHERE  " + _condition;
            dbcon.commandNonQuery(selectSQL);
        }

        public string getDBFSequenceNo(string seq_code)
        {
            string selectSQL = "";
            string retValue = "";
            MySqlDataReader reader;
            selectSQL = "SELECT seq_num FROM sequence WHERE seq_code='" + seq_code + "'";
            reader = dbcon.commandQuery(selectSQL);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    retValue = reader.GetString(0);
                }
            }
            return retValue;
        }

        public void setDBFSequenceNo(string seq_code, string seq_newnum)
        { 
            string incrementSQL = "";
            //int newSequencenum = Convert.ToInt16(seq_newnum) + 1;
            int newSequencenum = Convert.ToInt16(seq_newnum); //edited for updating sequence.dbf file skipping 1 number
            incrementSQL = "UPDATE sequence SET seq_num= " + newSequencenum.ToString() + " WHERE seq_code='" + seq_code + "'";

            cOleDBCommand oleDbCommand = new cOleDBCommand();
            oleDbCommand.setConStr();
            oleDbCommand.oleDBQueryNonReturn(incrementSQL);
        }
         
        public void getMySQLDataSet(ref DataSet setData, string fillQuery,string fillTable)
        {
            DBConnection db = new DBConnection(); 
            DataSet ds = setData;
            string setTable = fillTable,setQuery = fillQuery;
            MySqlConnection mysqlCon = new MySqlConnection(db.Connection.ConnectionString);
            MySqlCommand mysqlCom = new MySqlCommand(setQuery, mysqlCon);

            MySqlDataAdapter mySQLAdapter = new MySqlDataAdapter(mysqlCom);
            mySQLAdapter.Fill(ds.Tables[fillTable]);
            //return ds;
        }

        public void closeDBFAll()
        {
            cOleDBCommand oleDbCommand = new cOleDBCommand();
            oleDbCommand.setConStr();
            oleDbCommand.oleDBQueryNonReturn("CLOSE DATABASES ALL");
        }

        public void deleteQuery()
        {
            string selectSQL = "";
            selectSQL = "DELETE FROM " + _tblName + " WHERE " + _condition;
            dbcon.commandNonQuery(selectSQL);
        }

        public void emptyQuery()
        {
            string selectSQL = "";
            selectSQL = "TRUNCATE " + _tblName;
            dbcon.commandNonQuery(selectSQL);
        }

        public string countQueryWhere()
        {
            string selectSQL = "SELECT COUNT(*) FROM " + _tblName + " WHERE " + _condition;
            string queryCount = "0";
            MySqlDataReader reader;
            reader = dbcon.commandQuery(selectSQL);
                while (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        queryCount = reader.GetString(0);
                    }
                    else
                    {
                        queryCount = reader.GetString(0);
                    }
                }
            

            return queryCount;
        }
        public string countQuery()
        {
            string selectSQL = "SELECT COUNT(*) FROM " + _tblName;
            string queryCount = "0";
            MySqlDataReader reader;
            reader = dbcon.commandQuery(selectSQL);
            while (reader.Read())
            {
                if (reader.IsDBNull(0))
                {
                    queryCount = reader.GetString(0);
                }
                else
                {
                    queryCount = reader.GetString(0);
                }
            }
            return queryCount;
        }
    }
}
