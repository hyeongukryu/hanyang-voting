using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Clients.ServiceInterfaces
{
    interface ICodeReader
    {
        event Action<Bitmap> NewFrame;
        event Action<string> NewCode;

        void Start();
        void Stop();
    }
}
