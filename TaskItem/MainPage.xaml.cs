using TaskManager.Models;

namespace TaskManager;

public partial class MainPage : ContentPage
{
    private readonly List<MyTaskItems> _tasks = new();
    private int _nextId = 1;

    public MainPage()
    {
        InitializeComponent();

        PriorityPicker.ItemsSource = new List<string> { "Низкий", "Средний", "Высокий" };
        PriorityPicker.SelectedIndex = 1;   // по умолчанию «Средний»

        LoadTestData();
        RefreshList();
    }
    // Переключение темы вручную
    private void OnDarkThemeToggled(object? sender, ToggledEventArgs e)
    {
        if (Application.Current is null)
            return;

        Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
    }
    // Временная коллекция тестовых задач
    private void LoadTestData()
    {
        _tasks.Add(new MyTaskItems { Id = _nextId++, Title = "Изучить XAML",
            Priority = TaskPriority.Low, CreatedAt = DateTime.Now.AddDays(-2), IsCompleted = true });
        _tasks.Add(new MyTaskItems { Id = _nextId++, Title = "Выполнить практическую работу",
            Priority = TaskPriority.High, CreatedAt = DateTime.Now });
        _tasks.Add(new MyTaskItems { Id = _nextId++, Title = "Повторить C#",
            Priority = TaskPriority.Medium, CreatedAt = DateTime.Now.AddDays(-1) });
        _tasks.Add(new MyTaskItems { Id = _nextId++, Title = "Подготовить проект",
            Priority = TaskPriority.Low, CreatedAt = DateTime.Now });
    
    }

    // Обновление списка с учётом переключателя
    private void RefreshList()
    {
        TasksView.ItemsSource = HideCompletedSwitch.IsToggled
            ? _tasks.Where(t => !t.IsCompleted).ToList()
            : _tasks.ToList();
    }

    // Добавление новой задачи
    private void OnAddClicked(object? sender, EventArgs e)
    {
        var title = TaskEntry.Text?.Trim();
        if (string.IsNullOrEmpty(title))
            return;

        _tasks.Add(new MyTaskItems
        {
            Id = _nextId++,
            Title = title,
            Priority = (TaskPriority)PriorityPicker.SelectedIndex,
            CreatedAt = DateTime.Now
        });

        TaskEntry.Text = string.Empty;
        RefreshList();
    }

    // Переключатель «Скрыть выполненные»
    private void OnHideCompletedToggled(object? sender, ToggledEventArgs e)
    {
        
        RefreshList();
    }
}