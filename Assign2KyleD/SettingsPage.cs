using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

/**
 * Kyle Doerksen Assignment2
 * Settings page where you can reset data
 */
namespace Assign2KyleD
{
    public class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            Title = "Settings";

            Label l = new Label { Text = "Would you like to reset the data?"};
            Button btnReset = new Button { Text= "RESET", BackgroundColor = Color.Red };

            assignDB db = App.Database;

            btnReset.Clicked += (s, e) =>
            {
                db.reset();
                MainPage.lv.ItemsSource = db.GetCustomers(); // after resetting the database gotta reset the item source for the cusomter list
                Navigation.PopToRootAsync();
            };

            Content = new StackLayout
            {
                HorizontalOptions= LayoutOptions.Center,
                VerticalOptions= LayoutOptions.Center,
                Children= {l, btnReset}
            };
        }
    }
}