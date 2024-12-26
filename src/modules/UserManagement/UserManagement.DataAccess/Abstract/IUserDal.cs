using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess;
using UserManagement.Entity.Concrete;

namespace UserManagement.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByUsername(string username);
        Task<User> GetByIdAsync(int userId); // Kullanıcıyı ID'ye göre bul
        Task<bool> UpdateAsync(User entity); // Güncelleme metodu
       
    }
}