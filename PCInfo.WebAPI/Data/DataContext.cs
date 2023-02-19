using Microsoft.EntityFrameworkCore;

namespace PCInfo.WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<MyDriveInfo> myDriveInfos { get; set; }
        public DbSet<MyPCInfo>  pcInfos { get; set; }
    }
}
