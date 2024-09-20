using FrontEndApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FrontendWebApi.Data;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

    public DbSet<TodoItem> TodoItems { get; set; }
}
