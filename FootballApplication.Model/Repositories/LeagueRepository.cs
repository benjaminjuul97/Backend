using System;
using FootballApplication.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;


namespace FootballApplication.Model.Repositories;

public class LeagueRepository : BaseRepository
{
   public LeagueRepository(IConfiguration configuration) : base(configuration)
   {
   }

   public League GetLeagueByID(int id)
   {
      NpgsqlConnection dbConn = null;
      try
      {
         //create a new connection for database
         dbConn = new NpgsqlConnection(ConnectionString);

         //creating an SQL command
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = "select * from league where id = @leagueid";

         cmd.Parameters.Add("@leagueid", NpgsqlDbType.Integer).Value = id;

         //call the base method to get data
         var data = GetData(dbConn, cmd);

         if (data != null)
         {
            if (data.Read()) //every time loop runs it reads next like from fetched rows
            {
                    return new League(Convert.ToInt32(data["id"]))
               {
                  Name = data["lname"].ToString(),
                  CountryID = (int)data["countryid"]
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
   public List<League> GetLeagues()
   {
      NpgsqlConnection dbConn = null;
      var leagues = new List<League>();
      try
      {
         //create a new connection for database
         dbConn = new NpgsqlConnection(ConnectionString);

         //creating an SQL command
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = "select * from league";

         //call the base method to get data
         var data = GetData(dbConn, cmd);

         if (data != null)
         {
            while (data.Read()) //every time loop runs it reads next like from fetched rows
            {
               League l = new League(Convert.ToInt32(data["id"]))
               {
                  Name = data["lname"].ToString(),
                  CountryID = (int)data["countryid"]
               };

               leagues.Add(l);
            }
         }

         return leagues;
      }
      finally
      {
         dbConn?.Close();
      }
   }

   //add a new player
   public bool InsertLeague(League l)
   {
      NpgsqlConnection dbConn = null;
      try
      {
         dbConn = new NpgsqlConnection(ConnectionString);
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = @"
insert into league
(lname, countryid)
values
(@lname, @countryid)
";

         //adding parameters in a better way
         cmd.Parameters.AddWithValue("@lname", NpgsqlDbType.Text, l.Name);
         cmd.Parameters.AddWithValue("@countryid", NpgsqlDbType.Integer, l.CountryID);

         //will return true if all goes well
         bool result = InsertData(dbConn, cmd);

         return result;
      }
      finally
      {
         dbConn?.Close();
      }
   }

   public bool UpdateLeague(League l)
   {
      var dbConn = new NpgsqlConnection(ConnectionString);
      var cmd = dbConn.CreateCommand();
      cmd.CommandText = @"
update league set
lname = @lname,
countryid = @countryid
where
leagueid = @leagueid";

         cmd.Parameters.AddWithValue("@lname", NpgsqlDbType.Text, l.Name);
         cmd.Parameters.AddWithValue("@countryid", NpgsqlDbType.Text, l.CountryID);

      bool result = UpdateData(dbConn, cmd);
      return result;
   }

    public bool DeleteLeague(int id)
    {
       var dbConn = new NpgsqlConnection(ConnectionString);
       var cmd = dbConn.CreateCommand();
       cmd.CommandText = @"
 delete from league
 where leagueid = @leagueid
 ";

       //adding parameters in a better way
       cmd.Parameters.AddWithValue("@leagueid", NpgsqlDbType.Integer, id);

       //will return true if all goes well
       bool result = DeleteData(dbConn, cmd);

       return result;
    }

    public bool Deleteleague(int id)
    {
        throw new NotImplementedException();
    }
}
