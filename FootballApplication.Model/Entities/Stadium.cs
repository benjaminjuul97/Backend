using System;
using System.Data.Common;

namespace FootballApplication.Model.Entities;

public class Stadium
{

    public Stadium(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
    public string Sname { get; set; }

    public string Slocation { get; set; }
    public int Capacity { get; set; }

    public int ClubID { get; set; }

    public string Image { get; set; }

}