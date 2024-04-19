namespace MovieMinimalAPI;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? ReleaseYear { get; set; }
    public DateTime CreateDate { get; set; }
}
public class Actor
{
    public int Id { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime ModificationDate { get; set; }
}

public class Director
{
    public int Id { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime ModificationDate { get; set; }
}
public class perform
{
    public int Id { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime ModificationDate { get; set; }
}
public class users
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime ModificationDate { get; set; }
}