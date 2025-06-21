using Avalonia.Controls;
using AiUoVsix.Command.EntityFrameworkCore.ViewModels;
using System;
using System.Diagnostics;

namespace AiUoVsix.Command.EntityFrameworkCore.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                DataContext = new MainWindowViewModel();
                Console.WriteLine("MainWindow initialized successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing MainWindow: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}