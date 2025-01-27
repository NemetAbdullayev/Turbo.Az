using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.CarDTOs
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int Year { get; set; }
        public decimal EngineSize { get; set; }
        public decimal Odometry { get; set; }
        public decimal Price { get; set; }
        public int ColorId { get; set; }
        public int FuelTypeId { get; set; }
        public int BantypeId { get; set; }
        public int TransmissionId { get; set; }
        public virtual ICollection<int> FileIds { get; set; }
        public int InUser { get; set; }
    }
}
