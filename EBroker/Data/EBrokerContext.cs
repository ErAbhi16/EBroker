using EBroker.Data.Entities;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace EBroker.Data
{
    [ExcludeFromCodeCoverage]
    public class EBrokerContext : DbContext
    {
        protected IHostEnvironment HostEnvironment { get; set; }
        public virtual DbSet<Equity> Equity { get; set; }
        public virtual DbSet<Trader> Trader { get; set; }

        public virtual DbSet<TraderHolding> TraderHolding { get; set; }
        public virtual DbSet<TraderTransaction> TraderTransaction { get; set; }

        public EBrokerContext(DbContextOptions<EBrokerContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TraderHolding>()
                .HasKey(mr => new { mr.TraderId, mr.EquityId });
        }
    }
}
