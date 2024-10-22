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
    public string Name { get; set; }

    public string Location { get; set; }
    public int Capacity { get; set; }

    public int ClubID { get; set; }

}