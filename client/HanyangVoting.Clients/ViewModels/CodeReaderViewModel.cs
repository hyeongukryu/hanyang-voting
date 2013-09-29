using HanyangVoting.Clients.ServiceInterfaces;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using HanyangVoting.CodeReader;

namespace HanyangVoting.Clients.ViewModels
{
    class CodeReaderViewModel : NotificationObject
    {
        private readonly ICodeReader _codeReader;
        private readonly IContext _context;

        public string Code { get; set; }
        public ImageSource Image { get; set; }

        public CodeReaderViewModel(ICodeReader codeReader, IContext context)
        {
            _codeReader = codeReader;
            _context = context;

            _codeReader.NewFrame += _codeReader_NewFrame;
            _codeReader.NewCode += _codeReader_NewCode;

            _codeReader.Start();
        }

        void _codeReader_NewCode(string obj)
        {
            Code = obj;
            RaisePropertyChanged(() => Code);
        }

        void _codeReader_NewFrame(System.Drawing.Bitmap obj)
        {
            _context.Invoke(() =>
                {
                    Image = obj.ToBitmapImage();
                });
            RaisePropertyChanged(() => Image);
        }
    }
}
