using System;
using System.Collections.Generic;

namespace FAD_Importation.CLASSES
{
    //class used to convert exchange in currencies
    public class cExchange
    {
        public string _itemCode { get; set; }
        public string _description { get; set; }
        public string _quantity { get; set; }
        public string _uom { get; set; }
        public string _unitCost { get; set; }
        public string _discount { get; set; }
        public string _freight { get; set; }
        public string _vat { get; set; }
        public string _nettotal { get; set; }
        public string _exchange { get; set; }
        
        public cExchange()
        { }

        //class used properties with 5 decimal points
        public void convertToExchange()
        {
            _unitCost = (Convert.ToDouble(_unitCost) * Convert.ToDouble(_exchange)).ToString("N5");
            _discount = (Convert.ToDouble(_discount) * Convert.ToDouble(_exchange)).ToString("N5");
            _freight = (Convert.ToDouble(_freight) * Convert.ToDouble(_exchange)).ToString("N5");
            _vat = (Convert.ToDouble(_vat) * Convert.ToDouble(_exchange)).ToString("N5");
            _nettotal = (Convert.ToDouble(_nettotal) * Convert.ToDouble(_exchange)).ToString("N5");
        }

        //get converted discount to 5 decimal points
        public double getTotalDiscount(List<cExchange> cExcList)
        {
            double discTotal = 0.00000;
            foreach (cExchange cEx in cExcList)
            {
                discTotal = discTotal + Convert.ToDouble(cEx._discount);
            }
            return discTotal;
        }

        //get total of all add ons added : freight, vat etc.
        public double getTotalAddOns(List<cExchange> cExcList)
        {
            double addOnsTotal = 0.00000;
            foreach (cExchange cEx in cExcList)
            {
                addOnsTotal = addOnsTotal + (Convert.ToDouble(cEx._freight) + Convert.ToDouble(cEx._vat));
            }
            return addOnsTotal;
        }

        //get net total = total + add-ons - discount
        public double getNetTotal(List<cExchange> cExcList, string totalAddOn, string totalDiscount)
        {
            double netTotal = 0.00000;
            foreach (cExchange cEx in cExcList)
            {
                netTotal = netTotal + (Convert.ToDouble(_quantity )* Convert.ToDouble(_unitCost));
            }
            netTotal = (netTotal + Convert.ToDouble(totalAddOn)) -  Convert.ToDouble(totalDiscount);
            return netTotal;
        }

    }
}
