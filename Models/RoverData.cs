using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadImage.Models
{
    public class RoverData
    {
        private RoverDataStreaming RoverDataStreaming;
        private byte[] Bytes { get; set; }

        public RoverData(RoverDataStreaming roverDataStreaming)
        {
            this.RoverDataStreaming = roverDataStreaming;
        }

        public void AcceptBytes(byte[] Bytes)
        {
            this.Bytes = Bytes;
        }

    }
}