using System;
using FootballApplication.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;


namespace FootballApplication.Model.Repositories;

public class StadiumRepository : BaseRepository
{
   public StadiumRepository(IConfiguration configuration) : base(configuration)
   {
   }

   public Stadium GetStadiumByID(int id)
   {
      NpgsqlConnection dbConn = null;
      try
      {
         //create a new connection for database
         dbConn = new NpgsqlConnection(ConnectionString);

         //creating an SQL command
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = "select * from stadium where id = @id";

         cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

         //call the base method to get data
         var data = GetData(dbConn, cmd);

         if (data != null)
         {
            if (data.Read()) //every time loop runs it reads next like from fetched rows
            {
                    return new Stadium(Convert.ToInt32(data["id"]))
               {
                  Sname = data["sname"].ToString(),
                  Slocation = data["slocation"].ToString(),
                  Capacity = (int)data["capacity"],
                  ClubID = (int)data["clubid"],
                  Image = data["image"].ToString()
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
   public List<Stadium> GetStadiums()
   {
      NpgsqlConnection dbConn = null;
      var stadiums = new List<Stadium>();
      try
      {
         //create a new connection for database
         dbConn = new NpgsqlConnection(ConnectionString);

         //creating an SQL command
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = "select * from stadium";

         //call the base method to get data
         var data = GetData(dbConn, cmd);

         if (data != null)
         {
            while (data.Read()) //every time loop runs it reads next like from fetched rows
            {
               Stadium s = new Stadium(Convert.ToInt32(data["id"]))
               {
                  Sname = data["sname"].ToString(),
                  Slocation = data["slocation"].ToString(),
                  Capacity = (int)data["capacity"],
                  ClubID = (int)data["clubid"],
                  Image = data["image"].ToString()
               };

               stadiums.Add(s);
            }
         }

         return stadiums;
      }
      finally
      {
         dbConn?.Close();
      }
   }

   //add a new player
   public bool InsertStadiums(Stadium s)
   {
      NpgsqlConnection dbConn = null;
      try
      {
         dbConn = new NpgsqlConnection(ConnectionString);
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = @"
insert into stadium
(sname, slocation, capacity, clubid, image)
values
(@sname, @slocation, @capacity, @clubid, @image)
";

         //adding parameters in a better way
         cmd.Parameters.AddWithValue("@sname", NpgsqlDbType.Text, s.Sname);
         cmd.Parameters.AddWithValue("@slocation", NpgsqlDbType.Text, s.Slocation);
         cmd.Parameters.AddWithValue("@capacity", NpgsqlDbType.Integer, s.Capacity);
         cmd.Parameters.AddWithValue("@clubid", NpgsqlDbType.Integer, s.ClubID);
         cmd.Parameters.AddWithValue("@image", NpgsqlDbType.Text, s.Image);



         //will return true if all goes well
         bool result = InsertData(dbConn, cmd);

         return result;
      }
      finally
      {
         dbConn?.Close();
      }
   }

   public bool UpdateStadium(Stadium s)
   {
      var dbConn = new NpgsqlConnection(ConnectionString);
      var cmd = dbConn.CreateCommand();
      cmd.CommandText = @"
update Stadium set
sname = @sname,
slocation = @slocation,
capacity = @capacity,
clubid = @clubid,
image = @image
where
id = @id";

         cmd.Parameters.AddWithValue("@sname", NpgsqlDbType.Text, s.Sname);
         cmd.Parameters.AddWithValue("@slocation", NpgsqlDbType.Text, s.Slocation);
         cmd.Parameters.AddWithValue("@capacity", NpgsqlDbType.Integer, s.Capacity);
         cmd.Parameters.AddWithValue("@clubid", NpgsqlDbType.Integer, s.ClubID);
         cmd.Parameters.AddWithValue("@image", NpgsqlDbType.Text, s.Image);
         cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, s.Id);



      bool result = UpdateData(dbConn, cmd);
      return result;
   }

    public bool DeleteStadium(int id)
    {
       var dbConn = new NpgsqlConnection(ConnectionString);
       var cmd = dbConn.CreateCommand();
       cmd.CommandText = @"
 delete from stadium
 where id = @id
 ";

       //adding parameters in a better way
       cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

       //will return true if all goes well
       bool result = DeleteData(dbConn, cmd);

       return result;
    }

    public bool Deletestadium(int id)
    {
        throw new NotImplementedException();
    }
}
