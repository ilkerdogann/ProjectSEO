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
    public class CombineManager : ICombineService
    {
        ICombineDal _combineDal;

        public CombineManager(ICombineDal combineDal)
        {
            _combineDal = combineDal;
        }

        public void CombineAdd(Combine combine)
        {
            _combineDal.Insert(combine);
        }

        public void CombineDelete(Combine combine)
        {
            _combineDal.Delete(combine);
        }

        public void CombineUpdate(Combine combine)
        {
            _combineDal.Update(combine);
        }

        public Combine GetByID(int id)
        {
            return _combineDal.Get(x => x.CombineID == id);
        }

        public List<Combine> GetList()
        {
            return _combineDal.List();
        }

        public List<Combine> GetListByCombineID(int id)
        {
            return _combineDal.List(x => x.CombineID == id);
        }
    }
}
