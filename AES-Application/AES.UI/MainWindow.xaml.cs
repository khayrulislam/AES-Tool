using AES.EncryptDecrypt.algorithm;
using AES.EncryptDecrypt.utility;
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
        int init = 0;
        public MainWindow()
        {
            InitializeComponent();
            filePathTextBox.IsEnabled = false;
            InitialVectorTextBox.IsEnabled = false;
            outputFolderPathTextbox.IsEnabled = false;
            keyTextBox.IsEnabled = true;
        }

        
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mode.SelectedIndex == 1)
            {
                InitialVectorTextBox.IsEnabled = true;
                init = 1;
            }

            if(init == 1)
            {
                if(mode.SelectedIndex == 0)
                {
                    InitialVectorTextBox.IsEnabled = false;
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                filePathTextBox.Text = openFileDialog.FileName;
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
            string x = mode.SelectedIndex.ToString();
            Parameter par = new Parameter();
            par.Key = "Thats my Kung Fu";
            par.InitialVector = "ABCDEFGHIPQRSTUV";
            par.Type = "e";
            par.Mode = "ecb";
            par.InputFilePath = filePathTextBox.Text;
            par.OutputFolderPath = outputFolderPathTextbox.Text;
            var enc = new AESAlgorithm(par);
            enc.Execute();



            

        }

        // decript button
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }


        // error working on
        // output folder path
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                outputFolderPathTextbox.Text = dialog.SelectedPath;
            }
        }



    }
}
