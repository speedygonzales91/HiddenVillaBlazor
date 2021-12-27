using Business.Repository.IRepository;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    public class HotelRoomController : Controller
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;

        public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
        {
            this._hotelRoomRepository = hotelRoomRepository;
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllHotelRooms()
        {
            var allRooms = await _hotelRoomRepository.GetAllHotelRooms();
            return Ok(allRooms);
        }

        [HttpGet("{hotelRoomId}")]
        public async Task<IActionResult> GetHotelRoom(int? hotelRoomId)
        {
            if (hotelRoomId is null)
            {
                return BadRequest(new ErrorModel() { Title = "", ErrorMessage = "Invalid Room Id", StatusCode = StatusCodes.Status400BadRequest });
            }

            var roomDetails = _hotelRoomRepository.GetHotelRoom(hotelRoomId.Value);
            if (roomDetails is null)
            {
                return BadRequest(new ErrorModel() { Title = "", ErrorMessage = "Invalid Room Id", StatusCode = StatusCodes.Status404NotFound });
            }

            return Ok(roomDetails);
        }
    }
}
