using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using NumberConversion;

namespace Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void OnButtonPressed(object sender, RoutedEventArgs e)
        {
            if(! byte.TryParse(InputBase.Text, out byte inputBase))
            {
                Output.Content = "Invalid Base";
                return;
            }

            try
            {
                if(!byte.TryParse(InputNewBase.Text, out byte inputNewBase))
                {
                    Output.Content = "Invalid New Base";
                }

                string output = NumberConverter.Convert(InputNumber.Text, inputBase, inputNewBase);

                Output.Content = output;
            }
            catch (OverflowException)
            {
                Output.Content = "Number too large";
            }
            catch (FormatException)
            {
                Output.Content = "Invalid Number";
            }
            catch(ArgumentOutOfRangeException)
            {
                Output.Content = "Invalid Base";
            }
        }


        private void ValidateNumber(object sender, TextChangedEventArgs args)
        {
            TextBox textBox = (TextBox)sender;

            int caretIndex = textBox.CaretIndex;

            char[] chars = textBox.Text.ToUpper().ToCharArray();

            Regex regex = new Regex("^[0-9A-Z]");
            char[] newChars = new char[chars.Length];

            for (int i = 0, j = 0; i < chars.Length; i++)
            {
                if (regex.IsMatch(chars[i].ToString()))
                {
                    newChars[j] = chars[i];
                    j++;
                }
                else
                {
                    caretIndex = j;
                }
            }

            textBox.Text = new string(newChars);
            textBox.CaretIndex = caretIndex;

        }

        private void ValidateBase(object sender, TextChangedEventArgs args)
        {
            TextBox textBox = (TextBox)sender;
            int caretIndex = textBox.CaretIndex;

            char[] chars = textBox.Text.ToCharArray();

            Regex regex = new Regex("^[0-9]");

            char[] newChars = new char[chars.Length];

            for (int i = 0, j = 0; i < chars.Length; i++)
            {
                if (regex.IsMatch(chars[i].ToString()))
                {
                    newChars[j] = chars[i];
                    j++;
                }
                else
                {
                    caretIndex = j;
                }
            }

            if (ushort.TryParse(newChars, out ushort number))
            {
                if (number > NumberConverter.MaxBase)
                {
                    number = NumberConverter.MaxBase;
                    string output = number.ToString();
                    textBox.Text = output;
                    textBox.CaretIndex = output.Length;
                    return;
                }
            }

            textBox.Text = new string(newChars);
            textBox.CaretIndex = caretIndex;
        }
    }
}
