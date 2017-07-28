using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.App_Code
{
    //public class Persons
    //{
    //    public List<Person> ListOfPerson { get; set; }

    //    public Persons()
    //    {
    //        this.ListOfPerson = new List<Person>();
    //    }
    //}
    public class Person
    {
        public int Id { get; set; }
        public string Picture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}