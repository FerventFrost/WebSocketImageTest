using UploadImage.Models;
using Microsoft.AspNetCore.Mvc;

namespace UploadImage.Test
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestRoverDataController : ControllerBase
    {
        private RoverDataStreaming RoverData;
        public TestRoverDataController(RoverDataStreaming RoverData)
        {
            this.RoverData = RoverData;
        }
        [HttpGet]
        public ActionResult StartTest()
        {
            // Arrange
            // Use of the full name beacuse File has conflict with File Method in ControllerBase
            byte[] bytes = System.IO.File.ReadAllBytes("C:/Users/BAHER/Pictures/gabe-770x470.jpg");

            // Act
            for(int i = 0; i < 5; i++) 
            {
                this.RoverData.SendBytes(bytes, Sockets.VideoSocket);
                Thread.Sleep(5000);
                Console.WriteLine("Test Number: " + i);
            }

            // Assert
            return Ok();
        }
    }
}