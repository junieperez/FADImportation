using System;

namespace FAD_Importation.CLASSES
{
    //class set currency values
    public class cCurrencyValue
    {
        //currency values properties
        public string _currIsoCode { get; set; }
        public string _currName { get; set; }
        public double _currExchange { get; set; }
        public string _selected { get; set; }
        
        //convert the values based on the currency exchance
        //used in viewing the item with currency value conversion
        public double convertToExchange(string curValue)
        {
            string newConValue = (Convert.ToDouble(curValue) * _currExchange).ToString("N5");
            return Convert.ToDouble(newConValue);
        }
    }
}
