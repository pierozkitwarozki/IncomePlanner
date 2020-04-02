using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace IncomePlanner
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText incomePerHourEditText;
        EditText workHoursEditText;
        EditText taxRateEditText;
        EditText savingsRateEditText;

        TextView workSummaryValue;
        TextView grossIncomeValue;
        TextView taxPayableValue;
        TextView savingsValue;
        TextView spendableIncomeValue;

        Button calculateButton;
        RelativeLayout resultLayout;

        bool inputCalculeted = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);           
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            ConnectViews();
            calculateButton.Click += CalculateButton_Click;
        }

        private void CalculateButton_Click(object sender, System.EventArgs e)
        {
            if (inputCalculeted)
            {
                inputCalculeted = false;
                ClearInput();
                calculateButton.Text = "Calculate";
                return;
            }
            //Take inputs from user
            double incomePerHour = double.Parse(incomePerHourEditText.Text);
            double workHoursPerDay = double.Parse(workHoursEditText.Text);
            double taxRate = double.Parse(taxRateEditText.Text);
            double savingsRate = double.Parse(savingsRateEditText.Text);

            //Caluclate Income, etc...
            double annualWorkHourSummary = workHoursPerDay * 5 * 50; //5 days a week, 50 weeks a year (two weeks off)
            double annualIncome = annualWorkHourSummary * incomePerHour;
            double taxPayable = (taxRate / 100) * annualIncome;
            double annualSavings = (savingsRate / 100) * annualIncome;
            double spendableIncome = annualIncome - taxPayable - annualSavings;

            //Display values and set Visible
            workSummaryValue.Text = annualWorkHourSummary.ToString("#,##") + " HRS";
            grossIncomeValue.Text = annualIncome.ToString("#,##") + " USD";
            taxPayableValue.Text = taxPayable.ToString("#,##") + " USD";
            savingsValue.Text = annualSavings.ToString("#,##") + " USD";
            spendableIncomeValue.Text = spendableIncome.ToString("#,##") + " USD";
            resultLayout.Visibility = Android.Views.ViewStates.Visible;

            inputCalculeted = true;
            calculateButton.Text = "Clear";
        }

        private void ClearInput()
        {
            incomePerHourEditText.Text = "";
            workHoursEditText.Text = "";
            taxRateEditText.Text = "";
            savingsRateEditText.Text = "";

            resultLayout.Visibility = Android.Views.ViewStates.Invisible;
        }

        private void ConnectViews()
        {
            incomePerHourEditText = FindViewById<EditText>(Resource.Id.incomePerHourEditText);
            workHoursEditText = FindViewById<EditText>(Resource.Id.workHoursEditText);
            taxRateEditText = FindViewById<EditText>(Resource.Id.taxRateEditText);
            savingsRateEditText = FindViewById<EditText>(Resource.Id.savingsRateEditText);

            workSummaryValue = FindViewById<TextView>(Resource.Id.workSummaryValue);
            grossIncomeValue = FindViewById<TextView>(Resource.Id.grossIncomeValue);
            taxPayableValue = FindViewById<TextView>(Resource.Id.taxPayableValue);
            savingsValue = FindViewById<TextView>(Resource.Id.savingsValue);
            spendableIncomeValue = FindViewById<TextView>(Resource.Id.spendableIncomeValue);

            calculateButton = FindViewById<Button>(Resource.Id.calculateButton);
            resultLayout = FindViewById<RelativeLayout>(Resource.Id.resultLayout);

        }
        
    }
}