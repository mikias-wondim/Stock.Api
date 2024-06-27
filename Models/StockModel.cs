using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Api.Models
{
    public class StockModel
    {
        public int Id { get; set; }
        [Required]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18, 3)")]
        public decimal Purchase { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal LastDiv { get; set; }
        [Required]
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
    }
}