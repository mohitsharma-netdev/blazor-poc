
using Bunit;
using Moq;
using BlazorServerAppDemo.Components.Pages;
using BlazorServerAppDemo.Models;
using BlazorServerAppDemo.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorServerAppDemo.UnitTests;

public class TodoListTests : TestContext
{
    

    [Fact]
    public async Task TodoListRendersCorrectly()
    {
        // Arrange
        var mockService = new Mock<ITodoService>();
        mockService.Setup(service => service.GetTodos()).Returns(new List<TodoItemModel>
        {
            new TodoItemModel { Id = 1, Title = "Test Todo 1", IsComplete = false },
            new TodoItemModel { Id = 2, Title = "Test Todo 2", IsComplete = true }
        });

        Services.AddSingleton(mockService.Object);

        // Act
        var cut = RenderComponent<TodoList>();

        // Assert
        cut.MarkupMatches(@"
                            <h3>Todo List</h3>
                            <p class=""text-secondary"">
                              Below input text box has a databinding using @bind attribute.     This enables two way databinding between the input control and the ""textNewTodo"" property.
                            </p>
                            <p>
                              This setup demonstrates:      Component Architecture: Creating and routing between components with data-binding.     Service Layer: Implementing a service for managing Todo items.     Dependency Injection: Registering and injecting services.
                            </p>
                            <hr>
                            <h3>Todo List</h3>
                            <input placeholder=""New todo"" >
                            <button  class=""btn btn-sm btn-primary"">Add</button>
                            <div>
                              <input type=""checkbox"" >
                              <span style=""text-decoration: none"">Test Todo 1</span>
                              <button  class=""btn btn-sm btn-outline-danger pt-0 pb-0 ps-1 pe-1 ms-4"">x</button>
                            </div>
                            <div>
                              <input type=""checkbox"" checked="""" >
                              <span style=""text-decoration: line-through"">Test Todo 2</span>
                              <button  class=""btn btn-sm btn-outline-danger pt-0 pb-0 ps-1 pe-1 ms-4"">x</button>
                            </div>
");
    }

    [Fact]
    public async Task TodoListAddsNewItem()
    {
        // Arrange
        var mockService = new Mock<ITodoService>();
        mockService.Setup(service => service.GetTodos()).Returns(new List<TodoItemModel>());
        mockService.Setup(service => service.AddTodoItem(It.IsAny<TodoItemModel>()));

        Services.AddSingleton(mockService.Object);

        var cut = RenderComponent<TodoList>();

        // Act
        cut.Find("input").Change("New Todo");
        cut.Find("button").Click();

        // Assert
        mockService.Verify(service => service.AddTodoItem(It.Is<TodoItemModel>(item => item.Title == "New Todo")), Times.Once);
    }

    //[Fact]
    //public async Task TodoListRemovesItem()
    //{
    //    // Arrange
    //    var mockService = new Mock<ITodoService>();
    //    mockService.Setup(service => service.GetTodos()).Returns(new List<TodoItemModel>
    //    {
    //        new TodoItemModel { Id = 1, Title = "Test Todo 1", IsComplete = false }
    //    });
    //    mockService.Setup(service => service.RemoveTodoItem(It.IsAny<int>()));

    //    Services.AddSingleton(mockService.Object);

    //    var cut = RenderComponent<TodoList>();

    //    // Act
    //    cut.Find("button").Click();

    //    // Assert
    //    mockService.Verify(service => service.RemoveTodoItem(It.Is<int>(id => id == 1)), Times.Once);
    //}
}
