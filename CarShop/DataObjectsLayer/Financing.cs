using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjectsLayer
{
    public class Financing
    {
        /*
         FinancingID	int	
        SaleID	int	
        LoanProvider	nvarchar	50
        LoanAmount	float	
        InterestRate	float	
         */
        public int FinancingID {  get; set; }
        public int SaleID { get; set; }
        public string LoanProvider { get; set; }
        public float LoanAmount { get; set; }
        public float InterestRate { get; set; }
    }
    public class FinancingVM : Financing
    {
        public Sales Sale { get; set; }
    }
}
