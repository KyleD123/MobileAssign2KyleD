using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

/**
 * Kyle Doerksen Assignment2
 * Interactions model
 */
namespace Assign2KyleD
{
    public class Interactions
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int customerID { get; set; }
        public DateTime date { get; set; }
        public string comments { get; set; }
        public int productID { get; set; }
        public bool purchased { get; set; }

    }
}
