using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator;

public class CalculatorForm : Form
{
    private TextBox display;
    private double firstOperand;
    private string currentOperator = "";
    private bool newInput = true;

    public CalculatorForm()
    {
        Text = "Calculator";
        Size = new Size(320, 450);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        BackColor = Color.FromArgb(30, 30, 30);

        display = new TextBox
        {
            Bounds = new Rectangle(10, 10, 284, 50),
            Font = new Font("Segoe UI", 22, FontStyle.Bold),
            TextAlign = HorizontalAlignment.Right,
            Text = "0",
            ReadOnly = true,
            BackColor = Color.FromArgb(50, 50, 50),
            ForeColor = Color.White,
            BorderStyle = BorderStyle.None
        };
        Controls.Add(display);

        string[,] buttons = {
            { "C", "±", "%", "÷" },
            { "7", "8", "9", "×" },
            { "4", "5", "6", "−" },
            { "1", "2", "3", "+" },
            { "0", ".", "⌫", "=" }
        };

        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                string label = buttons[row, col];
                bool isOperator = label is "÷" or "×" or "−" or "+" or "=";
                bool isUtility = label is "C" or "±" or "%" or "⌫";

                var btn = new Button
                {
                    Text = label,
                    Bounds = new Rectangle(10 + col * 73, 75 + row * 65, 65, 57),
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = Color.White,
                    BackColor = isOperator ? Color.FromArgb(255, 149, 0)
                                : isUtility ? Color.FromArgb(80, 80, 80)
                                : Color.FromArgb(60, 60, 60),
                    Cursor = Cursors.Hand
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += OnButtonClick;
                Controls.Add(btn);
            }
        }
    }

    private void OnButtonClick(object? sender, EventArgs e)
    {
        string val = ((Button)sender!).Text;

        if (char.IsDigit(val[0]))
        {
            display.Text = (newInput || display.Text == "0") ? val : display.Text + val;
            newInput = false;
        }
        else if (val == ".")
        {
            if (newInput) { display.Text = "0."; newInput = false; }
            else if (!display.Text.Contains('.')) display.Text += ".";
        }
        else if (val == "C")
        {
            display.Text = "0"; firstOperand = 0; currentOperator = ""; newInput = true;
        }
        else if (val == "⌫")
        {
            display.Text = display.Text.Length > 1 ? display.Text[..^1] : "0";
        }
        else if (val == "±")
        {
            if (double.TryParse(display.Text, out double n)) display.Text = (-n).ToString();
        }
        else if (val == "%")
        {
            if (double.TryParse(display.Text, out double n)) display.Text = (n / 100).ToString();
        }
        else if (val is "+" or "−" or "×" or "÷")
        {
            firstOperand = double.Parse(display.Text);
            currentOperator = val;
            newInput = true;
        }
        else if (val == "=")
        {
            if (currentOperator == "") return;
            double second = double.Parse(display.Text);
            double result = currentOperator switch
            {
                "+" => firstOperand + second,
                "−" => firstOperand - second,
                "×" => firstOperand * second,
                "÷" => second != 0 ? firstOperand / second : double.NaN,
                _ => second
            };
            display.Text = double.IsNaN(result) ? "Error" : result.ToString();
            currentOperator = "";
            newInput = true;
        }
    }
}
