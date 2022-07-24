using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication4
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string NameProduct { get; set; }
        public string ModelProduct { get; set; }
        public string SerialNumberProduct { get; set; }
        public string TechCharProduct { get; set; }
        public int PriceProduct { get; set; }
        public string GuaranteePeriod { get; set; }
        public string GuaranteeOrganization { get; set; }
        public string WorkGroup { get; set; }
        public string Corpus { get; set; }
        public string OtherInfo { get; set; }

    }
}
