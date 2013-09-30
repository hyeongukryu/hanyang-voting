using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace HanyangVoting.CodeReader
{
    public class CodeReaderEngine
    {
        public event Action<Bitmap> NewFrame;
        public event Action<string> NewCode;

        private AutoResetEvent _newFrameEvent = new AutoResetEvent(false);
        private Bitmap _lastBitmap = null;
        private object _lastBitmapLock = new object();
        private IVideoSource _videoSource = null;
        private Task _task = null;

        public CodeReaderEngine()
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count <= 0)
            {
                throw new Exception();
            }

            _videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            _videoSource.NewFrame += videoSource_NewFrame;
        }

        public void Start()
        {
            _videoSource.Start();
            if (_task == null)
            {
                Task.Run(new Action(CodeWorker));
            }
        }

        public void Stop()
        {
            _videoSource.SignalToStop();
        }

        void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            var bitmap = eventArgs.Frame.CloneBitmap();
            if (NewFrame != null)
            {
                NewFrame(bitmap);
            }

            lock (_lastBitmapLock)
            {
                _lastBitmap = bitmap;
            }
            _newFrameEvent.Set();
        }

        string Process()
        {
            Bitmap currentBitmap = null;
            
            _newFrameEvent.WaitOne();
            lock (_lastBitmapLock)
            {
                currentBitmap = _lastBitmap;
            }

            if (currentBitmap == null)
            {
                return null;
            }

            var luminanceSource = new BitmapLuminanceSource(currentBitmap);
            var binarizer = new HybridBinarizer(luminanceSource);
            var binaryBitmap = new BinaryBitmap(binarizer);

            var reader = new QRCodeReader();

            try
            {
                var result = reader.decode(binaryBitmap);
                var text = result.Text;
                return text;
            }
            catch
            {
            }

            return null;
        }

        void CodeWorker()
        {
            while (true)
            {
                try
                {
                    string result = Process();
                    if (result != null)
                    {
                        if (NewCode != null)
                        {
                            NewCode(result);
                        }
                    }
                }
                catch
                {
                    return;
                }
            }
        }
    }
}
