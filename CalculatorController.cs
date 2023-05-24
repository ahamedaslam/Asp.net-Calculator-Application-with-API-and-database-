using Microsoft.AspNetCore.Http;
using System.Data.Common;
using System.Data.SqlClient;
using Calculator1;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Mvc;





namespace Calculatornew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {

        [HttpGet("addition/{value_1}/{value_2}")]
        public IActionResult Addition(int value_1, int value_2)
        {
            string op = "+";
            int result = value_1 + value_2;


            
            return Ok(new { operation = "addition", result});


        }





         [HttpGet("subtraction/{value_1}/{value_2}")]
         public IActionResult Subtraction(int value_1, int value_2)
         {

            string op = "-";
            int result1 = value_1 - value_2;



            return Ok(new { operation = "subtraction", result1});
         }


        [HttpGet("Multiplication/{value_1}/{value_2}")]
        public IActionResult Multiplication(int value_1, int value_2)
        {
            string op = "*";
            int result2 = value_1 * value_2;

            
            return Ok(new { operation = "multiplication", result2});
        }




        [HttpGet("Division/{value_1}/{value_2}")]
        public IActionResult Division(int value_1, int value_2)
        {
            string op = "/";
            int result3 = value_1 / value_2;

            

            return Ok(new { operation = "division", result3});
        }
       

    }
}
    
