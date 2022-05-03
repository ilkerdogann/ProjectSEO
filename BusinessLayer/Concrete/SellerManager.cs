using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class SellerManager : ISellerService
    {
        ISellerDal _sellerDal;

        public SellerManager(ISellerDal sellerDal)
        {
            _sellerDal = sellerDal;
        }

        public Seller GetByID(int id)
        {
            return _sellerDal.Get(x => x.SellerID == id);
        }

        public List<Seller> GetList()
        {
            return _sellerDal.List();
        }

        public void SellerAdd(Seller seller)
        {
            _sellerDal.Insert(seller);
        }

        public void SellerDelete(Seller seller)
        {
            _sellerDal.Insert(seller);
        }

        public void SellerUpdate(Seller seller)
        {
            _sellerDal.Insert(seller);
        }
    }
}
