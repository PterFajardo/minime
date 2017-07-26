using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//https://msdn.microsoft.com/en-us/library/bb398790.aspx#Code Examples

namespace WebApplication1
{
    public partial class gridsample : System.Web.UI.Page
    {
        List<Person> PERSONS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                createData();
                bindData();
            }
        }

        private void bindData()
        {
            lstData.DataSource = PERSONS;
            lstData.DataBind();

            grpData.DataSource = PERSONS;
            grpData.DataBind();
        }

        private void createData()
        {
            PERSONS = new List<Person>();

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
        }

        protected void CurrentRowTextBox_OnTextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            DataPager pager = (DataPager)lstData.FindControl("lstDataPager");
            pager.SetPageProperties(Convert.ToInt32(t.Text) - 1, pager.PageSize, true);
        }

    }

    public class Person
    {
        public int Id { get; set; }
        public string Picture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}