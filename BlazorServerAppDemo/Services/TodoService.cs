using BlazorServerAppDemo.Models;

namespace BlazorServerAppDemo.Services;

public class TodoService
{
    private readonly List<TodoItem> _todos = new();

    public IEnumerable<TodoItem> GetTodos() => _todos;

    public void AddTodoItem(TodoItem item)
    {
        item.Id = _todos.Count + 1;
        _todos.Add(item);
    }

    public void ToggleTodoItem(int id, bool isCompleted)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo != null)
        {
            todo.IsCompleted = isCompleted;
        }
    }

    public void RemoveTodoItem(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo != null)
        {
            _todos.Remove(todo);
        }
    }
}
