using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * Kyle Doerksen Assignment2
 * Database functions
 */
namespace Assign2KyleD
{
    public class assignDB
    {
        readonly SQLiteConnection database;

        public assignDB(string dbPath)
        {
            database = new SQLiteConnection(dbPath);

            database.CreateTable<Customer>();
            database.CreateTable<Interactions>();
            database.CreateTable<Products>();

            if (database.Table<Products>().Count() == 0)
            {
                Products p = new Products
                {
                    name = "Wonder Jacket",
                    description = "A wonderful jacket",
                    price = 499.99,
                };
                Products p2 = new Products
                {
                    name = "Wonder Hat",
                    description = "A wonderful hat",
                    price = 124.99,
                };
                Products p3 = new Products
                {
                    name = "Wonder Boots",
                    description = "A wonderful pair of high quality boots",
                    price = 224.99,
                };
                SaveProduct(p);
                SaveProduct(p2);
                SaveProduct(p3);
            }
        }

        //This function will drop the customer and interactions table then
        // recreate them
        public void reset()
        {
            //Remove all the tables
            database.DropTable<Customer>();
            database.DropTable<Interactions>();
            database.DropTable<Products>();
            //Recreate all the tables
            database.CreateTable<Customer>();
            database.CreateTable<Interactions>();
            database.CreateTable<Products>();

            Products p = new Products
            {
                name = "Wonder Jacket",
                description = "A wonderful jacket",
                price = 499.99,
            };
            Products p2 = new Products
            {
                name = "Wonder Hat",
                description = "A wonderful hat",
                price = 124.99,
            };
            Products p3 = new Products
            {
                name = "Wonder Boots",
                description = "A wonderful pair of high quality boots",
                price = 224.99,
            };
            SaveProduct(p);
            SaveProduct(p2);
            SaveProduct(p3);
        }

        // this function will get all customers
        public List<Customer> GetCustomers()
        {
            return database.Table<Customer>().ToList();
        }

        // this function will get all interactions
        public List<Interactions> GetInteractions()
        {
            return database.Table<Interactions>().ToList();
        }

        // this function will get all interactions
        public List<Interactions> GetInteractionsByCustID(int ID)
        {
            return database.Table<Interactions>().Where(i => i.customerID == ID).ToList();
        }

        // this function will get all products
        public List<Products> GetProducts()
        {
            return database.Table<Products>().ToList();
        }

        // this function will get 1 customer based on the id
        public Customer GetCustomer(int id)
        {
            return database.Table<Customer>().Where(i => i.ID == id).FirstOrDefault();
        }
        // this function will get 1 interaction based on the id
        public Interactions GetInteraction(int id)
        {
            return database.Table<Interactions>().Where(i => i.ID == id).FirstOrDefault();
        }

        // This will get a single interaction by the customer ID
        public Interactions GetInteractionByCustID(int ID)
        {
            return database.Table<Interactions>().Where(i => i.customerID == ID).FirstOrDefault();
        }

        // This function will get a count of interactions based on the product ID
        public int GetInteractionCount(int prodID)
        {
            return database.Table<Interactions>().Where(i => i.productID == prodID).Count();
        }

        // this function will get 1 product based on the id
        public Products GetProduct(int id)
        {
            return database.Table<Products>().Where(i => i.ID == id).FirstOrDefault();
        }

        // this function will save a customer
        public int SaveCustomer(Customer c)
        {
            if(c.ID != 0)
            {
                return database.Update(c);
            }
            else
            {
                return database.Insert(c);
            }
        }
        // this function will save an interactoin
        public int SaveInteraction(Interactions i)
        {
            if (i.ID != 0)
            {
                return database.Update(i);
            }
            else
            {
                return database.Insert(i);
            }
        }

        // This function will save a product into the database
        public int SaveProduct(Products p)
        {
            if (p.ID != 0)
            {
                return database.Update(p);
            }
            else
            {
                return database.Insert(p);
            }
        }

        // this function will delete a customer
        public int DeleteCustomer(Customer c)
        {
            return database.Delete(c);
        }

        // this function will delete a product
        public int DeleteProduct(Products p)
        {
            return database.Delete(p);
        }

        // this function will delete a interaction
        public int DeleteInteraction(Interactions i)
        {
            return database.Delete(i);
        }


    }
}
