using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AES.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            filePathTextBox.IsEnabled = false;
        }

        
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Console.WriteLine(filePathTextBox.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();

            if (openFileDialog1.ShowDialog() == true)
            {
                Console.WriteLine("hello");
                filePathTextBox.Text = openFileDialog1.FileName;
            }
        }

        private void keyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        // encript button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        // decript button
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }


        // error working on
        // output folder path
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            folderDlg.ShowDialog();

            Console.WriteLine(folderDlg.RootFolder);

        }



    }
}
