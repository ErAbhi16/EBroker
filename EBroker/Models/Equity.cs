using System.Diagnostics.CodeAnalysis;

namespace EBroker.Models
{
    [ExcludeFromCodeCoverage]
    public class Equity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double UnitPrice { get; set; }

    }
}
