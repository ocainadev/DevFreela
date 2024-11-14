﻿using DevFreela.API.Entities;

namespace DevFreela.API.Models;

public class CreateProjectInputModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int IdClient { get; set; }
    public int IdFreelancer { get; set; }
    public float TotalCost { get; set; }

    public Project ToEntity()
     => new Project(Title, Description, IdClient, IdFreelancer, TotalCost);
}