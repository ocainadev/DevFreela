namespace DevFreela.API.Entities;

public abstract class BaseEntity
{
    public int Id { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public bool IsDeleted { get; private set; } = false;

    public void SetIsDeleted() 
        => IsDeleted = true;
}