using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Product 
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductComment { get; set; }
        public string ProductImage { get; set; }
        public string ProductKey { get; set; }
        public int ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<Combine> Combines { get; set; }
    }
}
