using System;
using FootballApplication.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;


namespace FootballApplication.Model.Repositories;

public class ClubRepository : BaseRepository
{
   public ClubRepository(IConfiguration configuration) : base(configuration)
   {
   }

   public Club GetClubById(int id)
   {
      NpgsqlConnection dbConn = null;
      try
      {
         //create a new connection for database
         dbConn = new NpgsqlConnection(ConnectionString);

         //creating an SQL command
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = "select * from club where id = @id";

         cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

         //call the base method to get data
         var data = GetData(dbConn, cmd);

         if (data != null)
         {
            if (data.Read()) //every time loop runs it reads next like from fetched rows
            {
                    return new Club(Convert.ToInt32(data["id"]))
               {
                  // Cname = data["cname"].ToString(),
                  // LeagueID = (int)data["leagueid"],
                  // ManagerID = (int)data["managerid"],
                  // StadiumID = (int)data["stadiumid"],
                  // Logo = data["logo"].ToString()

                    Cname = data["cname"].ToString(),
                    LeagueID = (int)(data["leagueid"] != DBNull.Value ? (int?)data["leagueid"] : null),
                    ManagerID = (int)(data["managerid"] != DBNull.Value ? (int?)data["managerid"] : null),
                    StadiumID = data["stadiumid"] != DBNull.Value ? (int?)data["stadiumid"] : null,
                    Logo = data["logo"] != DBNull.Value ? data["logo"].ToString() : null

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
   public List<Club> GetClubs()
   {
      NpgsqlConnection dbConn = null;
      var clubs = new List<Club>();
      try
      {
         //create a new connection for database
         dbConn = new NpgsqlConnection(ConnectionString);

         //creating an SQL command
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = "select * from club";

         //call the base method to get data
         var data = GetData(dbConn, cmd);

         if (data != null)
         {
            while (data.Read()) //every time loop runs it reads next like from fetched rows
            {
               Club c = new Club(Convert.ToInt32(data["id"]))
               {
                  // Cname = data["cname"].ToString(),
                  // LeagueID = (int)data["leagueid"],
                  // ManagerID = (int)data["managerid"],
                  // StadiumID = (int)data["stadiumid"],
                  // Logo = data["logo"].ToString()

                   Cname = data["cname"].ToString(),
                    LeagueID = (int)(data["leagueid"] != DBNull.Value ? (int?)data["leagueid"] : null),
                    ManagerID = (int)(data["managerid"] != DBNull.Value ? (int?)data["managerid"] : null),
                    StadiumID = data["stadiumid"] != DBNull.Value ? (int?)data["stadiumid"] : null,
                    Logo = data["logo"] != DBNull.Value ? data["logo"].ToString() : null
               };

               clubs.Add(c);
            }
         }

         return clubs;
      }
      finally
      {
         dbConn?.Close();
      }
   }

   //add a new player
   public bool InsertClub(Club c)
   {
      NpgsqlConnection dbConn = null;
      try
      {
         dbConn = new NpgsqlConnection(ConnectionString);
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = @"
insert into club
(cname,leagueid, managerid, stadiumid, logo)
values
(@cname, @leagueid, @managerid, @stadiumid, @logo)
";

         //adding parameters in a better way
         cmd.Parameters.AddWithValue("@cname", NpgsqlDbType.Text, c.Cname);
         cmd.Parameters.AddWithValue("@leagueid", NpgsqlDbType.Integer, c.LeagueID);
         cmd.Parameters.AddWithValue("@managerid", NpgsqlDbType.Integer, c.ManagerID);
         cmd.Parameters.AddWithValue("@stadiumid", NpgsqlDbType.Integer, c.StadiumID);
         cmd.Parameters.AddWithValue("@logo", NpgsqlDbType.Text, c.Logo);


         //will return true if all goes well
         bool result = InsertData(dbConn, cmd);

         return result;
      }
      finally
      {
         dbConn?.Close();
      }
   }

   public bool UpdateClub(Club c)
   {
      var dbConn = new NpgsqlConnection(ConnectionString);
      var cmd = dbConn.CreateCommand();
      cmd.CommandText = @"
update club set
cname = @cname,
leagueid = @leagueid,
managerid = @managerid,
stadiumid = @stadiumid,
logo = @logo
where
id = @id";

         cmd.Parameters.AddWithValue("@cname", NpgsqlDbType.Text, c.Cname);
         cmd.Parameters.AddWithValue("@leagueid", NpgsqlDbType.Integer, c.LeagueID);
         cmd.Parameters.AddWithValue("@managerid", NpgsqlDbType.Integer, c.ManagerID);
         cmd.Parameters.AddWithValue("@stadiumid", NpgsqlDbType.Integer, c.StadiumID);
         cmd.Parameters.AddWithValue("@logo", NpgsqlDbType.Text, c.Logo);
         cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, c.Id);

      bool result = UpdateData(dbConn, cmd);
      return result;
   }

    public bool DeleteClub(int id)
    {
       var dbConn = new NpgsqlConnection(ConnectionString);
       var cmd = dbConn.CreateCommand();
       cmd.CommandText = @"
 delete from club
 where id = @id
 ";

       //adding parameters in a better way
       cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

       //will return true if all goes well
       bool result = DeleteData(dbConn, cmd);

       return result;
    }

    public bool DeleteClubSendErrorMessage(int id)
    {
        throw new NotImplementedException();
    }
}
