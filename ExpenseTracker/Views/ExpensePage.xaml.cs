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
        public String SelectedCategory { get; set; }
   
    public ExpensePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        SelectedCategory = Category.Miscellaneous.ToString();
        var expense = (Expense)BindingContext;
        Date.Date = DateTime.UtcNow;
        if (expense != null)
        {
            ExpenseName.Text = expense.Name;
            Amount.Text = expense.Amount.ToString();
            Date.Date = expense.Date;
            SetSelectedRadioCategory(expense.Category);

        }
    }

    private void SetSelectedRadioCategory(Category category)
    {
        switch (category.ToString())
        {
            case "Food":
                rbFood.IsChecked = true;
                break;
            case "Housing":
                rbHousing.IsChecked = true;
                break;
            case "Travel":
                rbTravel.IsChecked = true;
                break;
            case "Colthing":
                rbColthing.IsChecked = true;
                break;
            case "PetCare":
                rbPetCare.IsChecked = true;
                break;
            case "Miscellaneous":
                rbMiscellaneous.IsChecked = true;
                break;
        }
    }

    private void Amount_TextChanged(object sender, TextChangedEventArgs e)
    {
        var textbox = (Entry)sender;
        if (textbox != null)
        {
            var tempValue = decimal.Parse(textbox.Text, CultureInfo.InvariantCulture);
            var newFormat = tempValue.ToString("N2", CultureInfo.InvariantCulture);
            textbox.Text = newFormat;
            textbox.CursorPosition = newFormat.Length - 3;
        }
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (verifyRequiredFeilds())
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

    }

    private bool verifyRequiredFeilds()
    {
        List<string> required = new List<string>();
        if (string.IsNullOrEmpty(ExpenseName.Text))
            required.Add("Expense Name");

        if (Amount.Text == null)
            required.Add("Amount");

        if (required.Count > 0)
        {
            string missingFeilds = string.Join(", ", required);
            ErrorMessage.Text = $"Enter {missingFeilds}";
            return false;
        }

        return true;
    }

    private void CancelButton_Clicked(object sender, EventArgs e)
    {
        var expense = (Expense)BindingContext;
        if (expense != null && File.Exists(expense.FileName))
        {
            File.Delete(expense.FileName);
        }
        ExpenseName.Text = string.Empty;
        Amount.Text = null;
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