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
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
