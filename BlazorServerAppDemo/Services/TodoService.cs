using BlazorServerAppDemo.Models;

namespace BlazorServerAppDemo.Services;

public class TodoService
{
    private readonly List<TodoItemModel> _todos = new();

    public IEnumerable<TodoItemModel> GetTodos() => _todos;

    public void AddTodoItem(TodoItemModel item)
    {
        item.Id = _todos.Count + 1;
        _todos.Add(item);
    }

    public void ToggleTodoItem(int id, bool isCompleted)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo != null)
        {
            todo.IsComplete = isCompleted;
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
