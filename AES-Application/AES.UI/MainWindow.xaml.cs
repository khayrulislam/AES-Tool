﻿using AES.EncryptDecrypt.algorithm;
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
            if (keyTextBox.Text.Length >= 16)
            {
                 encriptButton.IsEnabled = true;
                 decriptButton.IsEnabled = true;
            } 
            
            else
            {
                encriptButton.IsEnabled = false;
                decriptButton.IsEnabled = false;
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (InitialVectorTextBox.Text.Length >= 16)
            {
                encriptButton.IsEnabled = true;
                decriptButton.IsEnabled = true;
            }

            else
            {
                encriptButton.IsEnabled = false;
                decriptButton.IsEnabled = false;
            }
        }

        // encript button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string x = mode.SelectedIndex.ToString();
            Parameter par = new Parameter();
            par.Key = keyTextBox.Text;
            par.InitialVector = InitialVectorTextBox.Text;
            par.Type =  "e";
            par.Mode = mode.SelectedIndex==0 ? "ecb": "cbc";
            par.InputFilePath = filePathTextBox.Text;
            par.OutputFolderPath = outputFolderPathTextbox.Text;
            var enc = new AESAlgorithm(par);
            Task.Run(() => { enc.Execute(); });

        }

        // decript button
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Parameter par = new Parameter();
            par.Key = keyTextBox.Text;
            par.InitialVector = InitialVectorTextBox.Text;
            par.Type = "d";
            par.Mode = mode.SelectedIndex == 0 ? "ecb" : "cbc";
            par.InputFilePath = filePathTextBox.Text;
            par.OutputFolderPath = outputFolderPathTextbox.Text;
            var enc = new AESAlgorithm(par);
            Task.Run(()=> { enc.Execute(); });
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

        private void TextBox_LostFocus_keyTextbox(object sender, RoutedEventArgs e)
        {
            if(keyTextBox.Text.Length < 16)
            {
                System.Windows.Forms.MessageBox.Show("Key size should be at least 16 characters.");
                encriptButton.IsEnabled = false;
                decriptButton.IsEnabled = false;
            }


        }

        private void TextBox_LostFocus_IVTextbox(object sender, RoutedEventArgs e)
        {
            if (InitialVectorTextBox.Text.Length < 16)
            {
                System.Windows.Forms.MessageBox.Show("Initial Vector size should be at least 16 characters.");
                encriptButton.IsEnabled = false;
                decriptButton.IsEnabled = false;
            }
        }

    }
}
