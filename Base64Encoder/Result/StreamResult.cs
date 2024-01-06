using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Base64Encoder.Result
{
    public class StreamResult: IActionResult
    {
        private readonly string _content;
        public StreamResult(string content)
        {
            _content = content;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var encodedText = Convert.ToBase64String(Encoding.UTF8.GetBytes(_content));
            var response = context.HttpContext.Response;
            response.ContentType = "text/plain";

            //foreach (var character in encodedText)
            //{
            //    Console.Write(character);
            //    await response.Body.WriteAsync(Encoding.UTF8.GetBytes(new[] { character }), 0, 1);
            //    await response.Body.FlushAsync();
            //    await Task.Delay(500); // Add a delay if needed
            //}

            using (var streamWriter = new StreamWriter(response.Body, Encoding.UTF8))
            {
                foreach(char character in encodedText)
                {
                    Console.Write(character);
                    await streamWriter.WriteAsync(character);
                    await streamWriter.FlushAsync();
                    await Task.Delay(2000);
                }
                
            }
        }
    }
}
