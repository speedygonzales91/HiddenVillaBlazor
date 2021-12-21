using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelAmenityRepository : IHotelAmenityRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HotelAmenityRepository(ApplicationDbContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }


        public async Task<HotelAmenityDTO> CreateHotelAmenity(HotelAmenityDTO hotelAmenityDTO)
        {
            try
            {
                var hotelAmenity = _mapper.Map<HotelAmenityDTO, HotelAmenity>(hotelAmenityDTO);
                var addedHotelAmenity = await _db.HotelAmenities.AddAsync(hotelAmenity);
                await _db.SaveChangesAsync();

                return _mapper.Map<HotelAmenity, HotelAmenityDTO>(addedHotelAmenity.Entity);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<int> DeleteHotelAmenityById(int hotelAmenityId)
        {
            try
            {
                var hotelAmenity = await _db.HotelAmenities.FindAsync(hotelAmenityId);
                if (hotelAmenity is not null)
                {
                    _db.HotelAmenities.Remove(hotelAmenity);
                    return await _db.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {

                return 0;
            }

        }

        public async Task<IEnumerable<HotelAmenityDTO>> GetHotelAmenities()
        {
            try
            {
                return _mapper.Map<IEnumerable<HotelAmenity>, IEnumerable<HotelAmenityDTO>>(_db.HotelAmenities);
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public async Task<HotelAmenityDTO> GetHotelAmenityById(int hotelAmenityId)
        {
            try
            {
                var hotelAmenity = await _db.HotelAmenities.FindAsync(hotelAmenityId);
                if (hotelAmenity is not null)
                {
                    return _mapper.Map<HotelAmenity, HotelAmenityDTO>(hotelAmenity);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<HotelAmenityDTO> UpdateHotelAmenityById(int hotelAmenityId, HotelAmenityDTO hotelAmenityDTO)
        {
            try
            {
                if (hotelAmenityId == hotelAmenityDTO.Id)
                {
                    var amenityDetails = await _db.HotelAmenities.FindAsync(hotelAmenityId);
                    var amenity = _mapper.Map<HotelAmenityDTO, HotelAmenity>(hotelAmenityDTO, amenityDetails);
                    var updatedAmenity = _db.HotelAmenities.Update(amenity);
                    await _db.SaveChangesAsync();

                    return _mapper.Map<HotelAmenity, HotelAmenityDTO>(updatedAmenity.Entity);
                }
                else
                {
                    //invalid
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
