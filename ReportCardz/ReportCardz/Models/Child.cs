namespace ReportCardz.Models;

public class Child
{
    public required string Name { get; set; }
    public IDictionary<string, char> Courses { get; set; } = new Dictionary<string, char>();
}