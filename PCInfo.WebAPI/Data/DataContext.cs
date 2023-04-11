using Microsoft.EntityFrameworkCore;

namespace PCInfo.WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<MyDriveInfo> myDriveInfos { get; set; }
        public DbSet<MyPCInfo>  pcInfos { get; set; }
    }
}
