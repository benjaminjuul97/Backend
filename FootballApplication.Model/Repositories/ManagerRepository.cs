using System;
using FootballApplication.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;


namespace FootballApplication.Model.Repositories;

public class ManagerRepository : BaseRepository
{
   public ManagerRepository(IConfiguration configuration) : base(configuration)
   {
   }

   public Manager GetManagerById(int id)
   {
      NpgsqlConnection dbConn = null;
      try
      {
         //create a new connection for database
         dbConn = new NpgsqlConnection(ConnectionString);

         //creating an SQL command
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = "select * from manager where id = @id";

         cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

         //call the base method to get data
         var data = GetData(dbConn, cmd);

         if (data != null)
         {
            if (data.Read()) //every time loop runs it reads next like from fetched rows
            {
                    return new Manager(Convert.ToInt32(data["id"]))
               {
                  // Firstname = data["firstname"].ToString(),
                  // Lastname = data["lastname"].ToString(),
                  // Age = (int)data["age"],
                  // Experienceyears = (int)data["experienceyears"],
                  // CountryID = (int)data["countryid"],
                  // ClubID = (int)data["clubid"],
                  // Image = data["image"].ToString()
                  //Dob = Convert.ToDateTime(data["dob"]),
                    Firstname = data["firstname"]?.ToString(),
                    Lastname = data["lastname"]?.ToString(),
                    CountryID = data["countryid"] != DBNull.Value ? (int?)data["countryid"] : null,
                    ClubID = data["clubid"] != DBNull.Value ? (int?)data["clubid"] : null,
                    Image = data["image"]?.ToString(),
                    Dob = Convert.ToDateTime(data["dob"]),
               };
                }
         }

         return null;
      }
      finally
      {
         dbConn?.Close();
      }
   }
   public List<Manager> GetManagers()
   {
      NpgsqlConnection dbConn = null;
      var managers = new List<Manager>();
      try
      {
         //create a new connection for database
         dbConn = new NpgsqlConnection(ConnectionString);

         //creating an SQL command
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = "SELECT manager.*, country.flag, club.logo FROM manager JOIN country ON manager.countryid = country.id JOIN club ON manager.clubid = club.id";


         //call the base method to get data
         var data = GetData(dbConn, cmd);

         if (data != null)
         {
            while (data.Read()) //every time loop runs it reads next like from fetched rows
            {
               Manager m = new Manager(Convert.ToInt32(data["id"]))
               {
                  Firstname = data["firstname"].ToString(),
                  Lastname = data["lastname"].ToString(),
                  CountryID = (int)data["countryid"],
                  ClubID = (int)data["clubid"],
                  Image = data["image"].ToString(),
                  Dob = Convert.ToDateTime(data["dob"]),
                  Flag = data["flag"].ToString(),
                  Logo = data["logo"].ToString()
               };

               managers.Add(m);
            }
         }

         return managers;
      }
      finally
      {
         dbConn?.Close();
      }
   }

   //add a new player
   public bool InsertManager(Manager m)
   {
      NpgsqlConnection dbConn = null;
      try
      {
         dbConn = new NpgsqlConnection(ConnectionString);
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = @"
insert into manager
(firstname, lastname, countryid, clubid, image, dob)
values
(@firstname, @lastname, @countryid, @clubid, @image, @dob)";

         //adding parameters in a better way
         cmd.Parameters.AddWithValue("@firstname", NpgsqlDbType.Text, m.Firstname);
         cmd.Parameters.AddWithValue("@lastname", NpgsqlDbType.Text, m.Lastname);
         cmd.Parameters.AddWithValue("@countryid", NpgsqlDbType.Integer, m.CountryID);
         cmd.Parameters.AddWithValue("@clubid", NpgsqlDbType.Integer, m.ClubID);
         cmd.Parameters.AddWithValue("@image", NpgsqlDbType.Text, m.Image);
         cmd.Parameters.AddWithValue("@dob", NpgsqlDbType.Date, m.Dob);


         //will return true if all goes well
         bool result = InsertData(dbConn, cmd);

         return result;
      }
      finally
      {
         dbConn?.Close();
      }
   }

   public bool UpdateManager(Manager m)
   {
      var dbConn = new NpgsqlConnection(ConnectionString);
      var cmd = dbConn.CreateCommand();
      cmd.CommandText = @"
update manager set
firstname = @firstname,
lastname = @lastname,
countryid = @countryid,
clubid = @clubid,
image = @image,
dob = @dob
where
id = @id";

         cmd.Parameters.AddWithValue("@firstname", NpgsqlDbType.Text, m.Firstname);
         cmd.Parameters.AddWithValue("@lastname", NpgsqlDbType.Text, m.Lastname);
         cmd.Parameters.AddWithValue("@countryid", NpgsqlDbType.Integer, m.CountryID);
         cmd.Parameters.AddWithValue("@clubid", NpgsqlDbType.Integer, m.ClubID);
         cmd.Parameters.AddWithValue("@image", NpgsqlDbType.Text, m.Image);
         cmd.Parameters.AddWithValue("@dob", NpgsqlDbType.Date, m.Dob);
         cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, m.Id);


      bool result = UpdateData(dbConn, cmd);
      return result;
   }

    public bool DeleteManager(int id)
    {
       var dbConn = new NpgsqlConnection(ConnectionString);
       var cmd = dbConn.CreateCommand();
       cmd.CommandText = @"
 delete from manager
 where id = @id
 ";

       //adding parameters in a better way
       cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

       //will return true if all goes well
       bool result = DeleteData(dbConn, cmd);

       return result;
    }

    public bool Deletemanager(int id)
    {
        throw new NotImplementedException();
    }
}
