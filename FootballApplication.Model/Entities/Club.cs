using System;
using System.Data.Common;

namespace FootballApplication.Model.Entities;

public class Club
{

      public Club(int id) {
        Id = id;
    }

    public int Id { get; set; }
    public string Cname { get; set; }
    public int LeagueID { get; set; }
    public int ManagerID { get; set; }
    public int? StadiumID { get; set; }

    public string Logo { get; set; }
}