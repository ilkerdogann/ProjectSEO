using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICombineService
    {
        List<Combine> GetList();
        List<Combine> GetListByCombineID(int id);
        Combine GetByID(int id);
        void CombineAdd(Combine combine);
        void CombineDelete(Combine combine);
        void CombineUpdate(Combine combine);
    }
}
