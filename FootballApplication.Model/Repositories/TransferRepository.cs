using System;
using FootballApplication.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;


namespace FootballApplication.Model.Repositories;

public class TransferRepository : BaseRepository
{
   public TransferRepository(IConfiguration configuration) : base(configuration)
   {
   }

   public Transfer GetTransferById(int id)
   {
      NpgsqlConnection dbConn = null;
      try
      {
         //create a new connection for database
         dbConn = new NpgsqlConnection(ConnectionString);

         //creating an SQL command
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = "select * from transfer where id = @id";

         cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

         //call the base method to get data
         var data = GetData(dbConn, cmd);

         if (data != null)
         {
            if (data.Read()) //every time loop runs it reads next like from fetched rows
            {
// #pragma warning disable CS8601 // Possible null reference assignment.
                    return new Transfer(Convert.ToInt32(data["id"]))
                    {
                        TransferFee = (int)data["transferfee"],
                        TransferDate = DateOnly.Parse(data["transferdate"].ToString()),
                        ToClubID = (int)data["toclubid"],
                        FromClubID = (int)data["fromclubid"],
                        PlayerID = (int)data["playerid"]
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
   public List<Transfer> GetTransfers()
   {
      NpgsqlConnection dbConn = null;
      var transfers = new List<Transfer>();
      try
      {
         //create a new connection for database
         dbConn = new NpgsqlConnection(ConnectionString);

         //creating an SQL command
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = "select * from transfer";

         //call the base method to get data
         var data = GetData(dbConn, cmd);

         if (data != null)
         {
            while (data.Read()) //every time loop runs it reads next like from fetched rows
            {
               Transfer t = new Transfer(Convert.ToInt32(data["id"]))
               {
                     TransferFee = (int)data["transferfee"],
                     TransferDate = DateOnly.Parse(data["transferdate"].ToString()),
                     ToClubID = (int)data["toclubid"],
                     FromClubID = (int)data["fromclubid"],
                     PlayerID = (int)data["playerid"]
               };

               transfers.Add(t);
            }
         }

         return transfers;
      }
      finally
      {
         dbConn?.Close();
      }
   }

   //add a new player
   public bool InsertTransfer(Transfer t)
   {
      NpgsqlConnection dbConn = null;
      try
      {
         dbConn = new NpgsqlConnection(ConnectionString);
         var cmd = dbConn.CreateCommand();
         cmd.CommandText = @"
insert into transfer
(transferfee, transferdate, toclubid, fromclubid, playerid)
values
(@transferfee, @transferdate, @toclubid, @fromclubid, @playerid)";

         //adding parameters in a better way
         cmd.Parameters.AddWithValue("@transferfee", NpgsqlDbType.Integer, t.TransferFee);
         cmd.Parameters.AddWithValue("@transferdate", NpgsqlDbType.Date, t.TransferDate);
         cmd.Parameters.AddWithValue("@toclubid", NpgsqlDbType.Integer, t.ToClubID);
         cmd.Parameters.AddWithValue("@fromclubid", NpgsqlDbType.Integer, t.FromClubID);
         cmd.Parameters.AddWithValue("@playerid", NpgsqlDbType.Integer, t.PlayerID);

         //will return true if all goes well
         bool result = InsertData(dbConn, cmd);

         return result;
      }
      finally
      {
         dbConn?.Close();
      }
   }

   public bool UpdateTransfer(Transfer t)
   {
      var dbConn = new NpgsqlConnection(ConnectionString);
      var cmd = dbConn.CreateCommand();
      cmd.CommandText = @"
update transfer set
transferfee = @transferfee,
transferdate = @transferdate,
toclubid = @toclubid,
fromclubid = @fromclubid,
playerid = @playerid
where
id = @id";

         cmd.Parameters.AddWithValue("@transferfee", NpgsqlDbType.Integer, t.TransferFee);
         cmd.Parameters.AddWithValue("@transferdate", NpgsqlDbType.Date, t.TransferDate);
         cmd.Parameters.AddWithValue("@toclubid", NpgsqlDbType.Integer, t.ToClubID);
         cmd.Parameters.AddWithValue("@fromclubid", NpgsqlDbType.Integer, t.FromClubID);
         cmd.Parameters.AddWithValue("@playerid", NpgsqlDbType.Integer, t.PlayerID);
         cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, t.Id);


      bool result = UpdateData(dbConn, cmd);
      return result;
   }

    public bool DeleteTransfer(int id)
    {
       var dbConn = new NpgsqlConnection(ConnectionString);
       var cmd = dbConn.CreateCommand();
       cmd.CommandText = @"
 delete from transfer
 where id = @id
 ";

       //adding parameters in a better way
       cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

       //will return true if all goes well
       bool result = DeleteData(dbConn, cmd);

       return result;
    }

    public bool Deletetransfer(int id)
    {
        throw new NotImplementedException();
    }
}
