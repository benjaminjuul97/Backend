using System;
using FootballApplication.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;


namespace FootballApplication.Model.Repositories;

public class CountryRepository : BaseRepository
{
   public CountryRepository(IConfiguration configuration) : base(configuration)
   {
   }

   public Country GetCountryById(int id)
   {
      NpgsqlConnection dbConn = null;
      try
      {
         //create a new connection for database
         dbConn = new NpgsqlConnection(ConnectionString);

         //creating an SQL command
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = "select * from country where id = @id";

         cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

         //call the base method to get data
         var data = GetData(dbConn, cmd);

         if (data != null)
         {
            if (data.Read()) //every time loop runs it reads next like from fetched rows
            {
                    return new Country(Convert.ToInt32(data["id"]))
               {
                  Cname = data["cname"].ToString(),
                  Flag = data["flag"].ToString()
               };
// #pragma warning restore CS8601 // Possible null reference assignment.
                }
         }

         return null;
      }
      finally
      {
         dbConn?.Close();
      }
   }
   public List<Country> GetCountries()
   {
      NpgsqlConnection dbConn = null;
      var countries = new List<Country>();
      try
      {
         //create a new connection for database
         dbConn = new NpgsqlConnection(ConnectionString);

         //creating an SQL command
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = "select * from country";

         //call the base method to get data
         var data = GetData(dbConn, cmd);

         if (data != null)
         {
            while (data.Read()) //every time loop runs it reads next like from fetched rows
            {
               Country c = new Country(Convert.ToInt32(data["id"]))
               {
                  Cname = data["cname"].ToString(),
                  Flag = data["flag"].ToString()
               };

               countries.Add(c);
            }
         }

         return countries;
      }
      finally
      {
         dbConn?.Close();
      }
   }

   //add a new country
   public bool InsertCountry(Country c)
   {
      NpgsqlConnection dbConn = null;
      try
      {
         dbConn = new NpgsqlConnection(ConnectionString);
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = @"
insert into country
(cname, flag)
values
(@cname, @flag)
";

         //adding parameters in a better way
         cmd.Parameters.AddWithValue("@cname", NpgsqlDbType.Text, c.Cname);
         cmd.Parameters.AddWithValue("@flag", NpgsqlDbType.Text, c.Flag);

         //will return true if all goes well
         bool result = InsertData(dbConn, cmd);

         return result;
      }
      finally
      {
         dbConn?.Close();
      }
   }

   public bool UpdateCountry(Country c)
   {
      var dbConn = new NpgsqlConnection(ConnectionString);
      var cmd = dbConn.CreateCommand();
      cmd.CommandText = @"
      update country set
      cname = @cname,
      flag = @flag
      where
      id = @id";

         cmd.Parameters.AddWithValue("@cname", NpgsqlDbType.Text, c.Cname);
         cmd.Parameters.AddWithValue("@flag", NpgsqlDbType.Text, c.Flag);
         cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, c.Id);



      bool result = UpdateData(dbConn, cmd);
      return result;
   }

    public bool DeleteCountry(int id)
    {
       var dbConn = new NpgsqlConnection(ConnectionString);
       var cmd = dbConn.CreateCommand();
       cmd.CommandText = @"
 delete from country
 where id = @id
 ";

       //adding parameters in a better way
       cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

       //will return true if all goes well
       bool result = DeleteData(dbConn, cmd);

       return result;
    }

    public bool Deletecountry(int id)
    {
        throw new NotImplementedException();
    }
}
