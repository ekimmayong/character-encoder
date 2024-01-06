using Base64Encoder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base64Encoder.Domain.Interface
{
    public interface IEncoderService
    {
        Task Encode(string text, Func<char, Task> characterHandler, CancellationToken cancellationToken);
    }
}
