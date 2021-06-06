using App.Models;
using Microsoft.EntityFrameworkCore;
using SysCore.Models;
using System;

namespace App.Data
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext()
        {

        }

        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }


        // 必须要加的方法
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                   AppConfigurtaionServices.conn,
                    options => options.EnableRetryOnFailure());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Test> Tests { get; set; }

        public DbSet<CooperativeCourierData> CooperativeCourierDatas { get; set; }

        public DbSet<ExpresscompanyData> ExpresscompanyDatas { get; set; }

        public DbSet<ExpressStaffData> ExpressStaffDatas { get; set; }
        public DbSet<RetentionTimeData> RetentionTimeDatas { get; set; }


        public DbSet<ArrivalSMSData> ArrivalSMSDatas { get; set; }

        public DbSet<PhotoData> PhotoDatas { get; set; }

        public DbSet<StaffManagementData> StaffManagementDatas { get; set; }
        public DbSet<StoreData> StoreDatas { get; set; }

        public DbSet<SwitchSettingsData> SwitchSettingsDatas { get; set; }
    }
}
