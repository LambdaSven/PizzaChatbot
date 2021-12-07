using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Data.Sqlite;
using PizzaBot.Orders;

namespace PizzaBot.Database
{
  public class DBAccessor
  {
    private SqliteConnection Connection = new SqliteConnection(GetConnectionString());

    // Provided by rhildred at https://github.com/rhildred/RazorOverAndUnder/blob/orders/OrderBot/DB.cs
    // Commit 1cefbf9
    public static string GetConnectionString()
    {
      string sFName = "/Orders.db";
      string sPrefix = "Data Source=";
      string sPath = Directory.GetCurrentDirectory();
      string[] subs = sPath.Split(Path.DirectorySeparatorChar);
      for (int n = subs.Length - 1; n > 1; n--)
      {
        // skip first empty path
        string sResult = "";
        for (int nsubs = 1; nsubs < n; nsubs++)
        {
          sResult += Path.DirectorySeparatorChar + subs[nsubs];
        }
        string[] aFiles = Directory.GetFiles(sResult, "*.sln", System.IO.SearchOption.TopDirectoryOnly);
        if (aFiles.Length > 0)
        {
          return sPrefix + sResult + sFName;
        }
      }
      return sPrefix + Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + sFName;
    }
    public DBAccessor()
    {
      Connection.Open();
      CreateTables();
      Connection.Close();
    }

    internal bool SaveOrder(Order o)
    {
      Connection.Open();
      SqliteTransaction Transaction = Connection.BeginTransaction();
      bool res = true;
      res = WriteCustomer(o.Customer);
      res = WriteOrder(o);

      if (res)
      {
        Transaction.Commit();
        Connection.Close();
        return true;
      }
      else
      {
        Transaction.Rollback();
        Connection.Close();
        throw new System.Exception("Error Writing to Database");
      }
    }

    private bool WriteOrder(Order o)
    {
      string q = @"
      INSERT OR IGNORE INTO PizzaOrder (OrderString, PhoneNumber) VALUES ($str, $phn)";
      SqliteCommand cmd = Connection.CreateCommand();
      cmd.CommandText = q;
      cmd.Parameters.AddWithValue("$str", o.OrderString);
      cmd.Parameters.AddWithValue("$phn", o.Customer);
      try {
        cmd.ExecuteNonQuery();
        return true;  
      } 
      catch (Exception e)
      {
        Console.WriteLine(e);
        return false;
      }
    }

    private bool WriteCustomer(String c)
    {
      string q = @"
      INSERT OR IGNORE INTO Customer (PhoneNumber) VALUES ($str)";
      SqliteCommand cmd = Connection.CreateCommand();
      cmd.CommandText = q;
      cmd.Parameters.AddWithValue("$str", c);
      try {
        cmd.ExecuteNonQuery();
        return true;  
      } 
      catch (Exception e)
      {
        Console.WriteLine(e);
        return false;
      }
    }

    internal List<string> GetHistory(string c)
    {
      string q = @"
      SELECT OrderString
      FROM PizzaOrder WHERE PhoneNumber = $str
      ";
      Connection.Open();
      SqliteCommand cmd = Connection.CreateCommand();
      cmd.CommandText = q;
      cmd.Parameters.AddWithValue("$str", c);
      try {
        List<string> results = new List<string>();
        SqliteDataReader data = cmd.ExecuteReader();
        while(data.Read())
        {
          results.Add(data.GetString(0));
        }
        results.Reverse(); // chronological order
        return results;
      }
      catch (Exception e) 
      {
        throw new Exception($"Error Fetching Data: {e}");
      }
    }
    private bool CreateTables()
    {
      try
      {
        string query = @"
            CREATE TABLE IF NOT EXISTS Customer 
              (PhoneNumber varchar(64) NOT NULL, 
              PRIMARY KEY (PhoneNumber));
            CREATE TABLE IF NOT EXISTS PizzaOrder 
              (OrderID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
              OrderString VARCHAR(512) NOT NULL, 
              PhoneNumber varchar(64) NOT NULL, 
              FOREIGN KEY(PhoneNumber) 
                REFERENCES Customer(PhoneNumber));
          ";
        SqliteCommand comm = Connection.CreateCommand();
        comm.CommandText = query;
        int res = comm.ExecuteNonQuery();
        System.Console.WriteLine($"Created {res} Tables");
        return true;
      }
      catch (System.Exception e)
      {
        System.Console.Error.Write(e);
        return false;
      }
    }
  }
}