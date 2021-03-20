using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QLTS.BACKEND.API.Controllers;
using MISA.QLTS.BACKEND.COMMON.Entity;
using MISA.QLTS.BACKEND.SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.QLTS.BACKEND.API.Entity
{
    /// <summary>
    /// Controller RefDecrement được kế thừa từ basecontroller để quản lý các phương thức gọi API Decrement
    /// </summary>
    /// CreatedBy: NMDAT(14/03/2021)
    public class RefDecrementsController : BasesController<RefDecrement>
    {
        public RefDecrementsController(IRefDecrementService _refDecrementService) : base(_refDecrementService)
        {
        }
    }
}
