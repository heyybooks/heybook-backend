using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using UserManagement.DataAccess.Abstract;
using UserManagement.Entity.Concrete;

namespace UserManagement.DataAccess.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, UserDbContext>, IUserDal
    {
        public async Task<User> GetByEmail(string email)
        {
            using (var context = new UserDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
        }

        public async Task<User> GetByUsername(string username)
        {
            using (var context = new UserDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            }
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            using (var context = new UserDbContext())
            {
                return await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == userId);
            }
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            using (var context = new UserDbContext())
            {
                var existingEntity = await context.Users.FindAsync(entity.UserId);

                if (existingEntity == null)
                    return false;

                // Tüm alanları güncelle
                context.Entry(existingEntity).CurrentValues.SetValues(entity);
                existingEntity.UpdatedAt = DateTime.UtcNow;

                try 
                {
                    int result = await context.SaveChangesAsync();
                    return result > 0; // Güncelleme başarılıysa true döner
                }
                catch (Exception ex)
                {
                    // Hata loglaması yapılabilir
                    Console.WriteLine($"Güncelleme hatası: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
