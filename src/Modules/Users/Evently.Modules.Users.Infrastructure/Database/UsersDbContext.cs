using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Users.Application.Abstractions.Data;
using Evently.Modules.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Users.Infrastructure.Database;
public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) 
    : DbContext(options) , IUnitOfWork
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);
        modelBuilder.ApplyDeciamalConfiguration();
        modelBuilder.ApplyRestrictRelationConfigration();

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
}

