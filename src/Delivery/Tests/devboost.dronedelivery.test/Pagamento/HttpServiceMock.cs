using devboost.dronedelivery.core.domain.Interfaces;
using devboost.dronedelivery.core.services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace devboost.dronedelivery.test
{
    public class HttpServiceMock : HttpService
    {
        public override async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

        public override void SetBaseAddress(string url)
        {
        }
    }
}
