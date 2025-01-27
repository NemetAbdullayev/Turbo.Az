using DataAccessLayer.Abstract;
using EntityLayer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
   public interface ICarRepository:IGenericRepository<Car>
    {
       public List<Car> GetCarList(int? BrandId, List<int>? models, int? minYear, int? maxYear, decimal? minEngineSize, decimal? maxEngineSize, decimal? minOdometry, decimal? maxOdometry, decimal? minPrice, decimal? maxPrice, List<int>? colors, List<int>? fuelTypes, List<int>? banTypes, List<int>? transmissions);


    }
}
