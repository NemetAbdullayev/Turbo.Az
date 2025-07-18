using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IFileService
    {
        Task<EntityLayer.Tables.File> Add(EntityLayer.Tables.File file);
        Task<EntityLayer.Tables.File> Get(int id);
        Task Delete(int id);
    }
}
