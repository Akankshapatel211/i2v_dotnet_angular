using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography; 
using System.Text; 

namespace Test_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashController : Controller
    {
        [HttpGet("{firstName}")]
        public JsonResult GetHash(string firstName)
        {
            Console.WriteLine(firstName);
            string input = firstName;
            // byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
             byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            
            using (var hash = SHA256.Create())
            {
                byte[] hashedBytes = hash.ComputeHash(inputBytes);

                //  var hashString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
                var hashString = BitConverter.ToString(hashedBytes).Replace("-", "").ToUpperInvariant();
                
                // Return a HashResponse Object which contains the hashString (alphanumeric, without '-') converted from hashedBytes
                var response = new HashResponse
                {
                    Hash = hashString
                };
                return new JsonResult(response);
            }
        }

    }

    public class HashResponse
    {
        public string Hash { get; set; }
    }
}
