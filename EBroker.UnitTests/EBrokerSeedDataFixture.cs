using EBroker.Data;
using EBroker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBroker.UnitTests
{
    public class EBrokerSeedDataFixture : IDisposable
    {
        public EBrokerContext context { get; private set; }

        private bool disposed = false;
        public EBrokerSeedDataFixture()
        {
            var options = new DbContextOptionsBuilder<EBrokerContext>()
            .UseInMemoryDatabase(databaseName: "EBrokerDB")
            .Options;

            // Insert seed data into the database using one instance of the context
            context = new EBrokerContext(options);
            context.Equity.Add(new Equity { Id = 1, Name = "Tata Motors", UnitPrice = 445.55 });
            context.Equity.Add(new Equity { Id = 2, Name = "Rain Industries", UnitPrice = 200.50 });
            context.Equity.Add(new Equity { Id = 3, Name = "Reliance Industries", UnitPrice = 2276.80 });
            context.TraderHolding.Add(new TraderHolding { EquityId = 2, TraderId = 1, UnitHoldings = 10 });
            context.TraderHolding.Add(new TraderHolding { EquityId = 3, TraderId = 5, UnitHoldings =20 });
            context.Trader.AddRange(new Trader
            {
                Id = 1,
                Name = "Abhi",
                Funds = 450.50
            },
                    new Trader
                    {
                        Id = 2,
                        Name = "Raghav",
                        Funds = 123400.00
                    },
                   new Trader
                   {
                       Id = 3,
                       Name = "Ashwani",
                       Funds = 450000.50
                   },
                    new Trader
                    {
                        Id = 4,
                        Name = "Manas",
                        Funds = 231900
                    },
                    new Trader
                    {
                        Id = 5,
                        Name = "Arshbeer",
                        Funds = 834400
                    },
                    new Trader
                    {
                        Id = 6,
                        Name = "Saurav",
                        Funds = 90000
                    });

            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }
    }
}
