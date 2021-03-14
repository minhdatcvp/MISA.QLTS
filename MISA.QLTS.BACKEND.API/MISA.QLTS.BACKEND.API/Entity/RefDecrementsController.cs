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
    [Route("api/[controller]")]
    [ApiController]
    public class RefDecrementsController : BasesController<RefDecrement>
    {
        IRefDecrementService _refDecrementService;
        public RefDecrementsController(IBaseService<RefDecrement> baseService, IRefDecrementService refDecrementService) : base(baseService)
        {
            _refDecrementService = refDecrementService;
        }
        /// <summary>
        /// Xóa nhiều đối tượng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<ValuesController>
        [HttpDelete]
        public IActionResult Deletes([FromQuery] string param)
        {
            // Gọi đến hàm Insert thực hiện validate -> Xóa
            var res = _refDecrementService.Deletes(param);

            if (res.Success == false)
            {
                return StatusCode(400, res.Data);
            }
            else if (res.Success == true && (int)res.Data > 0)
            {
                return StatusCode(201, res.Data);
            }
            else
            {
                return StatusCode(200, res.Data);
            }
        }
    }
}
