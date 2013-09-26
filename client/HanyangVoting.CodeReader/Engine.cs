using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.CodeReader
{
    public class Engine
    {
        public Action<string> Readed { get; set; }

        public Engine()
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count <= 0)
            {
                throw new Exception();
            }

            var videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += videoSource_NewFrame;
            videoSource.Start();
        }

        void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        public string ReadTicket()
        {
            throw new NotImplementedException();
        }

        public string ReadIdCard()
        {
            throw new NotImplementedException();
        }
    }
}
