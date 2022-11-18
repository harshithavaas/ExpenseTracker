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
        //following code is to display budget goal in the mainpage
        protected override void OnAppearing()
        {
            var budget = (Budget)BindingContext;
            if (budget != null && !string.IsNullOrEmpty(budget.FileName))
            {
                budget.Amount = Int32.Parse(File.ReadAllText(budget.FileName));
            }
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

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var budget = (Budget)BindingContext;
            if (File.Exists(budget.FileName))
            {
                File.Delete(budget.FileName);
            }
            BudgetGoal.Text = string.Empty;
            Navigation.PopModalAsync();
        }
    }
}