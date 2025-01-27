using BusinessLayer.Abstract;
using BusinessLayer.AuthService.Interface;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AuthService.Implementation
{
    public class FileService : IFileService
    {


        private readonly IFileRepository _fileRepository;


        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;


        }
        public async Task<EntityLayer.Tables.File> Add(EntityLayer.Tables.File file)
        {
            EntityLayer.Tables.File File = await _fileRepository.Add(file);
            return File;
        }

        public async Task<EntityLayer.Tables.File> Get(int id)
        {
            var file = await _fileRepository.Get(id);
            return  file;
        
        }
        public async Task Delete(int id)
        {
            EntityLayer.Tables.File file = await _fileRepository.Get(id);
            file.IsDeleted = true;
            await _fileRepository.Update(file);
        }
    }
}