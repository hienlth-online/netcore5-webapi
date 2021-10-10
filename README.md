# netcore5-webapi
Demo .NET Core 5 (NET 5) Web API


# Demo 02 - Relationship
## Some steps to action
1. Create new Model, named ```Publisher``` that is related to ```Book```. Set relationship between ```Book``` and ```Publisher```: One publisher has many books
    - In ```Publisher``` model add relationship attribute:
        ```cs
        public List<Book> Books { get; set; }
        ```
    - In ```Book``` model add relationship attribute:
        ```cs
        public int? PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
        ```
2. Create model ```Author``` and set relationship between ```Author``` and ```Book```. An author can write many books and a book is written by many authors.
    - We will create a new model ```Book Author``` to break out a many-many relationship into 2 one-many relationships.
    - We can define relationship betwen ```Book``` and ```Author``` in model ```MyDBContext```:
        ```cs
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Author>()
                .HasOne(b => b.Book)
                .WithMany(ba => ba.Book_Authors)
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<Book_Author>()
              .HasOne(b => b.Author)
              .WithMany(ba => ba.Book_Authors)
              .HasForeignKey(bi => bi.AuthorId);
        }
        ```
3. Create some services and some APIs for
    - Publisher
    - Author
    - Book