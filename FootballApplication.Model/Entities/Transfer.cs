using System;
using System.Data.Common;

namespace FootballApplication.Model.Entities;

public class Transfer
{
    public Transfer(int id) {
        Id = id;
    }

    public int Id { get; set; }
    public int FromClubID { get; set; }

    public int ToClubID { get; set; }
    public DateOnly TransferDate { get; set; }

    public int TransferFee { get; set; }

    public int PlayerID { get; set; }

}