using devboost.dronedelivery.security.domain.Entites;
using Newtonsoft.Json.Linq;
using Xunit;


namespace devboost.dronedelivery.test.JSONHelper
{
    public class JSONHelperTests
    {
        private readonly LoginDTO _loginDTO;

        public JSONHelperTests()
        {
            _loginDTO = new LoginDTO { UserId = "usuario", Password = "teste123" };
        }

        [Fact]
        public void DeserializeJsonToObject()
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(_loginDTO);
            var result = devboost.dronedelivery.Services.JSONHelper.DeserializeJsonToObject<LoginDTO>(json);
            Assert.Equal("teste123", result.Password);

        }

        [Fact]
        public void ConvertObjectToByteArrayContent()
        {
            Assert.NotNull(devboost.dronedelivery.Services.JSONHelper.ConvertObjectToByteArrayContent<LoginDTO>(_loginDTO));

        }

        [Fact]
        public void DeserializeJObject()
        {
            JObject login = JObject.FromObject(_loginDTO);

            var result = devboost.dronedelivery.Services.JSONHelper.DeserializeJObject<Newtonsoft.Json.Linq.JObject>(login);
            Assert.NotNull(result);

        }
    }
}
