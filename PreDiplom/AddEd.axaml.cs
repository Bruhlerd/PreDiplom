using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;

namespace PreDiplom;

public partial class AddEd : Window
{
    private List<inventory> Inv;
    private inventory CurrenOrder;
    public AddEd(inventory currentOrder, List<inventory> orders)
    {
        InitializeComponent();
        CurrenOrder = currentOrder;
        this.DataContext = currentOrder;
        Inv = orders;
    }
    
    private MySqlConnection conn;
    string connStr = "server=localhost;database=prediplom;port=3306;User Id=root;password=Pass&_word1";

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var usr = Inv.FirstOrDefault(x => x.ID == CurrenOrder.ID);
        if (usr == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO inventory VALUES (" + Convert.ToInt32(id.Text) + ", '" + eq_id.Text + "', '" + pl_id.Text + "', '" + co.Text + "');";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE inventory SET Equipment_id = '" + eq_id.Text + "', Place_id = '" +  pl_id.Text + "',  Count = '" + co.Text +"' WHERE ID = " + Convert.ToInt32(id.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        Table back = new Table();
        this.Close();
        back.Show(); 
    }
}