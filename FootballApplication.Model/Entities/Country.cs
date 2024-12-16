using System;
using System.Data.Common;

namespace FootballApplication.Model.Entities;

public class Country
{
    public Country(int id) {
        Id = id;
    }

    public int Id { get; set; }
    public string Cname { get; set; }

    public string Flag { get; set; }

}