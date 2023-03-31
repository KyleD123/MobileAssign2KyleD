using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

/**
 * Kyle Doerksen Assignment2
 * Main page is where the customers are displayed with a button
 * to add a new customer
 */
namespace Assign2KyleD
{
    public partial class MainPage : ContentPage
    {
        public static ListView lv;
        public MainPage()
        {
            InitializeComponent();
            Title = "Customers";

            assignDB database = App.Database;

            lv = new ListView
            {
                ItemsSource = database.GetCustomers(),
                ItemTemplate = new DataTemplate(typeof(CustomerCell)),
                RowHeight = CustomerCell.RowHeight,
                HeightRequest = 500,
            };

            lv.ItemTapped += (s, e) =>
            {
                lv.SelectedItem = null;
                //push to interactions page
                Customer c = e.Item as Customer;
                Navigation.PushAsync(new InteractionsPage(c.ID));
            };

            Button addNew = new Button
            {
                Text = "Add New Customer",
            };

            addNew.Clicked += (s, e) =>
            {
                Navigation.PushAsync(new AddCustomer());
            };

            StackLayout sLayout = new StackLayout
            {
                HorizontalOptions= LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children =
                {
                    lv, addNew
                }
            };

            Content = sLayout;
        }
    }

    public class CustomerCell : ViewCell
    {
        public const int RowHeight = 55;
        public CustomerCell()
        {
            assignDB db = App.Database;
            StackLayout stack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 2,
                Padding = 2
            };

            Label lName = new Label { FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
            Label fName = new Label { FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
            Label phone = new Label { FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };

            lName.SetBinding(Label.TextProperty, "lName");
            fName.SetBinding(Label.TextProperty, "fName");
            phone.SetBinding(Label.TextProperty, "phone");

            stack.Children.Add(fName);
            stack.Children.Add(lName);
            stack.Children.Add(phone);

            View = stack;

            MenuItem mi = new MenuItem { Text = "Delete", IsDestructive = true };
            mi.Clicked += (s, e) => // for deleting customer and associated interactions
            {
                ListView parent = (ListView)this.Parent;
                Customer c = this.BindingContext as Customer;
                db.DeleteCustomer(c);
                List<Interactions> il = db.GetInteractionsByCustID(c.ID);
                foreach (Interactions interaction in il)
                {
                    db.DeleteInteraction(interaction);
                }
                parent.ItemsSource = db.GetCustomers();
            };
            ContextActions.Add(mi);
        }
    }

}
