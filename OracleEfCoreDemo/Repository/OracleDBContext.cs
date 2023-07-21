using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using OracleEfCoreDemo.Model;

namespace OracleEfCoreDemo.Repository;

public class OracleDBContext:DbContext
{
    public OracleDBContext(DbContextOptions<OracleDBContext> options)
        : base(options)
    {
        
    }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}