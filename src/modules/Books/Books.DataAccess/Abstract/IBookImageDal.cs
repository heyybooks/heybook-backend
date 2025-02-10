using Books.Entity.Concrete;
using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Abstract
{
    public interface IBookImageDal : IEntityRepository<BookImage>
    {
    }
}
