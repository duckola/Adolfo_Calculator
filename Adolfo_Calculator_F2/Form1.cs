using System;
using System.Linq; // Added for .Last() in btnEquals_Click
using System.Windows.Forms;

namespace Adolfo_Calculator_F2
{
    public partial class Form1 : Form
    {
        double memoryValue = 0;   // for MS, MR, MC
        double result = 0;
        string operation = "";
        bool enterValue = false;
        double ansValue = 0;      // for ANS
        string expression = "";   // Stores the ongoing mathematical expression

        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            // You can leave this empty or add specific drawing logic if needed.
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {
            // You can leave this empty or add specific drawing logic if needed.
        }

        // Unary Operators
        private void btnSin_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                double sinResult = Math.Sin(ToRadians(value));
                txtDisplay.Text = sinResult.ToString();
                expression = $"sin({value}) = {sinResult}";
                ansValue = sinResult;
                enterValue = true;
            }
        }
        private void btnCos_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                double cosResult = Math.Cos(ToRadians(value));
                txtDisplay.Text = cosResult.ToString();
                expression = $"cos({value}) = {cosResult}";
                ansValue = cosResult;
                enterValue = true;
            }
        }
        private void btnTan_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                double tanResult = Math.Tan(ToRadians(value));
                txtDisplay.Text = tanResult.ToString();
                expression = $"tan({value}) = {tanResult}";
                ansValue = tanResult;
                enterValue = true;
            }
        }

        private void btnSinh_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                double sinhResult = Math.Sinh(value);
                txtDisplay.Text = sinhResult.ToString();
                expression = $"sinh({value}) = {sinhResult}";
                ansValue = sinhResult;
                enterValue = true;
            }
        }
        private void btnCosh_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                double coshResult = Math.Cosh(value);
                txtDisplay.Text = coshResult.ToString();
                expression = $"cosh({value}) = {coshResult}";
                ansValue = coshResult;
                enterValue = true;
            }
        }
        private void btnTanh_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                double tanhResult = Math.Tanh(value);
                txtDisplay.Text = tanhResult.ToString();
                expression = $"tanh({value}) = {tanhResult}";
                ansValue = tanhResult;
                enterValue = true;
            }
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value) && value > 0)
            {
                double logResult = Math.Log10(value);
                txtDisplay.Text = logResult.ToString();
                expression = $"log({value}) = {logResult}";
                ansValue = logResult;
                enterValue = true;
            }
            else if (value <= 0)
            {
                txtDisplay.Text = "Error";
                expression = "";
                enterValue = true;
            }
        }
        private void btnLn_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value) && value > 0)
            {
                double lnResult = Math.Log(value);
                txtDisplay.Text = lnResult.ToString();
                expression = $"ln({value}) = {lnResult}";
                ansValue = lnResult;
                enterValue = true;
            }
            else if (value <= 0)
            {
                txtDisplay.Text = "Error";
                expression = "";
                enterValue = true;
            }
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value) && value >= 0)
            {
                double sqrtResult = Math.Sqrt(value);
                txtDisplay.Text = sqrtResult.ToString();
                expression = $"sqrt({value}) = {sqrtResult}";
                ansValue = sqrtResult;
                enterValue = true;
            }
            else if (value < 0)
            {
                txtDisplay.Text = "Error"; // Cannot take sqrt of negative number
                expression = "";
                enterValue = true;
            }
        }
        private void btnPower_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out result))
            {
                operation = "xʸ";
                expression += $" {operation} ";
                txtDisplay.Text = expression;
                enterValue = true;
            }
        }
        private void btnSquared_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                double squaredResult = Math.Pow(value, 2);
                txtDisplay.Text = squaredResult.ToString();
                expression = $"({value})² = {squaredResult}";
                ansValue = squaredResult;
                enterValue = true;
            }
        }
        private void btnCube_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                double cubeResult = Math.Pow(value, 3);
                txtDisplay.Text = cubeResult.ToString();
                expression = $"({value})³ = {cubeResult}";
                ansValue = cubeResult;
                enterValue = true;
            }
        }

        private void btnPi_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = Math.PI.ToString();
            expression = Math.PI.ToString();
            enterValue = true;
        }
        private void btnFactorial_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtDisplay.Text, out int n) && n >= 0)
            {
                long fact = 1;
                for (int i = 1; i <= n; i++) fact *= i;
                txtDisplay.Text = fact.ToString();
                expression = $"{n}! = {fact}";
                ansValue = fact;
                enterValue = true;
            }
            else if (n < 0)
            {
                txtDisplay.Text = "Error";
                expression = "";
                enterValue = true;
            }
        }
        private void btnExp_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                double expResult = Math.Exp(value);
                txtDisplay.Text = expResult.ToString();
                expression = $"e^({value}) = {expResult}";
                ansValue = expResult;
                enterValue = true;
            }
        }
        private void btnAbs_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                double absResult = Math.Abs(value);
                txtDisplay.Text = absResult.ToString();
                expression = $"|{value}| = {absResult}";
                ansValue = absResult;
                enterValue = true;
            }
        }

        private void btnAns_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = ansValue.ToString();
            expression = ansValue.ToString();
            enterValue = false; // Allow adding operators/numbers to ANS
        }
        // Replace the following line:
        // private static readonly char[] separator = new char[] { '+', '-', '×', '÷', 'Mod', 'xʸ' };

        // With this corrected version:
        private static readonly string[] separator = new string[] { "+", "-", "×", "÷", "Mod", "xʸ" };

        private void btnEquals_Click(object sender, EventArgs e)
        {
            // If there's an ongoing operation
            if (!string.IsNullOrEmpty(operation))
            {
                // Get the last number from the displayed expression
                // This is a simplistic way to get the last number. For complex parsing, you'd need a more robust approach.
                string[] parts = txtDisplay.Text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                double secondNum = 0;

                if (parts.Length > 0 && double.TryParse(parts.Last().Trim(), out double num))
                {
                    secondNum = num;
                }
                else if (double.TryParse(txtDisplay.Text, out num)) // If no operator, just display the number as result
                {
                    result = num;
                    operation = ""; // Clear operation as nothing was truly operated
                }


                switch (operation)
                {
                    case "+": result = result + secondNum; break;
                    case "-": result = result - secondNum; break;
                    case "×": result = result * secondNum; break;
                    case "÷": result = (secondNum != 0) ? (result / secondNum) : double.NaN; break;
                    case "Mod": result = result % secondNum; break;
                    case "xʸ": result = Math.Pow(result, secondNum); break;
                }
            }
            else if (double.TryParse(txtDisplay.Text, out double currentVal))
            {
                result = currentVal; // If no operation, the current display is the result
            }


            if (double.IsNaN(result) || double.IsInfinity(result))
            {
                txtDisplay.Text = "Error";
                expression = ""; // Clear expression on error
            }
            else
            {
                txtDisplay.Text = result.ToString();
                expression = result.ToString(); // Update expression with the result
            }

            ansValue = result;
            operation = ""; // Clear operation after calculation
            enterValue = true; // Ready for a new calculation or to use the result
        }

        // Memory Functions
        private void btnMemoryStore_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                memoryValue = value;
                enterValue = true; // Ready for new input after storing
            }
        }
        private void btnMemoryRecall_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = memoryValue.ToString();
            expression = memoryValue.ToString();
            enterValue = true; // Ready for new input/operation after recalling
        }
        private void btnMemoryClear_Click(object sender, EventArgs e)
        {
            memoryValue = 0;
            enterValue = true; // Does not affect display, just internal memory
        }

        // Clear and Delete
        private void btnAC_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            result = 0;
            operation = "";
            expression = ""; // Clear the expression
            enterValue = false;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            // If an operation is pending and we're entering a new one,
            // process the previous operation first.
            if (!string.IsNullOrEmpty(operation) && !enterValue)
            {
                // This part would be more complex for full order of operations
                // For simplicity here, if another operator is pressed, we calculate
                // the previous part of the expression.
                btnEquals_Click(sender, e); // Simulate equals for the previous operation
                result = double.Parse(txtDisplay.Text); // Update result with the intermediate calculation
            }
            else if (double.TryParse(txtDisplay.Text, out double currentValue) && string.IsNullOrEmpty(operation))
            {
                result = currentValue; // Set the first operand if no operation was set yet
            }

            operation = btn.Text; // Store the new operation

            // Append the current display value if it's not already part of the expression
            if (enterValue || string.IsNullOrEmpty(expression))
            {
                expression = txtDisplay.Text;
            }

            expression += $" {btn.Text} "; // Add operator with spacing
            txtDisplay.Text = expression;
            enterValue = true; // Set to true so the next number clears the display for new input
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            Button num = (Button)sender;

            if (enterValue) // If a new operation is starting, clear previous expression from display
            {
                txtDisplay.Clear();
                if (!string.IsNullOrEmpty(operation) && !expression.EndsWith(" ")) // Keep expression if an operator was just added
                {
                    // No change to expression, as operator already added it.
                }
                else
                {
                    expression = ""; // Clear expression if it was a result or full clear
                }
                enterValue = false;
            }

            if (txtDisplay.Text == "0" && num.Text != ".") // If display is "0" and not a decimal, clear it
            {
                txtDisplay.Clear();
            }

            if (num.Text == ".")
            {
                if (!txtDisplay.Text.Contains("."))
                {
                    txtDisplay.Text += num.Text;
                    expression += num.Text;
                }
            }
            else
            {
                txtDisplay.Text += num.Text;
                expression += num.Text;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text.Length > 0)
            {
                txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.Text.Length - 1);
                if (expression.Length > 0) // Also remove from expression
                {
                    expression = expression.Remove(expression.Length - 1);
                }
            }

            if (txtDisplay.Text == "")
            {
                txtDisplay.Text = "0";
                expression = ""; // Clear expression if display is empty
            }
        }

        private double ToRadians(double angle) => angle * Math.PI / 180.0;
    }
}