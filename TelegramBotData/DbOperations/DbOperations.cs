using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotData.DbOperations
{
    public class DbOperations
    {
        public async Task AddValuesAsync(long chatId,string userName,string firstName,string lastName,string Bio, string message)
        {
            UserInfo userInfo = new UserInfo
            {
                ChatId = chatId,
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                Bio = Bio,
                Message = message
            };
            await AddAsync1(userInfo, chatId);
        }
        public async Task AddAsync1(UserInfo userInfo, long chatId)
        {
            using(AppDbContext db  = new AppDbContext())
            {
                await db.AddRangeAsync(userInfo);
                await db.SaveChangesAsync();
            }
        }
    }
}
