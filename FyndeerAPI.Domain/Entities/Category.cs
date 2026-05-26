namespace FyndeerAPI.Domain.Entities;

public class Category
{
    public int Id { get; private set; }
    public string Slug { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;

    private Category() { }

    public static Category Create(string slug, string name)
    {
        return new Category
        {
            Slug = slug,
            Name = name
        };
    }

    public void Update(string slug, string name)
    {
        Slug = slug;
        Name = name;
    }
}
