using EntityLayer.DTOs.CarDTOs;
using EntityLayer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICarService
    {
        Task<Car> Add(Car car);
        Task<CarViewModel> Get(int id);
        Task<Car> Update(Car car);
        Task Delete(int id);
        List<Car> Filter(int BrandId, List<int> models, int minYear, int maxYear, decimal minEngineSize, decimal maxEngineSize, decimal minOdometry, decimal maxOdometry, decimal minPrice, decimal maxPrice, List<int> colors, List<int> fuelTypes, List<int> banTypes, List<int> transmissions);


    }
}


