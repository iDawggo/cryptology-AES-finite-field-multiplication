using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lesson_26
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

        private void calculate_Click(object sender, RoutedEventArgs e)
        {
            String hex1 = hexIn1.Text;
            String hex2 = hexIn2.Text;

            String binary1 = Convert.ToString(Convert.ToInt32(hex1, 16), 2).PadLeft(8, '0');
            String binary2 = Convert.ToString(Convert.ToInt32(hex2, 16), 2).PadLeft(8, '0');

            //Creating an array of all bitshifts
            String[] bitShifts = new string[binary2.Length + 1];
            bitShifts[0] = hex1;

            for (int i = 0; i < binary2.Length; i++)
            {
                int tempInt;
                int tempXor;
                String tempString = "";

                //Checking if the first bit is 0
                if (Convert.ToInt32(bitShifts[i], 16) < 128)
                {
                    //Bitshift
                    tempInt = Convert.ToInt32(hex1, 16) << 1;
                    tempString = tempInt.ToString("X2");
                }
                //Else, the bit is 1
                else
                {
                    //XOR-ing
                    tempInt = Convert.ToInt32(hex1, 16) << 1;
                    tempXor = tempInt ^ Convert.ToInt32("11B", 16);
                    tempString = tempXor.ToString("X2");
                }

                //Adding the shift/xor to the array
                hex1 = tempString;
                bitShifts[i + 1] = hex1;
            }

            //XOR-ing the hexes together
            int totalInt = 0;
            String totalString = "";
            for (int i = 0; i < binary2.Length; i++)
            {
                if (binary2.Substring(7 - i, 1).Equals("1"))
                {
                    String bitShift = bitShifts[i];
                    totalInt = totalInt ^ Convert.ToInt32(bitShift, 16);
                    totalString = totalInt.ToString("X2");
                }
            }

            hexOut.Text = totalString;
        }

        private void hexIn1_TextChanged(object sender, TextChangedEventArgs e)
        {
            String hex1 = hexIn1.Text;

            //Restricting character input to hex characters
            foreach (char c in hex1)
            {
                if (!((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f')) && hex1 != String.Empty)
                {
                    hexIn1.Text = hex1.Remove(hex1.Length - 1, 1);
                    hexIn1.SelectionStart = hexIn1.Text.Length;
                }
            }
        }

        private void hexIn2_TextChanged(object sender, TextChangedEventArgs e)
        {
            String hex2 = hexIn2.Text;

            //Restricting character input to hex characters
            foreach (char c in hex2)
            {
                if (!((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f')) && hex2 != String.Empty)
                {
                    hexIn2.Text = hex2.Remove(hex2.Length - 1, 1);
                    hexIn2.SelectionStart = hexIn2.Text.Length;
                }
            }
        }
    }
}
