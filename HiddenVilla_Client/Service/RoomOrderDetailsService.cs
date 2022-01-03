using HiddenVilla_Client.Service.IService;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class RoomOrderDetailsService : IRoomOrderDetailsService
    {
        private readonly HttpClient _client;

        public RoomOrderDetailsService(HttpClient client) => this._client = client;

        public Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(RoomOrderDetailsDTO details)
        {
            throw new NotImplementedException();
        }

        public async Task<RoomOrderDetailsDTO> SaveRoomOrderDetailsDTO(RoomOrderDetailsDTO details)
        {

            var content = JsonConvert.SerializeObject(details);

            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/roomorder/create", bodyContent);
            //FOR DEBUG
            //var respResult = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();

                var res = JsonConvert.DeserializeObject<RoomOrderDetailsDTO>(contentTemp);
                return res;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(error.ErrorMessage);
            }
        }
    }
}
