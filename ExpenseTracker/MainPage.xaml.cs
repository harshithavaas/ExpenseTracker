using ExpenseTracker.Models;
using ExpenseTracker.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpenseTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            decimal totalexpense = 0;
            var expenses = new List<Expense>();
            var files = Directory.EnumerateFiles(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "*.notes.txt");
            foreach (var file in files)
            {
                var expenseFile = File.ReadAllText(file).Split(';').ToList();
                var category = expenseFile[3];
                var expense = new Expense
                {
                    Name = expenseFile[0],
                    Amount = Convert.ToDecimal(expenseFile[1]),
                    Date = Convert.ToDateTime(expenseFile[2]),
                    Category = SetCategory(expenseFile[3]),
                    FileName = file
                };
                expenses.Add(expense);
                totalexpense = totalexpense + expense.Amount;
            }
            ExpenseListView.ItemsSource = expenses.OrderByDescending(t => t.Date);

            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "budget.txt")))
            {
                var budgetfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "budget.txt");

                var budget = new Budget
                {
                    budgetAmount = Convert.ToDecimal(File.ReadAllText(budgetfile)),
                    Date = File.GetCreationTime(budgetfile),
                    FileName = budgetfile
                };
                TotalExpenses.Text = "$" + totalexpense.ToString();
                Budget.Text = "$" + budget.budgetAmount.ToString();
                BudgetToggle.Text = "Edit Budget";

                if (totalexpense > budget.budgetAmount)
                {
                    TotalExpenseBox.BackgroundColor = Color.LightPink;
                }
                else
                {
                    TotalExpenseBox.BackgroundColor = Color.YellowGreen;
                }
            }
            else
            {
                BudgetToggle.Text = "Add Budget";
            }
        }
        private Category SetCategory(string category)
        {
            var setCategory = new Category();
            switch (category)
            {
                case "Food":
                    setCategory = Category.Food;
                    break;
                case "Housing":
                    setCategory = Category.Housing;
                    break;
                case "Travel":
                    setCategory = Category.Travel;
                    break;
                case "Clothing":
                    setCategory = Category.Clothing;
                    break;
                case "PetCare":
                    setCategory = Category.PetCare;
                    break;
                case "Miscellaneous":
                    setCategory = Category.Miscellaneous;
                    break;
            }
            return setCategory;
        }

        private async void ExpenseListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new ExpensePage
            {
                BindingContext = (Expense)e.SelectedItem
            });
        }

        private async void ExpenseButton_Clicked(object sender, EventArgs e)
        {
            var ExpensePage = new ExpensePage();
            await Navigation.PushModalAsync(ExpensePage);
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            var BudgetPage = new BudgetPage();
            await Navigation.PushModalAsync(BudgetPage);
        }
    }
}
