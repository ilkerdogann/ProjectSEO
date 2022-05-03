using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ISellerService
    {
        List<Seller> GetList();
        void SellerAdd(Seller seller);
        Seller GetByID(int id);
        void SellerDelete(Seller seller);
        void SellerUpdate(Seller seller);
    }
}
