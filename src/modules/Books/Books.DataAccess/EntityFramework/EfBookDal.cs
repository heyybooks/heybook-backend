﻿using Books.DataAccess.Abstract;
using Books.Entity.Concrete;
using Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.EntityFramework
{
    public class EfBookDal : EfEntityRepositoryBase<Book,HeybooksContext>,IBookDal
    {
    }
}
