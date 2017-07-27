using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using MessagingToolkit.QRCode.Codec;
using System.Drawing.Imaging;

namespace WebApplication1
{
    public partial class imageupload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                try
                {
                    if (FileUploadControl.PostedFile.ContentType == "image/jpeg")
                    {
                        if (FileUploadControl.PostedFile.ContentLength < 102400)
                        {
                            string filename = Path.GetFileName(FileUploadControl.FileName);

                            byte[] imageByte = FileUploadControl.FileBytes;
                            FileUploadControl.SaveAs(Server.MapPath("~/images/uploads/") + filename);
                            StatusLabel.Text = "Upload status: File uploaded!";

                            uploadedImage.ImageUrl = "~/images/uploads/" + filename;
                            dbImage.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imageByte, 0, imageByte.Length);
                        }
                        else
                            StatusLabel.Text = "Upload status: The file has to be less than 100 kb!";
                    }
                    else
                        StatusLabel.Text = "Upload status: Only JPEG files are accepted!";
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }

        // convert image to byte array
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        //Byte array to photo
        public System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        
      
    }
}