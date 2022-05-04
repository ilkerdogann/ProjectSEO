﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Seller
    {
        [Key]
        public int SellerID { get; set; }
        public string SellerName { get; set; }
        public string SellerMail { get; set; }
        public string SellerPassword { get; set; }
    }
}
