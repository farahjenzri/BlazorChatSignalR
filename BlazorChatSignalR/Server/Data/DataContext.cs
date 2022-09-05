using BlazorChatSignalR.Server.Models;
using Microsoft.EntityFrameworkCore;


namespace BlazorChatSignalR.Server.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
