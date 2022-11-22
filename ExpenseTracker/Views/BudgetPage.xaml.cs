using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetPage : ContentPage
    {
        public BudgetPage()
        {
            InitializeComponent();
        }
        //following code is to display budget goal amount in the budgetpage
        protected override void OnAppearing()
        {
            var fileName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "budget.txt");
            if (!File.Exists(fileName))
            {
                return;
            }

            BudgetGoal.Text= File.ReadAllText(fileName);
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var budget = (Budget)BindingContext;
            if (budget == null || string.IsNullOrEmpty(budget.FileName))
            {
                budget = new Budget();
                budget.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                   "budget.txt");
            }
            File.WriteAllText(budget.FileName, BudgetGoal.Text);

            await Navigation.PopModalAsync();


        }

        private void OnCancleButtonClicked(object sender, EventArgs e)
        {
            
            Navigation.PopModalAsync();
        }
    }
}