using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer;
using DataAccessLayer.Abstract;
using EntityLayer.DTOs.CarDTOs;
using EntityLayer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CarService : ICarService
    {


        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarService(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
         
        }
        public async Task<CarViewModel> Get(int id)
        {
            Car Car = await _carRepository.Get(id);
          var carViewModel=  _mapper.Map<CarViewModel>(Car);
            if(Car!=null && Car.Files!=null)
            { 
            carViewModel.FileIds = Car.Files.Select(x => x.Id).ToList();
            }
            return carViewModel;
        }
        public async Task<Car> Add(Car car)
        {
            Car Car =await _carRepository.Add(car);
            return Car;
        }

  

        public async Task<Car> Update(Car car)
        {
            Car Car = await _carRepository.Update(car);
            return Car;
        }
        //public async Task<CarViewModel> Get(int id)
        //{
        //    Car Car = await _carRepository.Get(id);
        //    CarViewModel carViewModel = new CarViewModel();
        //    carViewModel.BantypeName=

        //    return Car;
        //}

        public  List<Car> Filter(int BrandId, List<int> models, int minYear, int maxYear, decimal minEngineSize, decimal maxEngineSize, decimal minOdometry, decimal maxOdometry, decimal minPrice, decimal maxPrice, List<int> colors, List<int> fuelTypes, List<int> banTypes, List<int> transmissions)
        {
            var cars = _carRepository.GetCarList(BrandId, models, minYear, maxYear, minEngineSize, maxEngineSize, minOdometry, maxOdometry, minPrice, maxPrice, colors, fuelTypes, banTypes, transmissions).ToList();
            return cars;
        }

        public async Task Delete(int id)
        {
            var Car = await _carRepository.Get(id);
            if(Car!=null)
            {
                Car.IsDeleted = true;
                await _carRepository.Update(Car);
            }
            
         
        }
    }
}
