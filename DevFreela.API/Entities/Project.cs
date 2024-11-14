using DevFreela.API.Enums;

namespace DevFreela.API.Entities;

public class Project : BaseEntity
{
    public Project(string title, string description, int idClient, int idFreelancer, float totalCost) : base()
    {
        Title = title;
        Description = description;
        IdClient = idClient;
        IdFreelancer = idFreelancer;
        TotalCost = totalCost;
        Status = ProjectStatusEnum.Created;
        Comments = [];
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public int IdClient { get; private set; }
    public User Client { get; private set; }
    public int IdFreelancer { get; private set; }
    public User Freelancer { get; private set; }
    public float TotalCost { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? CompleteAt { get; private set; }
    public ProjectStatusEnum Status { get; private set; }
    public List<ProjectComment> Comments { get; private set; }

    public void Cancel()
    {
        if (Status is ProjectStatusEnum.InProgress or ProjectStatusEnum.Suspended)
            Status = ProjectStatusEnum.Cancelled;
    }

    public void Start()
    {
        if (Status is ProjectStatusEnum.Created)
            Status = ProjectStatusEnum.InProgress;
        StartDate = DateTime.Now; 
    }

    public void Complete()
    {
        if (Status is ProjectStatusEnum.PaymentPending or ProjectStatusEnum.InProgress)
            Status = ProjectStatusEnum.Completed;
        CompleteAt = DateTime.Now;
    }

    public void SetPaymentPending()
    {
        if (Status is ProjectStatusEnum.InProgress)
            Status = ProjectStatusEnum.PaymentPending;
    }

    public void Update(string title, string description, float totalCost)
    {
        Title = title;
        Description = description;
        TotalCost = totalCost;
    }
}   

