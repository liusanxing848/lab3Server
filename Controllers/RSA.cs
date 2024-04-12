using lab3Server.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace lab3Server.Controllers
{
    [ApiController]
    [Route("rsa")]
    public class RSA : ControllerBase
    {
        public static string clientPublicKey = "";
        [HttpPost("exchangePublicKey")]
        [AllowAnonymous]
        public IActionResult ExchangePublicKey([FromBody] JsonElement jsonElement)
        {
            if(jsonElement.TryGetProperty("publicKey", out JsonElement publicKeyInfoElement))
            {
                clientPublicKey = publicKeyInfoElement.GetString()!;
            }

            string serverPublicKey = KeyExchange.GetPublicKey();

            dynamic successResp = new
            {
                publicKey = serverPublicKey
            };

            return Ok(successResp);
        }


    }
}
