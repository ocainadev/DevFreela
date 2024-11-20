namespace DevFreela.Application.Models;

public class UpdateProjectInputModel
{
    public int IdProject { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public float TotalCost { get; set; }
}