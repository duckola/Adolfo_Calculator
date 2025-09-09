using System;
using System.Windows.Forms;
using System.Data; 
namespace Adolfo_Calculator_F2
{
    public partial class Form1 : Form
    {
        double memoryValue = 0;   // for MS, MR, MC
        double ansValue = 0;      // for ANS

        // This will hold the entire expression as a string
        string currentExpression = "";
        // This will hold the formatted string for display
        string displayExpression = "";

        bool newNumberExpected = false; // To clear the display when a new number starts after an operator or equals
        bool lastInputWasOperator = false; // To handle operator replacement

        public Form1()
        {
            InitializeComponent();
            txtDisplay.Text = "0"; // Initialize display
        }

        // Helper to convert degrees to radians
        private double ToRadians(double angle) => angle * Math.PI / 180.0;

        // --- Number and Decimal Button Clicks ---
        private void btnNumber_Click(object sender, EventArgs e)
        {
            Button num = (Button)sender;

            if (newNumberExpected || txtDisplay.Text == "0" || (txtDisplay.Text == "Error" && num.Text != "."))
            {
                txtDisplay.Text = "";
                newNumberExpected = false;
            }

            if (num.Text == ".")
            {
                if (!txtDisplay.Text.Contains("."))
                {
                    txtDisplay.Text += num.Text;
                    currentExpression += num.Text;
                    displayExpression += num.Text;
                }
            }
            else
            {
                txtDisplay.Text += num.Text;
                currentExpression += num.Text;
                displayExpression += num.Text;
            }
            lastInputWasOperator = false;
        }

        // --- Operator Button Clicks ---
        private void btnOperator_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string op = btn.Text;

            // Handle operator replacement
            if (lastInputWasOperator)
            {
                // Replace the last operator in currentExpression and displayExpression
                if (currentExpression.Length > 0 && displayExpression.Length > 0)
                {
                    currentExpression = currentExpression.Remove(currentExpression.Length - 1) + ConvertOperatorForCalculation(op);
                    displayExpression = displayExpression.Remove(displayExpression.Length - 1) + op;
                }
            }
            else
            {
                currentExpression += ConvertOperatorForCalculation(op);
                displayExpression += op;
            }
            txtDisplay.Text = displayExpression; // Update the display to show the expression
            newNumberExpected = true;
            lastInputWasOperator = true;
        }

        // Helper to convert display operators to C# usable operators for DataTable.Compute
        private string ConvertOperatorForCalculation(string displayOp)
        {
            switch (displayOp)
            {
                case "×": return "*";
                case "÷": return "/";
                case "Mod": return "%"; 
                case "xʸ": return "^";
                default: return displayOp;
            }
        }

        private void ApplyUnaryFunction(Func<double, double> func, string functionName)
        {
            if (string.IsNullOrEmpty(txtDisplay.Text) || txtDisplay.Text == "Error") return;

            // Try to extract the last number from the current expression
            double num;
            string currentNumString = "";
            int lastNumStartIndex = -1;

            for (int i = currentExpression.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(currentExpression[i]) || currentExpression[i] == '.')
                {
                    currentNumString = currentExpression[i] + currentNumString;
                    lastNumStartIndex = i;
                }
                else if (lastNumStartIndex != -1) // Found an operator/function, number ended
                {
                    break;
                }
            }

            if (double.TryParse(currentNumString, out num))
            {
                try
                {
                    double result = func(num);
                    // Replace the number in the expression with the function result
                    if (lastNumStartIndex != -1)
                    {
                        currentExpression = currentExpression.Substring(0, lastNumStartIndex) + result.ToString();
                        displayExpression = displayExpression.Substring(0, lastNumStartIndex) + functionName + "(" + num.ToString() + ")";
                        txtDisplay.Text = displayExpression;
                    }
                    else // If it's just a single number
                    {
                        currentExpression = result.ToString();
                        displayExpression = functionName + "(" + num.ToString() + ")";
                        txtDisplay.Text = displayExpression;
                    }
                }
                catch (Exception ex)
                {
                    txtDisplay.Text = "Error";
                    currentExpression = "";
                    displayExpression = "";
                    MessageBox.Show("Calculation error: " + ex.Message);
                }
            }
            else // If no valid number to apply function to (e.g., expression is "2 + " or empty)
            {
                txtDisplay.Text = "Error: Invalid input for function.";
                currentExpression = "";
                displayExpression = "";
            }
            newNumberExpected = true; // After applying a function, the next number should start fresh
            lastInputWasOperator = false;
        }

        private void btnSin_Click(object sender, EventArgs e) => ApplyUnaryFunction(n => Math.Sin(ToRadians(n)), "sin");
        private void btnCos_Click(object sender, EventArgs e) => ApplyUnaryFunction(n => Math.Cos(ToRadians(n)), "cos");
        private void btnTan_Click(object sender, EventArgs e) => ApplyUnaryFunction(n => Math.Tan(ToRadians(n)), "tan");

        private void btnSinh_Click(object sender, EventArgs e) => ApplyUnaryFunction(n => Math.Sinh(n), "sinh");
        private void btnCosh_Click(object sender, EventArgs e) => ApplyUnaryFunction(n => Math.Cosh(n), "cosh");
        private void btnTanh_Click(object sender, EventArgs e) => ApplyUnaryFunction(n => Math.Tanh(n), "tanh");

        private void btnLog_Click(object sender, EventArgs e) => ApplyUnaryFunction(n => Math.Log10(n), "log");
        private void btnLn_Click(object sender, EventArgs e) => ApplyUnaryFunction(n => Math.Log(n), "ln");

        private void btnSqrt_Click(object sender, EventArgs e) => ApplyUnaryFunction(n => Math.Sqrt(n), "sqrt");
        private void btnSquared_Click(object sender, EventArgs e) => ApplyUnaryFunction(n => Math.Pow(n, 2), "sq");
        private void btnCube_Click(object sender, EventArgs e) => ApplyUnaryFunction(n => Math.Pow(n, 3), "cube");
        private void btnExp_Click(object sender, EventArgs e) => ApplyUnaryFunction(n => Math.Exp(n), "exp");
        private void btnAbs_Click(object sender, EventArgs e) => ApplyUnaryFunction(n => Math.Abs(n), "abs");


        // Special handling for factorial and Pi as they don't follow the general unary function pattern as neatly
        private void btnFactorial_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDisplay.Text) || txtDisplay.Text == "Error") return;

            double num;
            string currentNumString = "";
            int lastNumStartIndex = -1;

            for (int i = currentExpression.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(currentExpression[i]) || currentExpression[i] == '.')
                {
                    currentNumString = currentExpression[i] + currentNumString;
                    lastNumStartIndex = i;
                }
                else if (lastNumStartIndex != -1)
                {
                    break;
                }
            }

            if (double.TryParse(currentNumString, out num) && num >= 0 && num == (int)num) // Factorial only for non-negative integers
            {
                try
                {
                    long fact = 1;
                    for (int i = 1; i <= num; i++) fact *= i;

                    if (lastNumStartIndex != -1)
                    {
                        currentExpression = currentExpression.Substring(0, lastNumStartIndex) + fact.ToString();
                        displayExpression = displayExpression.Substring(0, lastNumStartIndex) + "fact(" + num.ToString() + ")";
                        txtDisplay.Text = displayExpression;
                    }
                    else
                    {
                        currentExpression = fact.ToString();
                        displayExpression = "fact(" + num.ToString() + ")";
                        txtDisplay.Text = displayExpression;
                    }
                }
                catch (Exception ex)
                {
                    txtDisplay.Text = "Error";
                    currentExpression = "";
                    displayExpression = "";
                    MessageBox.Show("Calculation error: " + ex.Message);
                }
            }
            else
            {
                txtDisplay.Text = "Error: Invalid input for factorial.";
                currentExpression = "";
                displayExpression = "";
            }
            newNumberExpected = true;
            lastInputWasOperator = false;
        }

        private void btnPi_Click(object sender, EventArgs e)
        {
            // If starting a new number or after an operator, replace current content
            if (newNumberExpected || txtDisplay.Text == "0" || lastInputWasOperator)
            {
                currentExpression = Math.PI.ToString();
                displayExpression = "π";
            }
            else // Append Pi (e.g., if user types 2, then Pi, make it 2*Pi implicitly)
            {
                currentExpression += "*" + Math.PI.ToString();
                displayExpression += "π";
            }
            txtDisplay.Text = displayExpression;
            newNumberExpected = false; // Pi is a number, so next input might be an operator
            lastInputWasOperator = false;
        }

        // --- Memory Functions ---
        private void btnMemoryStore_Click(object sender, EventArgs e)
        {
            // If the display contains a result, store that. Otherwise, try to evaluate the expression.
            if (double.TryParse(txtDisplay.Text, out double currentDisplayedValue))
            {
                memoryValue = currentDisplayedValue;
            }
            else
            {
                try
                {
                    DataTable dt = new DataTable();
                    var v = dt.Compute(currentExpression, "");
                    memoryValue = Convert.ToDouble(v);
                }
                catch
                {
                    MessageBox.Show("Cannot store invalid expression.", "Memory Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnMemoryRecall_Click(object sender, EventArgs e)
        {
            currentExpression = memoryValue.ToString();
            displayExpression = memoryValue.ToString();
            txtDisplay.Text = displayExpression;
            newNumberExpected = true; // After recalling, the next number input should start fresh
            lastInputWasOperator = false;
        }

        private void btnMemoryClear_Click(object sender, EventArgs e) => memoryValue = 0;

        // --- Special Buttons ---
        private void btnAns_Click(object sender, EventArgs e)
        {
            if (newNumberExpected || txtDisplay.Text == "0" || lastInputWasOperator)
            {
                currentExpression = ansValue.ToString();
                displayExpression = "Ans";
            }
            else
            {
                currentExpression += ansValue.ToString();
                displayExpression += "Ans";
            }
            txtDisplay.Text = displayExpression;
            newNumberExpected = false;
            lastInputWasOperator = false;
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            try
            {

                string expressionToEvaluate = currentExpression;

                // Simple Modulus fix for DataTable.Compute (it uses % operator)
                expressionToEvaluate = expressionToEvaluate.Replace("Mod", "%");
                expressionToEvaluate = expressionToEvaluate.Replace("xʸ", "^"); // Placeholder, needs proper parsing for evaluation

                // Handle the 'xʸ' operation manually if it's present
                while (expressionToEvaluate.Contains("^"))
                {
                    int powerIndex = expressionToEvaluate.IndexOf("^");

                    // Find base (number before ^)
                    int baseStartIndex = powerIndex - 1;
                    while (baseStartIndex >= 0 && (char.IsDigit(expressionToEvaluate[baseStartIndex]) || expressionToEvaluate[baseStartIndex] == '.'))
                    {
                        baseStartIndex--;
                    }
                    baseStartIndex++; // Move back to the start of the number

                    string baseStr = expressionToEvaluate.Substring(baseStartIndex, powerIndex - baseStartIndex);

                    // Find exponent (number after ^)
                    int exponentEndIndex = powerIndex + 1;
                    while (exponentEndIndex < expressionToEvaluate.Length && (char.IsDigit(expressionToEvaluate[exponentEndIndex]) || expressionToEvaluate[exponentEndIndex] == '.'))
                    {
                        exponentEndIndex++;
                    }
                    string exponentStr = expressionToEvaluate.Substring(powerIndex + 1, exponentEndIndex - (powerIndex + 1));

                    if (double.TryParse(baseStr, out double baseVal) && double.TryParse(exponentStr, out double exponentVal))
                    {
                        double powResult = Math.Pow(baseVal, exponentVal);
                        expressionToEvaluate = expressionToEvaluate.Remove(baseStartIndex, (exponentEndIndex - baseStartIndex));
                        expressionToEvaluate = expressionToEvaluate.Insert(baseStartIndex, powResult.ToString());
                    }
                    else
                    {
                        txtDisplay.Text = "Error: Invalid power expression";
                        currentExpression = "";
                        displayExpression = "";
                        return;
                    }
                }

                // Evaluate the simplified expression using DataTable.Compute
                DataTable dt = new DataTable();
                var result = dt.Compute(expressionToEvaluate.Replace("×", "*").Replace("÷", "/"), ""); // Convert display operators to C# operators

                ansValue = Convert.ToDouble(result);
                txtDisplay.Text = ansValue.ToString();
                currentExpression = ansValue.ToString(); // Set currentExpression to the result for further calculations
                displayExpression = ansValue.ToString();
            }
            catch (SyntaxErrorException)
            {
                txtDisplay.Text = "Error: Invalid expression";
                currentExpression = "";
                displayExpression = "";
            }
            catch (DivideByZeroException)
            {
                txtDisplay.Text = "Error: Division by zero";
                currentExpression = "";
                displayExpression = "";
            }
            catch (Exception ex)
            {
                txtDisplay.Text = "Error";
                currentExpression = "";
                displayExpression = "";
                MessageBox.Show("Calculation error: " + ex.Message);
            }
            newNumberExpected = true;
            lastInputWasOperator = false;
        }

        private void btnAC_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            currentExpression = "";
            displayExpression = "";
            newNumberExpected = false;
            lastInputWasOperator = false;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (displayExpression.Length > 0)
            {
                displayExpression = displayExpression.Remove(displayExpression.Length - 1);
                currentExpression = currentExpression.Remove(currentExpression.Length - 1); // Assuming 1:1 char removal for now

                if (displayExpression == "")
                {
                    txtDisplay.Text = "0";
                }
                else
                {
                    txtDisplay.Text = displayExpression;
                }
                // Re-evaluate lastInputWasOperator after deletion
                lastInputWasOperator = displayExpression.Length > 0 &&
                                       (displayExpression.EndsWith("+") || displayExpression.EndsWith("-") ||
                                        displayExpression.EndsWith("×") || displayExpression.EndsWith("÷") ||
                                        displayExpression.EndsWith("Mod") || displayExpression.EndsWith("xʸ"));

                // Re-evaluate newNumberExpected - tricky. For simplicity, set to false
                newNumberExpected = false;
            }
            else
            {
                txtDisplay.Text = "0";
            }
        }


    }
}