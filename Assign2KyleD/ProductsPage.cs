using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

/**
 * Kyle Doerksen Assignment2
 * Products page which shows product info as well as number of interactions
 */
namespace Assign2KyleD
{
    public class ProductsPage : ContentPage
    {
        public ProductsPage()
        {
            Title = "Products";
            assignDB db = App.Database;

            ListView list = new ListView
            {
                ItemsSource = db.GetProducts(),
                ItemTemplate = new DataTemplate(typeof(ProductCell)),
                RowHeight = ProductCell.RowHeight,
                HeightRequest = 400,
            };

            list.ItemTapped += (s, e) =>
            {
                list.SelectedItem = null;
            };


            Content = list;
        }
    }

    public class ProductCell : ViewCell
    {
        public const int RowHeight = 80;
        private Label count = new Label();
        public ProductCell()
        {
            assignDB db = App.Database;

            StackLayout outerStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = 5
            };
            StackLayout innerStack1 = new StackLayout   // stack for the
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = 0
            };
            StackLayout innerStack2 = new StackLayout   // stack for the name
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = 0
            };
            StackLayout innerStack3 = new StackLayout // stack for the product and purchased
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = 0
            };

            Label name = new Label { FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
            name.SetBinding(Label.TextProperty, "name");
            innerStack1.Children.Add(name);

            Label desc = new Label { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
            desc.SetBinding(Label.TextProperty, "description");
            Label sign = new Label { Text = "$", HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.CenterAndExpand };
            Label price = new Label { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
            price.SetBinding(Label.TextProperty, "price");
            innerStack2.Children.Add(desc);
            innerStack2.Children.Add(sign);
            innerStack2.Children.Add(price);

            Label interactions = new Label { Text = "# Interactions: ", HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };

            //int interactionsCount = db.GetInteractionCount(p.ID);
            count = new Label { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
            innerStack3.Children.Add(interactions);
            innerStack3.Children.Add(count);

            outerStack.Children.Add(innerStack1);
            outerStack.Children.Add(innerStack2);
            outerStack.Children.Add(innerStack3);

            

            View = outerStack;
        }

        // for Displaying the number of interactions a product has
        protected override void OnBindingContextChanged() 
        {
            base.OnBindingContextChanged();
            assignDB db = App.Database;
            Products p = (Products)this.BindingContext;
            int interactionsCount = db.GetInteractionCount(p.ID);
            string c = "" + interactionsCount;
            this.count.Text = c;

        }

    }
}