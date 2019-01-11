using System;

namespace FAD_Importation.CLASSES
{
    //a class used to custom calculate certain values in transaction
    class cCustomCalculate
    {
        //common properties in class calculate used
        public string _Quantity { get; set; }
        public string _UnitCost { get; set; }
        public string _Vat { get; set; }
        public string _Freight { get; set; }
        public string _Discount { get; set; }
        public string _NetAmount { get; set; }
        
        //get total method calculation
        public string getTotal()
        {
            #pragma warning disable IDE0018 // Inline variable declaration
            double result;
            #pragma warning restore IDE0018 // Inline variable declaration
                if (Double.TryParse(_Quantity, out result) && Double.TryParse(_UnitCost, out result) &&
                    Double.TryParse(_Vat, out result) && Double.TryParse(_Freight, out result) &&
                    Double.TryParse(_Discount, out result))
                {
                    double formatnet;
                    formatnet = ((Convert.ToDouble(_Quantity)*Convert.ToDouble(_UnitCost)) + 
                                 (Convert.ToDouble(_Vat) +    Convert.ToDouble(_Freight))) - Convert.ToDouble(_Discount);
                    string newDisplayFormat = formatnet.ToString("N5");
                    return newDisplayFormat;
                }
                else
                {
                    return "0.00000";
                }
        }

        //used to format values to 5 decimal points
        public void formatdata()
        {
            double formatQty,formatCost,formatVat,formatFreight,formatDiscount;
            formatQty = Convert.ToDouble(_Quantity);
            _Quantity = formatQty.ToString("N5");

            if (_UnitCost == "") 
            {
                _UnitCost = "0.00";
            }
            formatCost = Convert.ToDouble(_UnitCost);
            _UnitCost = formatCost.ToString("N5");

            if (_Vat == "")
            {
                _Vat = "0.00";
            }
            formatVat = Convert.ToDouble(_Vat);
            _Vat = formatVat.ToString("N5");

            if (_Freight == "")
            {
                _Freight = "0.00";
            }
            formatFreight = Convert.ToDouble(_Freight);
            _Freight = formatFreight.ToString("N5");

            if (_Discount == "")
            {
                _Discount = "0.00";
            }
            formatDiscount = Convert.ToDouble(_Discount);
            _Discount = formatDiscount.ToString("N5");
        }
        
        //convert certain percentage to its values represented
        public double percentToValue(string percValue, string percTotal)
        {
            if (percValue == "") { percValue = "0.00"; }
            if (percTotal == "") { percTotal = "0.00000"; }
            double retVal = ((Convert.ToDouble(percValue) / 100) * Convert.ToDouble(percTotal));
            if (percTotal == "0.00000" || percValue == "0.00")
            {
                retVal = 0.00;
            }
            return retVal;
        }
        
        //convert certain values to its percentage represented
        public double valueToPercent(string amtValue, string percTotal)
        {
            if (amtValue == "") { amtValue = "0.00"; }
            if (percTotal == "") { percTotal = "0.00000"; }
            double retVal = (Convert.ToDouble(amtValue) / Convert.ToDouble(percTotal)) * 100 ;
            if (amtValue == "0.00" || percTotal == "0.00000")
            {
                retVal = 0.00;
            }
            return retVal;
        }
    }
}
