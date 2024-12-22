using DevFreela.Core.Entities;

namespace DevFreela.Application.Models;

public class UserViewModel
{
    public UserViewModel(string fullName, string email, DateTime birthDate)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
    }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }

    public static UserViewModel FromEntity(User entity)
    {
        return new UserViewModel(entity.FullName, entity.Email, entity.BirthDate);
    }
}