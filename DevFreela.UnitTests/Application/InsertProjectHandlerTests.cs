using DevFreela.Application.Commands;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositorys;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application;

public class InsertProjectHandlerTests
{
    [Fact]
    public async Task InputDataAreOk_Insert_Sucess_NSubstitute()
    {
        //Arrange
        const int ID = 1;
        var repository = Substitute.For<IProjectRepository>();
        repository.AddAsync(Arg.Any<Project>()).Returns(Task.FromResult(ID));
        var command = new CreateProjectCommand
        {
            Title = "titulo",
            Description = "descripcion",
            IdClient = 1,
            IdFreelancer = 2,
            TotalCost = 56000
        };
        var handler = new CreateProjectHandler(repository);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken()); 
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ID, result.Data);
        await repository.Received(ID).AddAsync(Arg.Any<Project>());
    }
    
    public async Task InputDataAreOk_Insert_Sucess_Moq()
    {
        //Arrange
        const int ID = 1;
        
        var repository = Mock.Of<IProjectRepository>(r => 
            r.AddAsync(It.IsAny<Project>()) == Task.FromResult(ID));
        
        var command = new CreateProjectCommand
        {
            Title = "titulo",
            Description = "descripcion",
            IdClient = 1,
            IdFreelancer = 2,
            TotalCost = 56000
        };
        var handler = new CreateProjectHandler(repository);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken()); 
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ID, result.Data);
        
        Mock.Get(repository).Verify(r => r.AddAsync(It.IsAny<Project>()), Times.Once);
    }
}