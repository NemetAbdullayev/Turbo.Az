using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class CarRepository : GenericRepository<Car>,
        ICarRepository
    {
        public ApplicationDbContext _dataContext;
        public CarRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
            _dataContext=dataContext;
        }
        [HttpGet]
        public  List<Car> GetCarList(int? BrandId, List<int> models, int? minYear, int? maxYear, decimal? minEngineSize, decimal? maxEngineSize, decimal? minOdometry, decimal? maxOdometry, decimal? minPrice, decimal? maxPrice, List<int>? colors, List<int>? fuelTypes, List<int>? banTypes, List<int>? transmissions)
        {
           
            List<Car> cars =  _dataContext.Cars.Where(x => ((BrandId == null) ? true : x.BrandId == BrandId )&& ((models.Count == 0) ? true : models.Contains(x.ModelId)) &&
              ( (minYear == null) ? true : x.Year >= minYear) && ((maxYear == null) ? true : x.Year <= maxYear) &&
               ((minEngineSize == null) ? true : x.EngineSize >= minEngineSize) && ((maxEngineSize == null) ? true : x.EngineSize <= maxEngineSize) &&
              ( (minOdometry == null) ? true : x.Odometry >= minOdometry) &&( (maxOdometry == null) ? true : x.Odometry <= maxOdometry) &&
               ( (minPrice == null) ? true : x.Price >= minPrice) && ((maxPrice == null) ? true : x.Price <= maxPrice) &&
               ( (colors == null || colors.Count == 0) ? true : colors.Contains(x.ColorId)) &&
               ( (fuelTypes == null || fuelTypes.Count == 0) ? true : fuelTypes.Contains(x.FuelTypeId) )&&
                ( (banTypes == null || banTypes.Count == 0) ? true : banTypes.Contains(x.BantypeId) )&&
               ( (transmissions == null || transmissions.Count == 0) ? true : transmissions.Contains(x.TransmissionId))).ToList();
            return cars;
        }
    }
}