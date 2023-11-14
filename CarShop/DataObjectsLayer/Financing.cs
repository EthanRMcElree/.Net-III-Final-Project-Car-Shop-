using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class Financing
    {        
        public int FinancingID {  get; set; }
        public int SaleID { get; set; }
        public string LoanProvider { get; set; }
        public Double LoanAmount { get; set; }
        public Double InterestRate { get; set; }
    }
    public class FinancingVM : Financing
    {
        public Sales Sale { get; set; }
    }
}
