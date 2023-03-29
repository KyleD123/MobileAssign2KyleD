using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

/**
 * Kyle Doerksen Assignment2
 * Add Customer page which has a form to create new customers
 */
namespace Assign2KyleD
{
    public class AddCustomer : ContentPage
    {
        public AddCustomer()
        {
            Title = "New Customer";
            assignDB db = App.Database;

            StackLayout sLayout = new StackLayout();
            Label lbl = new Label { Text = "Add New Customer" };
            sLayout.Children.Add(lbl);

            TableView tv = new TableView { Intent = TableIntent.Form};
            TableSection section = new TableSection();
            TableRoot root = new TableRoot { section };
            EntryCell fName = new EntryCell { Label = "First Name: ", Text = "" };
            EntryCell lName = new EntryCell { Label = "Last Name: ", Text = "" };
            EntryCell addr = new EntryCell { Label = "Address: ", Text = "" };
            EntryCell phone = new EntryCell { Label = "Phone: ", Text = "" };
            EntryCell email = new EntryCell { Label = "Email: ", Text = "" };
            section.Add(fName);
            section.Add(lName);
            section.Add(addr);
            section.Add(phone);
            section.Add(email);
            tv.Root = root;

            sLayout.Children.Add(tv);

            Label err = new Label { Text = "" };
            sLayout.Children.Add(err);

            Button btnSave = new Button { Text = "Save"};
            sLayout.Children.Add(btnSave);

            btnSave.Clicked += (s, e) =>
            {
                Customer c = new Customer
                {
                    fName = fName.Text.Length > 0 ? fName.Text : null,
                    lName = lName.Text.Length > 0 ? lName.Text : null,
                    address = addr.Text.Length > 0 ? addr.Text : null,
                    phone = phone.Text.Length > 0 ? phone.Text : null,
                    email = email.Text.Length > 0 ? email.Text : null,
                };
                if(c.fName != null && c.lName != null && c.address != null && c.phone != null && c.email != null)
                {
                    db.SaveCustomer(c);
                    err.Text = "";
                    fName.Text = "";
                    lName.Text = "";
                    addr.Text = "";
                    phone.Text = "";
                    email.Text = "";
                    MainPage.lv.ItemsSource = db.GetCustomers();
                    Navigation.PopAsync();
                }
                else
                {
                    err.Text = "All Fields Are Required"; // Validation
                    err.TextColor = Color.Red;
                }
            };


            Content = sLayout;
        }
    }
}