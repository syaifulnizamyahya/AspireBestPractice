using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.DTOs
{
    public class UpdateProductDto
    {
        [Required]
        public string Name { get; set; }
        [Range(0.01, 1_000_000)]
        public decimal Price { get; set; }
    }
}
