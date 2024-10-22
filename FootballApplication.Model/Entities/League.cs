using System;
using System.Data.Common;

namespace FootballApplication.Model.Entities;

public class League
{
    public League(int id) {
        Id = id;
        // Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public int CountryID { get; set; }

}