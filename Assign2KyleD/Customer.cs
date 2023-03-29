using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

/**
 * Kyle Doerksen Assignment2
 * Customer model
 */
namespace Assign2KyleD
{
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email{ get; set; }

    }
}
