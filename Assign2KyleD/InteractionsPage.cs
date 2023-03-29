using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

/**
 * Kyle Doerksen Assignment2
 * Interactions page where you can view and create interactions for a user
 */
namespace Assign2KyleD
{
    public class InteractionsPage : ContentPage
    {
        public static int custID;
        public static int prodID;
        public InteractionsPage(int customerID)
        {
            assignDB db = App.Database;
            Title = "Interactions";

            custID = customerID;

            Interactions i = db.GetInteractionByCustID(custID);
            if (i != null)
            {
                prodID = i.productID;
            }
            
            //List view for showoing interaction
            ListView list = new ListView
            {
                ItemsSource = db.GetInteractionsByCustID(custID),
                ItemTemplate = new DataTemplate(typeof(InteractionCell)),
                RowHeight = InteractionCell.RowHeight,
                HeightRequest = 1000,
            };



            StackLayout stack = new StackLayout();

            stack.Children.Add(list);

            // Table for adding interaction
            Label add = new Label { Text = "Add Interaction"};
            TableView table = new TableView { Intent = TableIntent.Form };
            TableSection section = new TableSection();
            TableRoot root = new TableRoot { section };
            DatePicker dp = new DatePicker { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Format = "dddd, MMMM dd, yyyy" };
            Label d = new Label { Text = "Date:", HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.CenterAndExpand };
            StackLayout sDate = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { d, dp } };
            ViewCell vDate = new ViewCell
            {
                View = sDate,
            };
            EntryCell eCom = new EntryCell { Label = "Comment: ", Text = "" };

            Picker pick = new Picker { Title = "Select a product", ItemsSource = db.GetProducts(), ItemDisplayBinding = new Binding("name") };
            Console.WriteLine(db.GetProducts());
            ViewCell picker = new ViewCell { View = pick };

            SwitchCell sSwitch = new SwitchCell { Text = "Purchased? "};
            section.Add(vDate);
            section.Add(eCom);
            section.Add(picker);
            section.Add(sSwitch);
            table.Root = root;

            stack.Children.Add(add);
            stack.Children.Add(table);

            Button btnAdd = new Button { Text = "Add", HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
            Button btnNew = new Button { Text = "New", HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };

            Label err = new Label { Text = "", HorizontalOptions = LayoutOptions.Center};
            stack.Children.Add(err);

            Interactions current = null;

            list.ItemTapped += (s, e) =>
            {
                //list.SelectedItem = null;
                Interactions interaction = e.Item as Interactions;
                current = interaction;
                dp.Date = interaction.date;
                eCom.Text = interaction.comments;
                Products p = db.GetProduct(interaction.productID);
                List<Products> prods = db.GetProducts();
                for(int x = 0; x < prods.Count; x++)
                {
                    if(p.name.Equals(prods[x].name))
                    {
                        pick.SelectedIndex = x;
                    }
                }
                sSwitch.On = interaction.purchased;
            };

            btnAdd.Clicked += (s, e) =>
            {
                if(current == null) // FOR NEW ITEMS
                {
                    Products p = (Products)pick.SelectedItem;
                    Interactions ii = null;
                    if (p != null)
                    {
                        prodID = p.ID;
                        ii = new Interactions
                        {
                            comments = eCom.Text,
                            date = dp.Date,
                            customerID = custID,
                            productID = p.ID,
                            purchased = sSwitch.On
                        };

                    }
                    // validation
                    if (ii != null && ii.comments.Length > 0 && ii.customerID > 0)
                    {
                        db.SaveInteraction(ii);
                        eCom.Text = "";
                        dp.Date = DateTime.Now;
                        pick.SelectedItem = null;
                        sSwitch.On = false;
                        list.ItemsSource = db.GetInteractionsByCustID(custID);
                        err.Text = "";
                    }
                    else
                    {
                        err.Text = "All fields are required";
                        err.TextColor = Color.Red;
                    }
                }
                else // FOR UPDATING ITEMS
                {
                    Products p = (Products)pick.SelectedItem;
                    Interactions ii = null;
                    if (p != null)
                    {
                        prodID = p.ID;
                        ii = new Interactions
                        {
                            ID = current.ID,
                            comments = eCom.Text,
                            date = dp.Date,
                            customerID = custID,
                            productID = p.ID,
                            purchased = sSwitch.On
                        };

                    }

                    if (ii != null && ii.comments.Length > 0 && ii.customerID > 0)
                    {
                        db.SaveInteraction(ii);
                        eCom.Text = "";
                        dp.Date = DateTime.Now;
                        pick.SelectedItem = null;
                        sSwitch.On = false;
                        list.ItemsSource = db.GetInteractionsByCustID(custID);
                        err.Text = "";
                        current = null;
                    }
                    else
                    {
                        err.Text = "All fields are required";
                        err.TextColor = Color.Red;
                    }
                }


            };

            btnNew.Clicked += (s, e) =>
            {
                current = null;
                eCom.Text = "";
                dp.Date = DateTime.Now;
                pick.SelectedItem = null;
                sSwitch.On = false;
                err.Text = "";
            };

            StackLayout btns = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children=
                {
                    btnAdd,
                    btnNew
                }
            };

            stack.Children.Add(btns);

            Content = stack;
        }
    }

    public class InteractionCell : ViewCell
    {
        public const int RowHeight = 80;
        public InteractionCell()
        {
            assignDB db = App.Database;
            StackLayout outerStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
            };
            StackLayout innerStack1 = new StackLayout   // stack for the name
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
            };
            StackLayout innerStack2 = new StackLayout // stack for the date and comments
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
            };
            StackLayout innerStack3 = new StackLayout // stack for the product and purchased
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
            };

            // Getting the name
            Customer c = db.GetCustomer(InteractionsPage.custID);
            Label lName = new Label { Text = c.lName, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
            Label fName = new Label { Text = c.fName, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
            innerStack1.Children.Add(fName);
            innerStack1.Children.Add(lName);

            // getting the date and comment
            Label date = new Label { FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
            Label comment = new Label { FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
            date.SetBinding(Label.TextProperty, new Binding(path: "date", stringFormat: "{0:dddd, MMMM dd, yyyy}"));
            comment.SetBinding(Label.TextProperty, "comments");
            innerStack2.Children.Add(date);
            innerStack2.Children.Add(comment);

            Label Purchased = new Label { Text = "Purchased? ", FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
            Label product = new Label { FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
            Switch s = new Switch { HorizontalOptions = LayoutOptions.StartAndExpand };
            s.SetBinding(Switch.IsToggledProperty, "purchased");

            Products p = db.GetProduct(InteractionsPage.prodID);
            product.Text = p.name;
            
            innerStack3.Children.Add(product);
            innerStack3.Children.Add(Purchased);
            innerStack3.Children.Add(s);

            outerStack.Children.Add(innerStack1);
            outerStack.Children.Add(innerStack2);
            outerStack.Children.Add(innerStack3);

            // for Swip delete
            SwipeItem delete = new SwipeItem
            {
                Text = "Delete",
                BackgroundColor = Color.Red,
            };

            delete.Invoked += (sender, args) =>
            {
                ListView parent = (ListView)this.Parent;
                db.DeleteInteraction(this.BindingContext as Interactions);
                parent.ItemsSource = db.GetInteractionsByCustID(InteractionsPage.custID);
            };

            List<SwipeItem> l = new List<SwipeItem>() { delete};

            SwipeView sv = new SwipeView
            {
                RightItems = new SwipeItems(l),
                Content = outerStack
            };

            View = sv;
        }
        
    }

}