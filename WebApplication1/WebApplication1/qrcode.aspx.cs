using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class qrcode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void processBarcode(string filename)
        {
            QRCodeEncoder encoder = new QRCodeEncoder();
            encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;

            var result = qrText.Text.Trim().Replace("\\r\\n", System.Environment.NewLine);

            Bitmap img = encoder.Encode(result);

            img.Save(Server.MapPath(filename), ImageFormat.Jpeg);

            qrImage.ImageUrl = filename;

        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            var filename = "~/images/qrCodes/" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".jpg";

            processBarcode(filename);

            QRCodeDecoder dec = new QRCodeDecoder();
            var image = System.Drawing.Image.FromFile(Server.MapPath(filename));
            qrDecode.Text = (dec.Decode(new QRCodeBitmapImage(image as Bitmap)));
        }
    }
}