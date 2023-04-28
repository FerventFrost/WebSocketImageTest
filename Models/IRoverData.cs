using System.Net.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadImage.Models
{
    public interface IRoverData
    {
        void SetSockets(WebSocket webSocket, Sockets SetSocket);

        void CloseSockets(Sockets CloseSocket);

        Task SendBytes(byte[] Bytes, Sockets ChooseSocket);
    }
}