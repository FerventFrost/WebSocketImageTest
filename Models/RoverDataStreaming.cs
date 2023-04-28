using System.Net.WebSockets;
using System.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadImage.Models
{
    public enum Sockets 
    {
        VideoSocket,
        RecordSocket
    }

    public class RoverDataStreaming : IRoverData
    {
        private WebSocket VideoSocket {get; set;}
        private WebSocket RecordSocket {get; set;}
        public void SetSockets(WebSocket webSocket, Sockets SetSocket) 
        {
            switch(SetSocket)
            {
                case Sockets.VideoSocket:
                    this.VideoSocket = webSocket;
                    break;
                
                case Sockets.RecordSocket:
                    this.RecordSocket = webSocket;
                    break;
            }  
        }
        public void CloseSockets(Sockets CloseSocket) 
        {
            switch(CloseSocket)
            {
                case Sockets.VideoSocket:
                    this.VideoSocket.Dispose();
                    break;
                
                case Sockets.RecordSocket:
                    this.RecordSocket.Dispose();
                    break;
            }  
        }

        // Methods that is related to Sending Bytes (Video, Image, Records, etc..)
        public async Task SendBytes(byte[] Bytes, Sockets ChooseSocket) {
            switch(ChooseSocket)
            {
                case Sockets.VideoSocket:
                    await Send(Bytes, this.VideoSocket);
                    break;
                
                case Sockets.RecordSocket:
                    await Send(Bytes, this.RecordSocket);
                    break;
            } 
        }

        // Common send implementation
        private static async Task Send(byte[] Bytes, WebSocket Sockets)
        {
            await Sockets.SendAsync(
                new ArraySegment<byte>(Bytes, 0, Bytes.Length),
                WebSocketMessageType.Binary,
                true,
                CancellationToken.None);
        }

    }

}