using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication4
{
    public class AcessoriesModel
    {
        public int Id { get; set; }
        public string DatePost { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string SeriesNumber { get; set; }
        public string TechCharacteristics { get; set; }
        public int Price { get; set; }
        public string GuaranteePeriod { get; set; }
        public string GuaranteeOrganization { get; set; }
        public string BasicProduct { get; set; }
        public string FIORespPerson { get; set; }
        public string OtherInfo { get; set; }
    }
}
