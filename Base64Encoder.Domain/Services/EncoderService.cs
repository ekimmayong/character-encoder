using Base64Encoder.Domain.Interface;
using Base64Encoder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Base64Encoder.Domain.Services
{
    public class EncoderService: IEncoderService
    {
        public async Task Encode(string text, Func<char, Task> characterHandler, CancellationToken cancellationToken)
        {
            var encodedText = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
            var random = new Random();

            foreach (char character in encodedText)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    await characterHandler(' '); // Placeholder character for cancellation
                    return;
                }

                await characterHandler(character);

                // Introduce a random delay
                await Task.Delay(random.Next(1000, 2001), cancellationToken);
            }
        }
    }
}
