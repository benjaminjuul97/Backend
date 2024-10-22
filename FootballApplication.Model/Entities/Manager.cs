using System;
using System.Data.Common;

namespace FootballApplication.Model.Entities;

public class Manager
{
    public Manager(int id) {
        Id = id;
    }

    public int Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }
    public int Age { get; set; }
    public int ExperienceYears { get; set; }
    public int CountryID { get; set; }

    public int ClubID { get; set; }

}