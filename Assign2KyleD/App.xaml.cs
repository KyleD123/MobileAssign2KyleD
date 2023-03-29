using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/**
 * Kyle Doerksen Assignment2
 */
namespace Assign2KyleD
{
    public partial class App : Application
    {
        static assignDB database;
        public static assignDB Database
        {
            get
            {
                if(database == null)
                {
                    database = new assignDB(DependencyService.Get<IFileHelper>().GetLocalFilePath("assignment.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            database = Database;

            MainPage = new NavigationPage(new MainPage());
            ToolbarItem product = new ToolbarItem { Text = "Products"};
            ToolbarItem settings = new ToolbarItem { Text = "Settings" };

            product.Clicked += (s, e) =>    // toolbar item for product page
            {
                MainPage.Navigation.PushAsync(new ProductsPage());
            };

            settings.Clicked += (s, e) =>   // toolbar item for settings page
            {
                MainPage.Navigation.PushAsync(new SettingsPage());
            };


            MainPage.ToolbarItems.Add(product);
            MainPage.ToolbarItems.Add(settings);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
