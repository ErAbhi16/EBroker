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

            modelBuilder.Entity<Equity>()
                .HasData(
                        new Equity
                        {
                            Id=12,
                            Name = "Tata Motors",
                            UnitPrice = 445.55
                        },
                        new Equity
                        {
                            Id = 14,
                            Name = "Indusind Bank Ltd",
                            UnitPrice = 848
                        },
                        new Equity
                        {
                            Id = 15,
                            Name = "HDFC Bank",
                            UnitPrice = 1424.50
                        },
                        new Equity
                        {
                            Id=23,
                            Name = "Reliance Industries Ltd",
                            UnitPrice = 2276.80
                        },
                        new Equity
                        {
                            Id=45,
                            Name = "Rain Industries Ltd",
                            UnitPrice = 200.50
                        },
                        new Equity
                        {
                            Id=67,
                            Name = "Goa Carbon Ltd",
                            UnitPrice = 311.95
                        });

            modelBuilder.Entity<Trader>()
                .HasData(
                        new Trader
                        {
                            Id=1,
                            Name = "Abhi",
                            Funds = 4500.50                            
                        },
                        new Trader
                        {
                            Id=2,
                            Name = "Raghav",
                            Funds = 1234.00
                        },
                       new Trader
                       {
                           Id=3,
                           Name = "Ashwani",
                           Funds = 4500.50
                       },
                        new Trader
                        {
                            Id=4,
                            Name = "Manas",
                            Funds = 2319
                        },
                        new Trader
                        {
                            Id=5,
                            Name = "Arshbeer",
                            Funds = 8344
                        },
                        new Trader
                        {
                            Id=6,
                            Name = "Saurav",
                            Funds = 9000
                        });
        }
    }
}
