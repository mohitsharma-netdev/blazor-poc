using FrontEndApi.Models;
using FrontendWebApi.Data;
using FrontEndWebAPI.Controllers;
using FrontEndWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontEndWebAPI.UnitTests;
 
public class TodoControllerTests : IDisposable
{
    private readonly TodoContext _context;
    private readonly TodoController _controller;

    public TodoControllerTests()
    {
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new TodoContext(options);
        _controller = new TodoController(_context);
    }

    public void Initialize()
    {
        // Clear the database
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        // Seed the database with test data
        _context.TodoItems.Add(new TodoItem { Id = 1, Name = "Test Todo 1", IsComplete = false });
        _context.TodoItems.Add(new TodoItem { Id = 2, Name = "Test Todo 2", IsComplete = true });
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task GetTodoItems_ReturnsAllTodoItems()
    {
        Initialize();
        
        // Act
        var result = await _controller.GetTodoItems();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse>(okResult.Value);
        var todoItems = Assert.IsType<List<TodoItem>>(apiResponse.Data);
        Assert.Equal(2, todoItems.Count);
    }



    [Fact]
    public async Task GetTodoItem_ReturnsTodoItem()
    {
        Initialize();
        
        // Act
        var result = await _controller.GetTodoItem(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse>(okResult.Value);
        var todoItem = Assert.IsType<TodoItem>(apiResponse.Data);
        Assert.Equal(1, todoItem.Id);
    }


    [Fact]
    public async Task PostTodoItem_CreatesTodoItem()
    {
        Initialize();
        
        // Arrange
        var newTodoItem = new TodoItem { Id = 3, Name = "Test Todo 3", IsComplete = false };

        // Act
        var result = await _controller.PostTodoItem(newTodoItem);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var todoItem = Assert.IsType<TodoItem>(createdResult.Value);
        Assert.Equal(3, todoItem.Id);
    }

    [Fact]
    public async Task PutTodoItem_UpdatesTodoItem()
    {
        Initialize();
        
        // Arrange
        var updatedTodoItem = new TodoItem { Id = 1, Name = "Updated Todo", IsComplete = true };

        // Act
        var result = await _controller.PutTodoItem(1, updatedTodoItem);

        // Assert
        Assert.IsType<NoContentResult>(result);
        var todoItem = _context.TodoItems.Find(1);
        Assert.Equal("Updated Todo", todoItem.Name);
        Assert.True(todoItem.IsComplete);
    }

    [Fact]
    public async Task DeleteTodoItem_DeletesTodoItem()
    {
        Initialize();
        
        // Act
        var result = await _controller.DeleteTodoItem(1);

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Null(_context.TodoItems.Find(1));
    }


}
