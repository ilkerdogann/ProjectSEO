using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Combine
    {
        [Key]
        public int CombineID { get; set; }
        public string CombineName { get; set; }
        public string CombineDescription { get; set; }
        public string CombineImage { get; set; }
        public int CombinePrice { get; set; }
        public int ProductID { get; set; }


        public virtual Product Product { get; set; }
    }
}
