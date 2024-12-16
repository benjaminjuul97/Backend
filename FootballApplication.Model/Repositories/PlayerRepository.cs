using System;
using FootballApplication.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;


namespace FootballApplication.Model.Repositories;

public class PlayerRepository : BaseRepository
{
   public PlayerRepository(IConfiguration configuration) : base(configuration)
   {
   }

   public Player GetPlayerById(int id)
   {
      NpgsqlConnection dbConn = null;
      try
      {
         //create a new connection for database
         dbConn = new NpgsqlConnection(ConnectionString);

         //creating an SQL command
         var cmd = dbConn.CreateCommand();
         //cmd.CommandText = "select * from player where id = @id";
         //cmd.CommandText = "SELECT player.*, country.flag, club.logo FROM player JOIN country ON player.countryid = country.id and JOIN club ON player.clubid = club.id WHERE player.id = @id";
         cmd.CommandText = @"
            SELECT player.*, country.flag, club.logo
            FROM player
            JOIN country ON player.countryid = country.id
            JOIN club ON player.clubid = club.id
            WHERE player.id = @id";



         cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

         //call the base method to get data
         var data = GetData(dbConn, cmd);

         if (data != null)
         {
            if (data.Read()) //every time loop runs it reads next like from fetched rows
            {

                    return new Player(Convert.ToInt32(data["id"]))
               {
                  Firstname = data["firstName"].ToString(),
                  Lastname = data["lastname"].ToString(),
                  Age = (int)data["age"],
                  Pposition = data["pposition"].ToString(),
                  ClubID = (int)data["clubid"],
                  CountryID = (int)data["countryid"],
                  Image = data["image"].ToString(),
                  Flag = data["flag"].ToString(),
                  Logo = data["logo"].ToString()
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
   public List<Player> GetPlayers()
   {
      NpgsqlConnection dbConn = null;
      var players = new List<Player>();
      try
      {
         //create a new connection for database
         dbConn = new NpgsqlConnection(ConnectionString);

         //creating an SQL command
         var cmd = dbConn.CreateCommand();
         //cmd.CommandText = "select * from player";
         //cmd.CommandText = "SELECT player.*, country.flag FROM player JOIN country ON player.countryid = country.id";
         cmd.CommandText = "SELECT player.*, country.flag, club.logo FROM player JOIN country ON player.countryid = country.id JOIN club ON player.clubid = club.id";

         //call the base method to get data
         var data = GetData(dbConn, cmd);

         if (data != null)
         {
            while (data.Read()) //every time loop runs it reads next like from fetched rows
            {
               Player p = new Player(Convert.ToInt32(data["id"]))
               {
                  Firstname = data["firstname"].ToString(),
                  Lastname = data["lastname"].ToString(),
                  Age = (int)data["age"],
                  Pposition = data["pposition"].ToString(),
                  ClubID = (int)data["clubid"],
                  CountryID = (int)data["countryid"],
                  Image = data["image"].ToString(),
                  Flag = data["flag"].ToString(),
                  Logo = data["logo"].ToString()

               };

               players.Add(p);
            }
         }

         return players;
      }
      finally
      {
         dbConn?.Close();
      }
   }

   //add a new player
   public bool InsertPlayer(Player p)
   {
      NpgsqlConnection dbConn = null;
      try
      {
         dbConn = new NpgsqlConnection(ConnectionString);
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = @"
insert into Player
(firstname, lastname, age, pposition, clubid, countryid, image)
values
(@firstname, @lastname, @age, @pposition, @clubid, @countryid, @image)
";

         //adding parameters in a better way
         cmd.Parameters.AddWithValue("@firstname", NpgsqlDbType.Text, p.Firstname);
         cmd.Parameters.AddWithValue("@lastname", NpgsqlDbType.Text, p.Lastname);
         cmd.Parameters.AddWithValue("@age", NpgsqlDbType.Integer, p.Age);
         cmd.Parameters.AddWithValue("@pposition", NpgsqlDbType.Text, p.Pposition);
         cmd.Parameters.AddWithValue("@clubid", NpgsqlDbType.Integer, p.ClubID);
         cmd.Parameters.AddWithValue("@countryid", NpgsqlDbType.Integer, p.CountryID);
         cmd.Parameters.AddWithValue("@image", NpgsqlDbType.Text, p.Image);



         //will return true if all goes well
         bool result = InsertData(dbConn, cmd);

         return result;
      }
      finally
      {
         dbConn?.Close();
      }
   }

   public bool UpdatePlayer(Player p)
   {
      var dbConn = new NpgsqlConnection(ConnectionString);
      var cmd = dbConn.CreateCommand();
      cmd.CommandText = @"
update player set
firstname = @firstname,
lastname = @lastname,
age = @age,
pposition = @pposition,
clubid = @clubid,
countryid = @countryid,
image = @image
where
id = @id";

         cmd.Parameters.AddWithValue("@firstname", NpgsqlDbType.Text, p.Firstname);
         cmd.Parameters.AddWithValue("@lastname", NpgsqlDbType.Text, p.Lastname);
         cmd.Parameters.AddWithValue("@age", NpgsqlDbType.Integer, p.Age);
         cmd.Parameters.AddWithValue("@pposition", NpgsqlDbType.Text, p.Pposition);
         cmd.Parameters.AddWithValue("@clubid", NpgsqlDbType.Integer, p.ClubID);
         cmd.Parameters.AddWithValue("@countryid", NpgsqlDbType.Integer, p.CountryID);
         cmd.Parameters.AddWithValue("@image", NpgsqlDbType.Text, p.Image);
         cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, p.Id);

      bool result = UpdateData(dbConn, cmd);
      return result;
   }

    public bool DeletePlayer(int id)
    {
       var dbConn = new NpgsqlConnection(ConnectionString);
       var cmd = dbConn.CreateCommand();
       cmd.CommandText = @"
 delete from player
 where id = @id
 ";

       //adding parameters in a better way
       cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

       //will return true if all goes well
       bool result = DeleteData(dbConn, cmd);

       return result;
    }

    public bool Deleteplayer(int id)
    {
        throw new NotImplementedException();
    }
}
