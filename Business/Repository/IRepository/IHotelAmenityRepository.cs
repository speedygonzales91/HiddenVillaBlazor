using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelAmenityRepository
    {
        public Task<IEnumerable<HotelAmenityDTO>> GetHotelAmenities();
        public Task<HotelAmenityDTO> GetHotelAmenityById(int hotelAmenityId);
        public Task<HotelAmenityDTO> CreateHotelAmenity(HotelAmenityDTO hotelAmenity);
        public Task<HotelAmenityDTO> UpdateHotelAmenityById(int hotelAmenityId, HotelAmenityDTO hotelAmenity);
        public Task<int> DeleteHotelAmenityById(int hotelAmenityId);

    }
}
