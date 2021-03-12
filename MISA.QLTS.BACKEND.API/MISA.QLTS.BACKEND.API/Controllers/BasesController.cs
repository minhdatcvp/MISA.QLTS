using Microsoft.AspNetCore.Mvc;
using MISA.QLTS.BACKEND.SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.QLTS.BACKEND.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        /// <summary>
        /// Khai báo interface tham chiếu đến BaseService
        /// </summary>
        IBaseService<T> _baseService;
        public BasesController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }
        /// <summary>
        /// Lấy toàn bộ dữ liệu đối tượng
        /// </summary>
        /// <returns>Danh sách dữ liệu dối tượng</returns>
        

        // GET: api/<BasesController>
        [HttpGet]
        public IActionResult Get()
        {
            var res = _baseService.Get();
            var result = res.Data as List<T>;
            // Nếu có kết quả trả về result.Count > 0 thì trả về 200 nếu không trả về 204
            if (result.Count == 0)
                return StatusCode(204, res.Data);
            else
                return StatusCode(200, res.Data);
        }

        // GET api/<BasesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BasesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BasesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BasesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
