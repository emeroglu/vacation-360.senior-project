using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Vacation360.Models.Orm
{
    public class Entities : DbContext
    {
        public Entities()
            :base("data source=37.230.108.249;initial catalog=farbeyond;user id=emeroglu;password=Erhan116emre800;MultipleActiveResultSets=True;App=EntityFramework")
        {

        }

        public DbSet<vaCity> City { get; set; }

        public DbSet<vaComment> Comment { get; set; }

        public DbSet<vaCountry> Country { get; set; }

        public DbSet<vaFavorite> Favorite { get; set; }

        public DbSet<vaHotel> Hotel { get; set; }

        public DbSet<vaMember> Member { get; set; }

        public DbSet<vaMemberType> MemberType { get; set; }

        public DbSet<vaPhoto> Photo { get; set; }

        public DbSet<vaPhotoTag> PhotoTag { get; set; }

        public DbSet<vaTag> Tag { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}