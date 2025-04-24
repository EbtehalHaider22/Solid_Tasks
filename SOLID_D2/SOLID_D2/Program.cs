using System;
using System.Collections.Generic;
using System.Text;

// Interfaces split to follow Interface Segregation Principle
public interface ITaskCreator
{
    void CreateSubTask();
}

public interface ITaskAssigner
{
    void AssignTask();
}

public interface ITaskWorker
{
    void WorkOnTask();
}

// Developer class
public class Developer
{
    public string Name { get; set; }
}

// Custom task class to avoid conflict with System.Threading.Tasks.Task
public class ProjectTask
{
    public string Title { get; set; }
    public string Description { get; set; }

    public void AssignTo(Developer developer)
    {
        Console.WriteLine($"Task '{Title}' assigned to {developer.Name}");
    }
}

// TeamLead can create, assign, and work on tasks
public class TeamLead : ITaskCreator, ITaskAssigner, ITaskWorker
{
    public void AssignTask()
    {
        ProjectTask task = new ProjectTask
        {
            Title = "Merge and Deploy",
            Description = "Task to merge and deploy sharing feature to develop"
        };

        task.AssignTo(new Developer { Name = "Developer1" });
    }

    public void CreateSubTask()
    {
        Console.WriteLine("Subtask created.");
    }

    public void WorkOnTask()
    {
        Console.WriteLine("Working on task.");
    }
}

// Manager only assigns tasks
public class Manager : ITaskAssigner
{
    public void AssignTask()
    {
        Console.WriteLine("Manager assigned a task.");
    }
}
/*--------------------------------------------------------------------------*/
// Interface for SQL file abstraction
public interface ISqlFile
{
    string LoadText();
    void SaveText();
}

// Writable SQL file
public class SqlFile : ISqlFile
{
    public string FilePath { get; set; }
    public string FileText { get; set; }

    public string LoadText()
    {
        Console.WriteLine($"Loading from {FilePath}");
        return FileText;
    }

    public void SaveText()
    {
        Console.WriteLine($"Saving to {FilePath}");
    }
}

// Read-only SQL file
public class ReadOnlySqlFile : ISqlFile
{
    public string FilePath { get; set; }
    public string FileText { get; set; }

    public string LoadText()
    {
        Console.WriteLine($"Loading from read-only {FilePath}");
        return FileText;
    }

    public void SaveText()
    {
        Console.WriteLine($"Cannot save to read-only file: {FilePath}");
        // Gracefully skip saving instead of throwing an exception
    }
}

// File manager that works with any ISqlFile
public class SqlFileManager
{
    public List<ISqlFile> SqlFiles { get; set; }

    public string GetTextFromFiles()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var file in SqlFiles)
        {
            sb.Append(file.LoadText());
        }
        return sb.ToString();
    }

    public void SaveTextIntoFiles()
    {
        foreach (var file in SqlFiles)
        {
            file.SaveText(); // Safe for both read/write and read-only
        }
    }
}
