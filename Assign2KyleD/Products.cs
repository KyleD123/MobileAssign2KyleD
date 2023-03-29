using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

/**
 * Kyle Doerksen Assignment2
 * Products model
 */
namespace Assign2KyleD
{
    public class Products
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
    }
}
