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
                  FirstName = data["firstname"].ToString(),
                  LastName = data["lastname"].ToString(),
                  Age = (int)data["age"],
                  ExperienceYears = (int)data["experienceyears"],
                  CountryID = (int)data["countryid"],
                  ClubID = (int)data["clubid"]
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
         cmd.CommandText = "select * from manager";

         //call the base method to get data
         var data = GetData(dbConn, cmd);

         if (data != null)
         {
            while (data.Read()) //every time loop runs it reads next like from fetched rows
            {
               Manager m = new Manager(Convert.ToInt32(data["id"]))
               {
                  FirstName = data["firstname"].ToString(),
                  LastName = data["lastname"].ToString(),
                  Age = (int)data["age"],
                  ExperienceYears = (int)data["experienceyears"],
                  CountryID = (int)data["countryid"],
                  ClubID = (int)data["clubid"]
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
(firstname, lastname, age, experienceyears, countryid, clubid)
values
(@firstname, @lastname, @age, @experienceyears, @countryid, @clubid)";

         //adding parameters in a better way
         cmd.Parameters.AddWithValue("@firstname", NpgsqlDbType.Text, m.FirstName);
         cmd.Parameters.AddWithValue("@lastname", NpgsqlDbType.Text, m.LastName);
         cmd.Parameters.AddWithValue("@age", NpgsqlDbType.Integer, m.Age);
         cmd.Parameters.AddWithValue("@experienceyears", NpgsqlDbType.Integer, m.ExperienceYears);
         cmd.Parameters.AddWithValue("@countryid", NpgsqlDbType.Integer, m.CountryID);
         cmd.Parameters.AddWithValue("@clubid", NpgsqlDbType.Integer, m.ClubID);

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
age = @age,
experienceyears = @experienceyears,
countryid = @countryid,
clubid = @clubid
where
managerid = @id";

         cmd.Parameters.AddWithValue("@firstname", NpgsqlDbType.Text, m.FirstName);
         cmd.Parameters.AddWithValue("@lastname", NpgsqlDbType.Text, m.LastName);
         cmd.Parameters.AddWithValue("@age", NpgsqlDbType.Integer, m.Age);
         cmd.Parameters.AddWithValue("@experienceyears", NpgsqlDbType.Integer, m.ExperienceYears);
         cmd.Parameters.AddWithValue("@countryid", NpgsqlDbType.Integer, m.CountryID);
         cmd.Parameters.AddWithValue("@clubid", NpgsqlDbType.Integer, m.ClubID);

      bool result = UpdateData(dbConn, cmd);
      return result;
   }

    public bool Deletemanager(int id)
    {
       var dbConn = new NpgsqlConnection(ConnectionString);
       var cmd = dbConn.CreateCommand();
       cmd.CommandText = @"
 delete from manager
 where managerid = @id
 ";

       //adding parameters in a better way
       cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

       //will return true if all goes well
       bool result = DeleteData(dbConn, cmd);

       return result;
    }

    public bool DeleteManager(int id)
    {
        throw new NotImplementedException();
    }
}
