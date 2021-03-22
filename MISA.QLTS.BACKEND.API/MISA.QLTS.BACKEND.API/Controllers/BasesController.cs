using Microsoft.AspNetCore.Mvc;
using MISA.Common.Model;
using MISA.QLTS.BACKEND.SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.QLTS.BACKEND.API.Controllers
{
    /// <summary>
    /// Base Controller viết các đầu API chung
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    /// CreatedBy : NMDAT (14/03/2021)
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
        [HttpGet]
        public IActionResult Get()
        {
            var res = new ServiceResult();
            try
            {
                res = _baseService.Get();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                res.Success = false;
            }
            return new JsonResult(res);
            
        }
        
        // POST api/<CustomersController>
        /// <summary>
        /// Thêm đối tượng
        /// </summary>
        /// <param name="entity">Thực thể</param>
        [HttpPost]
        public IActionResult Post([FromBody] T entity)
        {
            var res = new ServiceResult();
            try
            {
                res = _baseService.Insert(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                res.Success = false;
            }
            return new JsonResult(res);
        }
        // POST api/<CustomersController>
        /// <summary>
        /// Sửa đối tượng
        /// </summary>
        /// <param name="entity">Thực thể</param>
        [HttpPut]
        public IActionResult Put(T entity)
        {
            var res = new ServiceResult();
            try
            {
                res = _baseService.Update(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                res.Success = false;
            }
            return new JsonResult(res);
        }
        /// <summary>
        /// Xóa đối tượng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var res = new ServiceResult();
            try
            {
                res = _baseService.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                res.Success = false;
            }
            return new JsonResult(res);
        }
        /// <summary>
        /// Lấy dữ liệu theo id
        /// </summary>
        /// <param name="id">id thực thể</param>
        /// <returns>Dữ liệu cần lấy theo id</returns>
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var res = new ServiceResult();
            try
            {
                res = _baseService.GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                res.Success = false;
            }
            return new JsonResult(res);

        }
    }
}
