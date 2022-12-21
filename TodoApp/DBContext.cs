using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todo { get; set; }

        public DbSet<SubTodo> SubTodo { get; set; }
    }
}
