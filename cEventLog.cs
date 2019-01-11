using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace FAD_Importation.CLASSES
{
    public class cEventLog
    {
        public string _logEventDateTime { get; set; }
        public string _locCode = Config.Default.SELLOCCODE;

        public string _logEventName { get; set; }
        public string _logEventCount { get; set; }
        public string _locName { get; set; }

        public string _entryno { get; set; }

        public cEventLog(string eCount, string eName)
        {
            _logEventCount = eCount;
            _logEventName = eName;
        }

        public cEventLog()
        {
        }

        public void addEvent()
        {
            cLocation oLocation = new cLocation();
            oLocation._code = _locCode;
            oLocation.setLocationDetails();
            _logEventDateTime = Convert.ToString(DateTime.Now);

            cMySQLCommands oMySQLCommands = new cMySQLCommands();
            oMySQLCommands._tblName = "eventlog";
            oMySQLCommands.addFields("eventtime");
            oMySQLCommands.addFields("eventname");
            oMySQLCommands._values = "";
            oMySQLCommands.addValues(_logEventDateTime);
            oMySQLCommands.addValues(oLocation._name.TrimEnd() + " has " + _logEventCount + " entries of " + _logEventName);
            oMySQLCommands.insertQuery();
        }

        public void addEventEntry()
        {
            cLocation oLocation = new cLocation();
            oLocation._code = _locCode;
            oLocation.setLocationDetails();

            cMySQLCommands oMySQLCommands = new cMySQLCommands();
            oMySQLCommands._tblName = "eventlog";
            oMySQLCommands.addFields("eventtime");
            oMySQLCommands.addFields("eventname");
            oMySQLCommands._values = "";
            oMySQLCommands.addValues(_logEventDateTime);
            oMySQLCommands.addValues(oLocation._name.TrimEnd() + " has " + _logEventCount + "entries of " + _logEventName);
            oMySQLCommands.insertQuery();
        }

        public void removePastEvents()
        {

        }

        public List<cEventLog> getAllEventLogs()
        {
            List<cEventLog> oEventsList = new List<cEventLog>();
            MySqlDataReader reader;
            cMySQLCommands oMySQLCommands = new cMySQLCommands();
            oMySQLCommands._tblName = "eventlog";
            oMySQLCommands._orderby = " docno DESC";
            reader = oMySQLCommands.selectQueryOrderBy();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    oEventsList.Add(new cEventLog
                    {
                        _logEventDateTime = reader.GetString(1),
                        _logEventName = reader.GetString(2)
                    });
                }
            }
            return oEventsList;
        }



    }
}