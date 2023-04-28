using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using UploadImage.Models;

namespace UploadImage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OnlineSession : ControllerBase
    {
        private readonly RoverDataStreaming RoverData;

        public OnlineSession(RoverDataStreaming RoverData) 
        {
            this.RoverData = RoverData;
        }

        [HttpGet("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(webSocket, this.RoverData, Sockets.VideoSocket);
            }
        }

        private static async Task Echo(WebSocket webSocket, RoverDataStreaming RoverData, Sockets Socket)
        {
            RoverData.SetSockets(webSocket, Socket);
            
            var buffer = new byte[1024];
            var sendBuffer = new byte[] { 54, 57 };
            await webSocket.SendAsync(
                    new ArraySegment<byte>(sendBuffer, 0, 2  ),
                    WebSocketMessageType.Binary,
                    true,
                    CancellationToken.None);

            var PingPong = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), 
                CancellationToken.None);
            
            while (!PingPong.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(
                    new ArraySegment<byte>(buffer, 0, PingPong.Count),
                    PingPong.MessageType,
                    PingPong.EndOfMessage,
                    CancellationToken.None);

                PingPong = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), 
                    CancellationToken.None);
            }   

            await webSocket.CloseAsync(
                PingPong.CloseStatus.Value,
                PingPong.CloseStatusDescription,
                CancellationToken.None);
                
            RoverData.CloseSockets(Socket);
        }

    }
}