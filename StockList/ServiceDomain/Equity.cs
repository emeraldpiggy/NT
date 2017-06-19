using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDomain
{
    public class Equity
    {
        private decimal _price;

        public string Symbol { get; set; }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
    }


    public enum InvestmentType
    {
        Equity = 0,
        Bonds = 1,
        Fixed = 2
    }
}
