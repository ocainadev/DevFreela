using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Core;

public class ProjectTests
{
    [Fact]
    public void ProjectIsCreated_Start_Sucess()
    {
        // Arrange
        var project = new Project("Projeto", "Descricao", 1, 2, 5000);
        // Act
        project.Start();
        
        // Assert
        Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
        Assert.NotNull(project.StartDate);
    }
    
    [Fact]
    public void ProjectIsInInvalidState_Start_ThrowsException()
    {
        // Arrange
        var project = new Project("Projeto", "Descricao", 1, 2, 5000);
        project.Start();
        
        // Act + Assert
        var start = project.Start;
        
        var exception = Assert.Throws<InvalidOperationException>(start);
        Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);
    }
    
}