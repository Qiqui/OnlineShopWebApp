using OnlineShop.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Profiles
{
    public class ComparisonProfile
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<ProductDTO> Products { get; }
    }
}
