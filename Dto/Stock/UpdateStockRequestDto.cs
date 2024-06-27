using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Api.Dto.Stock
{
    public class UpdateStockRequestDto
    {
        [Required(ErrorMessage = "Symbol is required.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Symbol must be between 2 and 10 characters.")]
        public string Symbol { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company name is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Company name must be between 2 and 20 characters.")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Purchase amount is required.")]
        [Range(1, 1_000_000_000_000, ErrorMessage = "Purchase amount must be between 1 and 1 trillion.")]
        public decimal Purchase { get; set; }

        [Required(ErrorMessage = "Last dividend amount is required.")]
        [Range(0, 100, ErrorMessage = "Last dividend must be between 0 and 100.")]
        public decimal LastDiv { get; set; }

        [Required(ErrorMessage = "Industry is required.")]
        public string Industry { get; set; } = string.Empty;

        [Range(1, 100_000_000_000_000, ErrorMessage = "Market cap must be between 1 and 100 trillion.")]
        public long MarketCap { get; set; }
    }
}