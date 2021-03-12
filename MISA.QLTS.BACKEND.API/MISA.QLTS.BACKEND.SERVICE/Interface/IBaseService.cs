using MISA.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.BACKEND.SERVICE.Interface
{
    public interface IBaseService<T>
    {
        ServiceResult Get();
    }
}
