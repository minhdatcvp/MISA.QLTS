﻿using MISA.Common.Model;
using MISA.QLTS.BACKEND.COMMON.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.BACKEND.SERVICE.Interface
{
    public interface IRefDecrementService : IBaseService<RefDecrement>
    {
        ServiceResult Deletes(string param);
    }
}