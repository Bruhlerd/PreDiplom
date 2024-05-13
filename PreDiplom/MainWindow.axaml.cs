using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace PreDiplom;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    string connectionString = "server=localhost; database=prediplom; port=3306; User Id=root;password=Pass&_word1";

    public void Vxod(object? sender, RoutedEventArgs e)
    {
        if (Login.Text == "admin" && Password.Text == "admin")
        {
            Table employee = new Table();
            employee.Add.IsVisible = true;
            employee.Update.IsVisible = true;
            employee.Delete.IsVisible = true;
            employee.Title = "Главная форма (права сотрудника)";
            this.Hide();
            employee.Show();
        }
        else 
        {
            Table client = new Table();
            client.Add.IsVisible = false;
            client.Update.IsVisible = false;
            client.Delete.IsVisible = false;
            this.Hide();
            client.Show();
        }
    }
    
    
    public void Exit_PR(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}