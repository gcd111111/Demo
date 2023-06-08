using MySql.Data.MySqlClient;
using System;

class Program
{
    static MySqlConnection conn = null;
    static void Main(string[] args)
    {
        try
        {
            conn = new MySqlConnection("server=localhost;userid=root;password=root;database=studymysql;charset=utf8mb4");
            conn.Open();
            //Add();
            //Query();
            //Update();
            Delete();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        finally
        {
            Console.ReadKey();
            conn.Close();
        }
    }

    //增
    static void Add()
    {
        try
        {
            //MySqlCommand cmd = new MySqlCommand("insert into userinfo (name, age) values ('haha', 66)", conn);
            //int rowsAffected = cmd.ExecuteNonQuery();
            //Console.WriteLine($"{rowsAffected} rows added to database.");
            MySqlCommand cmd = new MySqlCommand("insert into userinfo set name='haha',age=66", conn);
            cmd.ExecuteNonQuery();
            int Id = (int)cmd.LastInsertedId;
            Console.WriteLine("Sql Insert Key{0}:",Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while inserting data: " + ex.Message);
        }
    }

    //删
    static void Delete()
    {
        MySqlCommand cmd = new MySqlCommand("delete from userinfo where Id=1", conn);
        cmd.ExecuteNonQuery();
        Console.WriteLine("delete done");
    }

    //改
    static void Update()
    {
        MySqlCommand cmd = new MySqlCommand("update userinfo set name = @name, age = @age where Id = @Id", conn);
        cmd.Parameters.AddWithValue("name", "xoxo");
        cmd.Parameters.AddWithValue("age", 123);
        cmd.Parameters.AddWithValue("Id", 1);
        cmd.ExecuteNonQuery();
        Console.WriteLine("update done");
    }

    //查
    static void Query()
    {
        //MySqlCommand cmd = new MySqlCommand("select * from userinfo", conn);
          MySqlCommand cmd = new MySqlCommand("select * from userinfo where name='plane'", conn);
         MySqlDataReader reader= cmd.ExecuteReader();
        while (reader.Read())
        { 
            int Id=reader.GetInt32("Id");
            string name=reader.GetString("Name");
            int age = reader.GetInt32("age");

            Console.WriteLine(string.Format("sql result：Id:{0} name:{1} age:{2}",Id,name,age));
        }
    }
}