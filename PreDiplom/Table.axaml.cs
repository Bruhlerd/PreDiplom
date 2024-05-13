using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows;

namespace PreDiplom;

public partial class Table : Window
{
    public Table()
    {
        InitializeComponent();
        ShowTable(fullTable);
        FillStatus();
    }
    string fullTable = "SELECT inventory.ID, equipment.name, inventory.Place_id, inventory.Count FROM inventory JOIN equipment on inventory.Equipment_id = equipment.ID ";

    private List<inventory> inv;
    private List<equipment> equip;
    private List<place> pl;
    string connStr = "server=localhost;database=prediplom;port=3306;User Id=root;password=Pass&_word1";
    private MySqlConnection conn;
    public void ShowTable(string sql)
    {
        inv = new List<inventory>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var ord = new inventory()
            {
                ID = reader.GetInt32("ID"),
                Equipment_id = reader.GetString("name"),
                Place_id= reader.GetString("Place_id"),
                Count = reader.GetInt32("Count"),
               
            };
            inv.Add(ord);
        }
        conn.Close();
        DataGrid.ItemsSource = inv;
    }
    
    private void SearchGoods(object? sender, TextChangedEventArgs e)
    {
        var gds = inv;
        gds = gds.Where(x => x.Place_id.Contains((Search_Goods.Text))).ToList();
        DataGrid.ItemsSource = gds;
    }
    
    private void Reset_OnClick(object? sender, RoutedEventArgs e)
    {
        ShowTable(fullTable);
        Search_Goods.Text = string.Empty;
    }

    private void Del(object? sender, RoutedEventArgs e)
    {
        try
        {
            inventory usr = DataGrid.SelectedItem as inventory;
            if (usr == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM inventory WHERE ID = " + usr.ID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            inv.Remove(usr);
            ShowTable(fullTable);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    private void AddData(object? sender, RoutedEventArgs e)
    {
        inventory ort = new inventory();
        AddEd add = new AddEd(ort, inv);
        add.Show();
        this.Close();
    }
    
    private void Edit(object? sender, RoutedEventArgs e)
    {
        inventory currenOrder = DataGrid.SelectedItem as inventory;
        if (currenOrder == null)
            return;
        AddEd edit = new AddEd(currenOrder, inv);
        edit.Show();
        this.Close();
    }
    
    private void CmbStatus(object? sender, SelectionChangedEventArgs e)
    {
        var genderComboBox = (ComboBox)sender;
        var currentGender = genderComboBox.SelectedItem as equipment;
        var filteredUsers = equip
            .Where(x => x.name == currentGender.name)
            .ToList();
        DataGrid.ItemsSource = filteredUsers;
    }
    
    public void FillStatus()
    {
        equip = new List<equipment>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("select * from equipment", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentGender = new equipment()
            {
                ID = reader.GetInt32("Id"),
                name = reader.GetString("name"),
                model_id = reader.GetInt32("model_id")

            };
            equip.Add(currentGender);
        }
        conn.Close();
        var genderComboBox = this.Find<ComboBox>("CmbGender");
        genderComboBox.ItemsSource = equip;
    }

    public void Non (object? sender, RoutedEventArgs routedEventArgs)
    {
        Environment.Exit(0);
    }
}