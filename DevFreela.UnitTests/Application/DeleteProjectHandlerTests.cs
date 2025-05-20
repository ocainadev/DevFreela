using DevFreela.Application.Commands;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositorys;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application;

public class DeleteProjectHandlerTests
{
    [Fact]
    public async Task ProjectExists_Delete_Sucess_NSubstitute()
    {
        //Arrange
        const int ID = 1;
        var project = new Project("Projeto", "Descricao", 1, 2, 5000);
        var repository = Substitute.For<IProjectRepository>();
        repository.GetByIdAsync(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
        repository.UpdateAsync(Arg.Any<Project>()).Returns(Task.CompletedTask);
        var handler = new DeleteProjectHandler(repository);
        var command = new DeleteProjectCommand(ID);
        
        // Act
        var result = await handler.Handle(command,new CancellationToken());
        
        //Assert
        Assert.True(result.IsSuccess);
        await repository.Received(ID).GetByIdAsync(ID);
        await repository.Received(ID).UpdateAsync(Arg.Any<Project>());
    }

    [Fact]
    public async Task ProjectDoesNotExists_Delete_Error_NSubstitute()
    {
        //Arrange
        var repository = Substitute.For<IProjectRepository>();
        repository.GetByIdAsync(Arg.Any<int>()).Returns(Task.FromResult((Project?)null));
        var handler = new DeleteProjectHandler(repository);
        var command = new DeleteProjectCommand(1);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);
    }
    
    [Fact]
    public async Task ProjectExists_Delete_Sucess_Moq()
    {
        //Arrange
        const int ID = 1;
        var project = new Project("Projeto", "Descricao", 1, 2, 5000);
        
        var repository = Mock.Of<IProjectRepository>(p => 
            p.GetByIdAsync(It.IsAny<int>()) == Task.FromResult(project) &&
            p.UpdateAsync(It.IsAny<Project>()) == Task.CompletedTask);
        
        var handler = new DeleteProjectHandler(repository);
        var command = new DeleteProjectCommand(ID);
        
        // Act
        var result = await handler.Handle(command,new CancellationToken());
        
        //Assert
        Assert.True(result.IsSuccess);
        Mock.Get(repository).Verify(p => p.GetByIdAsync(ID), Times.Once) ;
        Mock.Get(repository).Verify(p => p.UpdateAsync(It.IsAny<Project>()), Times.Once) ;
    }
    
    [Fact]
    public async Task ProjectDoesNotExists_Delete_Error_Moq()
    {
        //Arrange
        
        var repository = Mock.Of<IProjectRepository>(r => 
            r.GetByIdAsync(It.IsAny<int>()) == Task.FromResult((Project?)null));
        
        var handler = new DeleteProjectHandler(repository);
        var command = new DeleteProjectCommand(1);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);
        
        Mock.Get(repository).Verify(p => p.GetByIdAsync(It.IsAny<int>()), Times.Once);
        Mock.Get(repository).Verify(p => p.UpdateAsync(It.IsAny<Project>()), Times.Never);
    }
}