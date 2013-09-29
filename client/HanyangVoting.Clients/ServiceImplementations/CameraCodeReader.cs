using HanyangVoting.Clients.ServiceInterfaces;
using HanyangVoting.CodeReader;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Clients.ServiceImplementations
{
    class CameraCodeReader : ICodeReader
    {
        private readonly CodeReaderEngine _engine;

        public CameraCodeReader()
        {
            _engine = new CodeReaderEngine();
            _engine.NewFrame += _engine_NewFrame;
            _engine.NewCode += _engine_NewCode;
        }

        void _engine_NewCode(string obj)
        {
            if (NewCode != null)
            {
                NewCode(obj);
            }
        }

        void _engine_NewFrame(Bitmap obj)
        {
            if (NewFrame != null)
            {
                NewFrame(obj);
            }
        }

        public event Action<Bitmap> NewFrame;
        public event Action<string> NewCode;

        public void Start()
        {
            _engine.Start();
        }

        public void Stop()
        {
            _engine.Stop();
        }
    }
}
