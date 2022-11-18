using ExpenseTracker.Models;
using ExpenseTracker.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public string myBudget { get; set; }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    
        protected override void OnAppearing()
        {
            var fileName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "budget.txt");
            if (!File.Exists(fileName))
            {
                return;
            }

            myBudget = File.ReadAllText(fileName);
            // Notify UI,the property has been changed.
            this.OnPropertyChanged(nameof(myBudget));



        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new BudgetPage());

        }
    }
}
