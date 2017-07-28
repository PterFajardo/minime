using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.App_Code;

//https://msdn.microsoft.com/en-us/library/bb398790.aspx#Code Examples

namespace WebApplication1
{
    public partial class gridsample : System.Web.UI.Page
    {
        List<Person> PERSONS;

        protected void Page_Load(object sender, EventArgs e)
        {
            PERSONS = new List<Person>();  

            if (!Page.IsPostBack)
            {
                Session["persons"] = createData();
                bindData();
            }

            if (IsPostBack && imageUploader.PostedFile != null)
            {
                if (imageUploader.PostedFile.FileName.Length > 0)
                {
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + imageUploader.FileName.Substring(imageUploader.FileName.IndexOf('.'));
                    imageUploader.SaveAs(Server.MapPath("~/images/uploads/") + filename);
                    personImage.ImageUrl = "~/images/uploads/" + filename;
                }
            }
        }

        private void bindData()
        {
            lstData.DataSource = PERSONS;
            lstData.DataBind();

            grpData.DataSource = PERSONS;
            grpData.DataBind();
        }

        private List<Person> createData()
        {
            var p = new Person
            {
                Id = 1,
                Picture = "~/images/persons/blue.png",
                FirstName = "peter",
                LastName = "fajardo"
            };

            PERSONS.Add(p);

            p = new Person
            {
                Id = 2,
                Picture = "~/images/persons/gray.png",
                FirstName = "vilma",
                LastName = "fajardo"
            };

            PERSONS.Add(p);

            p = new Person
            {
                Id = 3,
                Picture = "~/images/persons/green.png",
                FirstName = "niko",
                LastName = "fajardo"
            };

            PERSONS.Add(p);

            p = new Person
            {
                Id = 4,
                Picture = "~/images/persons/orange.png",
                FirstName = "niki",
                LastName = "fajardo"
            };

            PERSONS.Add(p);

            p = new Person
            {
                Id = 5,
                Picture = "~/images/persons/red.png",
                FirstName = "nika",
                LastName = "fajardo"
            };

            PERSONS.Add(p);

            return PERSONS;
        }

        protected void CurrentRowTextBox_OnTextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            DataPager pager = (DataPager)lstData.FindControl("lstDataPager");
            pager.SetPageProperties(Convert.ToInt32(t.Text) - 1, pager.PageSize, true);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            PERSONS = (List<Person>)Session["persons"];
            Button btn = (Button)sender;
            if (!btn.Text.ToLower().Equals("edit"))
            {
                var p = new Person
                {
                    Id = 0,
                    Picture = personImage.ImageUrl,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim()
                };

                PERSONS.Add(p);
            }
            else 
            {
                var person = (Person)Session["EditedRecord"];
                person.Picture = personImage.ImageUrl;
                person.FirstName = txtFirstName.Text.Trim();
                person.LastName = txtLastName.Text.Trim();

                btnCreate.Text = "Create";
                Session.Remove("EditedRecord");
            }

            resetInputForm();
            bindData();
        }

        private void resetInputForm()
        {
            personImage.ImageUrl = "~/images/persons/blue-noimage.png";
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void lstData_DeleteItem(int id)
        {
            var x = string.Empty;
        }

        protected void lstData_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ListViewItem item = lstData.Items[e.NewEditIndex];
            Image image = (Image)item.FindControl("picture");
            Label firstLabel = (Label)item.FindControl("FirstNameLabel");
            Label LastLabel = (Label)item.FindControl("LastNameLabel");
            txtFirstName.Text = firstLabel.Text;
            txtLastName.Text = LastLabel.Text;
            personImage.ImageUrl = image.ImageUrl;
            btnCreate.Text = "Edit";

            getCurrentRecord(Convert.ToInt32(lstData.DataKeys[e.NewEditIndex].Value));
        }

        protected void lstData_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            

            var id  = e.Keys[0];
            var person = PERSONS.FirstOrDefault(x => x.Id.Equals(id));

            if (person != null)
            {
                //var list = lstData.Items[e.N
                var txtFirst = (TextBox)FindControl("txtEFirstName");
                var txtLast = (TextBox)FindControl("txtELastName");
                person.FirstName = txtFirst.Text.Trim();
                person.LastName = txtLast.Text.Trim();

                bindData();

                Session["persons"] = PERSONS;
            }
        }

        private void getCurrentRecord(int id)
        {
            PERSONS = (List<Person>)Session["persons"];
            Session["EditedRecord"] = PERSONS.FirstOrDefault(x => x.Id.Equals(id));
        }

        protected void lstData_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            PERSONS = (List<Person>)Session["persons"];
            var p = PERSONS.FirstOrDefault(x => x.Id.Equals(Convert.ToInt32(e.Keys[0])));
            
            if (((List<Person>)Session["persons"]).Remove(p))
            {
                bindData();
            }
        }
    }

}