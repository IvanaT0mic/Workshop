namespace AnimalShelter.Jan.assoziations;

class Book(string title, string author, string genre)
{
    public string Title { get; set; } = title;  

    public string Author { get; set; } = author;

    public string Genre { get; set; } = genre;
}

class Library(int capacity = 100, int squareMeeters = 50)
{
    private List<Book> Books { get; set; } = [];

    private int Capacity { get; set; } = capacity;

    private int SquareMeters { get; set; } = squareMeeters;

    public void AddBook(Book book)
    {
        Books.Add(book);
        Console.WriteLine($"Added book: {book.Title} by {book.Author}");
    }

    public void ShowBooks()
    {
        Console.WriteLine("Books in the library:");
        foreach (var book in Books)
        {
            Console.WriteLine($"{book.Title} by {book.Author}");
        }
    }

    public void RemoveBook(Book book)
    {
        if (Books.Contains(book))
        {
            Books.Remove(book);
            Console.WriteLine($"Removed book: {book.Title} by {book.Author}");
        }
        else
        {
            Console.WriteLine($"Book not found: {book.Title}");
        }
    }

    public void ShowLibraryInfo()
    {
        Console.WriteLine($"Library Capacity: {Capacity} books");
        Console.WriteLine($"Library Size: {SquareMeters} square meters");
    }
}