using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.AuthService.Implementation;
using BusinessLayer.AuthService.Interface;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Turbo.Az.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class FileController : ControllerBase
    {
        public readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpGet]
        public async Task<EntityLayer.Tables.File> GetFile(int id)
        {
            var file = await _fileService.Get(id);
            return file;

        }
        
    }
}