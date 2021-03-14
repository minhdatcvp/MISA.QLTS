using MISA.QLTS.BACKEND.COMMON.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.BACKEND.DATALAYER.Interface
{
    public interface IRefDecrementData : IBaseData<RefDecrement>
    {
        int Deletes(string param);
        RefDecrement CheckRefDecrementCodeExits(string ref_no);
    }
}
