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
    /// Controller Asset được kế thừa từ basecontroller để quản lý các phương thức gọi API Asset
    /// </summary>
    /// CreatedBy: NMDAT(20/02/2021)
    public class AssetsController : BasesController<Asset>
    {
        public AssetsController(IAssetService _assetService) : base(_assetService)
        {

        }

    }
}
