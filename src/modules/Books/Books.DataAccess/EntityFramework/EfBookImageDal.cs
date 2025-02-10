using Books.DataAccess.Abstract;
using Books.Entity.Concrete;
using Core.DataAccess.EntityFramework;

namespace Books.DataAccess.EntityFramework
{
    public class EfBookImageDal : EfEntityRepositoryBase<BookImage, HeybooksContext>, IBookImageDal
    {
    }
}
