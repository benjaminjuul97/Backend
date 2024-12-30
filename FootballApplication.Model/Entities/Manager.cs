using System;
using System.Data.Common;

namespace FootballApplication.Model.Entities;

public class Manager
{
    public Manager(int id) {
        Id = id;
    }

    public int Id { get; set; }
    public string Firstname { get; set; }

    public string Lastname { get; set; }
    public int? CountryID { get; set; }

    public int? ClubID { get; set; }

    public string? Image { get; set; }

    public DateTime Dob { get; set; }

    public string Flag { get; set;}

    public string Logo { get; set; }



}