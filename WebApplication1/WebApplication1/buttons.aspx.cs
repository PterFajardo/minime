using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class buttons : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            initiateCSS();
            //getData();
            createImageButtons();
            
        }

        private void initiateCSS()
        {
            // Define an HtmlLink control.
            HtmlLink myHtmlLink = new HtmlLink();
            myHtmlLink.Href = "~/StyleSheet1.css";
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");

            // Add the HtmlLink to the Head section of the page.
            Page.Header.Controls.Add(myHtmlLink);

        }


        private void createImageButtons()
        {
            var divMyButtons = FindControl("myButtons");

            if (divMyButtons != null)
            {
                var imageLocation = "~/images/imageButtons/";
                string[] fileEntries = Directory.GetFiles(Server.MapPath(imageLocation));
                foreach (string fileName in fileEntries)
                {
                    FileInfo info = new FileInfo(fileName);
                    var name = info.Name;
                    var imageButton = new ImageButton();
                    imageButton.ID = "btn" + Common.UppercaseFirst(name.Substring(0, name.IndexOf('.')));
                    imageButton.ImageUrl = imageLocation + name;
                    imageButton.Click += new ImageClickEventHandler(this.imgButton_Click);
                    imageButton.AlternateText = name;
                    imageButton.ToolTip = name;
                    divMyButtons.Controls.Add(imageButton);
                }
            }
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            var imgButton = (ImageButton)sender;
            if (string.IsNullOrEmpty(imgButton.CssClass))
            {
                imgButton.CssClass = "inactive";
            }
            else
            {
                imgButton.CssClass = string.Empty;
            }


        }
    }
}