namespace BlazorServerAppDemo.UnitTests;

using Bunit;
using Xunit;
using BlazorServerAppDemo.Components.Pages;
using Microsoft.AspNetCore.Components;

public class TodoItemTests : TestContext
{
    [Fact]
    public void TodoItemRendersCorrectly()
    {
        // Arrange
        var title = "Test Todo";
        var isComplete = false;

        // Act
        var cut = RenderComponent<TodoItem>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.IsComplete, isComplete)
        );

        // Assert
        cut.MarkupMatches(@" <div>
                              <input type=""checkbox"" >
                              <span style=""text-decoration: none"">Test Todo</span>
                              <button class=""btn btn-sm btn-outline-danger pt-0 pb-0 ps-1 pe-1 ms-4"">x</button>
                            </div>");
    }

    [Fact]
    public void TodoItemCheckboxChangesState()
    {
        // Arrange
        var isComplete = false;

        // Act
        var cut = RenderComponent<TodoItem>(parameters => parameters
            .Add(p => p.IsComplete, isComplete)
            .Add(p => p.OnCompleteChanged, EventCallback.Factory.Create<bool>(this, value => isComplete = value))
        );

        // Act
        cut.Find("input").Change(true);

        // Assert
        Assert.True(isComplete);
    }

    [Fact]
    public void TodoItemRemoveButtonCallsOnRemove()
    {
        // Arrange
        var removed = false;

        // Act
        var cut = RenderComponent<TodoItem>(parameters => parameters
            .Add(p => p.OnRemove, EventCallback.Factory.Create(this, () => removed = true))
        );

        // Act
        cut.Find("button").Click();

        // Assert
        Assert.True(removed);
    }
}
