using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.CarDTOs
{
    public class CarFilterModel
    {
        public int BrandId { get; set; }
        public List<int>? Models { get; set; }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
        public decimal MinEngineSize { get; set; }
        public decimal MaxEngineSize { get; set; }
        public decimal MinOdometry { get; set; }
        public decimal MaxOdometry { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public List<int>? Colors { get; set; }
        public List<int>? FuelTypes { get; set; }
        public List<int>? Bantypes { get; set; }
        public List<int>? Transmissions { get; set; }
    }
}
