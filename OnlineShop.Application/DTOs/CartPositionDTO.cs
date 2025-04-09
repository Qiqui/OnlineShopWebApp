using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.DTOs
{
    public class CartPositionDTO
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public ProductDTO Product { get; set; }
        public CartDTO Cart { get; set; }
    }
}
