using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace HanyangVoting.CodeReader
{
    class CodeWriter
    {
        public static System.Drawing.Bitmap GetQRCode(HanyangVoting.Models.Ticket ticket)
        {
            return GetQRCode(ticket.Key);
        }

        public static System.Drawing.Bitmap GetQRCode(string content)
        {
            ErrorCorrectionLevel errorCorrectionLevel = ErrorCorrectionLevel.L;

            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options.Width = 1600;
            writer.Options.Height = 1600;
            writer.Options.Hints.Add(EncodeHintType.ERROR_CORRECTION, errorCorrectionLevel);

            var bitmap = writer.Write(content);
            return bitmap;
        }
    }
}
