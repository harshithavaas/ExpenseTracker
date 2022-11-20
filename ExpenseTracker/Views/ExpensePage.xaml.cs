using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExpenseTracker.Models;
using System.IO;

namespace ExpenseTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensePage : ContentPage
    {
        public String SelectedCategory
        {
            get; // get method
            set;
        }
        public ExpensePage()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            SelectedCategory = Category.Miscellaneous.ToString();
            var expense = (Expense)BindingContext;
            if (expense != null && !string.IsNullOrEmpty(expense.FileName))
            {
                string TotalFileText = File.ReadAllText(expense.FileName);
                // assign value in each feild
            }
        }

        private void Amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (Entry)sender;
            var tempValue = decimal.Parse(textbox.Text, CultureInfo.InvariantCulture);
            var newFormat = tempValue.ToString("N2", CultureInfo.InvariantCulture);
            textbox.Text = newFormat;
            textbox.CursorPosition = newFormat.Length - 3;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            var expense = (Expense)BindingContext;
            if (expense == null || string.IsNullOrEmpty(expense.FileName))
            {
                expense = new Expense();
                expense.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.notes.txt");
            }

            string TotalFileText = $"{ExpenseName.Text};{Amount.Text};{Date.Date};{SelectedCategory}";

            File.WriteAllText(expense.FileName, TotalFileText);

            Navigation.PopModalAsync();

        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            var expense = (Expense)BindingContext;
            if (File.Exists(expense.FileName))
            {
                File.Delete(expense.FileName);
            }
            ExpenseName.Text = string.Empty;
            Amount.Text = String.Empty;
            Date.Date = DateTime.Now;
            rbMiscellaneous.IsChecked = true; 


            Navigation.PopModalAsync();
        }

        private void ExpenseCategory_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            SelectedCategory = button.Value.ToString();
        }
    }
}