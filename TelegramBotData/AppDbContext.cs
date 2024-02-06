using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramBotData
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserInfo> userInfo { get; set; } = null;
        //public DbSet<OnlyUserId> OnlyUserId { get; set; } = null;
        public AppDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=UsersInformation.db");
        }
    }
    public class UserInfo
    {
        public int Id { get; set; }
        public long ChatId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public string? Message { get; set; }
    }
    public class OnlyUserId
    {
        public int Id { get; set; }
        public long UserIdCopy { get; set; }
    }
}
