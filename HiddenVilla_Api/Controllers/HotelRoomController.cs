using Business.Repository.IRepository;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        //[Authorize(Roles = SD.Role_Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllHotelRooms(string checkInDate = null, string checkOutDate = null)
        {
            if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to be supplied"
                });
            }
            if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckInDate format. Valid format: MM/dd/yyy"
                });
            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckOutDate format. Valid format: MM/dd/yyy"
                });
            }

            var allRooms = await _hotelRoomRepository.GetAllHotelRooms();
            return Ok(allRooms);
        }

        [HttpGet("{hotelRoomId}")]
        public async Task<IActionResult> GetHotelRoom(int? hotelRoomId, string checkInDate = null, string checkOutDate = null)
        {
            if (hotelRoomId is null)
            {
                return BadRequest(new ErrorModel() { Title = "", ErrorMessage = "Invalid Room Id", StatusCode = StatusCodes.Status400BadRequest });
            }

            if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to be supplied"
                });
            }
            if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckInDate format. Valid format: MM/dd/yyy"
                });
            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckOutDate format. Valid format: MM/dd/yyy"
                });
            }

            var roomDetails = await _hotelRoomRepository.GetHotelRoom(hotelRoomId.Value, checkInDate, checkOutDate);
            if (roomDetails is null)
            {
                return BadRequest(new ErrorModel() { Title = "", ErrorMessage = "Invalid Room Id", StatusCode = StatusCodes.Status404NotFound });
            }

            return Ok(roomDetails);
        }
    }
}
