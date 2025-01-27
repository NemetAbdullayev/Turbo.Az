using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class FileRepository:GenericRepository<EntityLayer.Tables.File>, IFileRepository    
    {
        public FileRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }
    }
}
