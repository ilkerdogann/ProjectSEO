using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSEO.Models
{
    public class ProductAdd
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public IFormFile ImageURL { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

    }
}