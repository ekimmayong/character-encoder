using Base64Encoder.Models;
using Microsoft.AspNetCore.Mvc;
using Base64Encoder.Domain.Interface;
using System.Text;
using Base64Encoder.Result;

namespace Base64Encoder.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IEncoderService _encoder;
        public HomeController(IEncoderService encoder)
        {
            _encoder = encoder;
        }
        [HttpPost]
        [Route("encode")]
        public async Task EncodeText([FromBody] RequestModel model, CancellationToken cancellationToken)
        {
            async Task CharacterHandler(char character)
            {
                await HttpContext.Response.WriteAsync(character.ToString(), cancellationToken);
                await Response.Body.FlushAsync();
            }

            await _encoder.Encode(model.Text, CharacterHandler, cancellationToken);

        }
    }
}
