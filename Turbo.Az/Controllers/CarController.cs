using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer;
using EntityLayer.DTOs.CarDTOs;
using EntityLayer.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace Turbo.Az.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class CarController : ControllerBase
    {
        public readonly ICarService _carService;
        public readonly IFileService _fileService;
        public readonly IHttpContextAccessor _httpContext;
        public readonly ApplicationDbContext _applicationDbContext;

        public IMapper _mapper;
        public CarController(ICarService carService, IFileService fileService, IMapper mapper, IHttpContextAccessor httpContext, ApplicationDbContext applicationDbContex)
        {

            _carService = carService;
            _fileService = fileService;
            _httpContext = httpContext;
            _mapper = mapper;
            _applicationDbContext = applicationDbContex;

        }
        [HttpPost]
        public async Task<CarViewModel> GetCar(int id)
        {

            var car =await _carService.Get(id);
            return car;
        }
        [HttpPost]
        public async Task<IActionResult> Filter([FromBody] CarFilterModel model)
        {

            var carList = _carService.Filter(model.BrandId, model.Models, model.MinYear, model.MaxYear, model.MinEngineSize, model.MaxEngineSize, model.MinOdometry, model.MaxOdometry, model.MinPrice, model.MaxPrice, model.Colors, model.FuelTypes, model.Bantypes, model.Transmissions);
            return Ok(carList);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CarAddModel carModel)
        {
            string? userName = _httpContext.HttpContext.User.Identity.Name;
            var currentUser = _applicationDbContext.Users.FirstOrDefault(x => x.UserName == userName);

            var car = _mapper.Map<Car>(carModel);
            car.InUser = currentUser.Id;
            await _carService.Add(car);
            foreach (var item in carModel.images)
            {
                if (item == null || item.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                // Read the contents of the file into a byte array
                using (var memoryStream = new MemoryStream())
                {
                    await item.CopyToAsync(memoryStream);
                    var fileContent = memoryStream.ToArray();

                    // Create a new entity to store file metadata and content
                    var fileEntity = new EntityLayer.Tables.File
                    {
                        FileName = item.FileName,
                        ContentType = item.ContentType,
                        Content = fileContent,
                        CarId = car.Id
                    };

                    // Save the file entity into the database
                    await _fileService.Add(fileEntity);
                }
            }
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update(CarAddModel carModel)
        {
            string? userName = _httpContext.HttpContext.User.Identity.Name;
            Car? car = await _applicationDbContext.Cars.FirstOrDefaultAsync(x => x.Id == carModel.Id);
            var currentUser = _applicationDbContext.Users.FirstOrDefault(x => x.UserName == userName);
            if (currentUser != null && currentUser.Id != car.InUser)
            {
                return BadRequest("You can not update data");
            }
            await _carService.Update(_mapper.Map<Car>(car));
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            await _carService.Delete(id);    
            return Ok();
        }


    }

}
