using Business.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RoomOrderController : Controller
    {
        private readonly IRoomOrderDetailsRepository _db;

        public RoomOrderController(IRoomOrderDetailsRepository db)
        {
            this._db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomOrderDetailsDTO details)
        {
            if (ModelState.IsValid)
            {
                var result = await _db.Create(details);
                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel
                {
                    ErrorMessage = "Error while creating room details / booking"
                });
            }
        }
    }
}
