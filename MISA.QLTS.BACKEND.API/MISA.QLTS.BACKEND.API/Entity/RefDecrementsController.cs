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
    public class RefDecrementsController : BasesController<RefDecrement>
    {
        public RefDecrementsController(IRefDecrementService _refDecrementService) : base(_refDecrementService)
        {
        }

        /// <summary>
        /// Xóa nhiều đối tượng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<ValuesController>
        //[HttpDelete]
        //public IActionResult Deletes([FromQuery] string param)
        //{
        //    // Gọi đến hàm Insert thực hiện validate -> Xóa
        //    var res = _refDecrementService.Deletes(param);

        //    if (res.Success == false)
        //    {
        //        return StatusCode(400, res.Data);
        //    }
        //    else if (res.Success == true && (int)res.Data > 0)
        //    {
        //        return StatusCode(201, res.Data);
        //    }
        //    else
        //    {
        //        return StatusCode(200, res.Data);
        //    }
        //}

    }
}
