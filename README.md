# CSharpGenericRepository
A repository pattern implementation using ccqrs concepts.

Often applications need to access a database to store and query information. 
The **Repository Pattern** serves as an abstraction to persist data, reducing code duplication and increasing cohesion and low coupling between parts of the system.

**Command Query Responsibility Segregation** (CQRS) is an architectural standard that seeks to separate the responsibility for writing and reading your data.

This project provides a generic Repository interface using the CQRS concepts.

## 1. Read Repository

Use the abstract class `ReadRepository<TDbContext, TEntity>` where `TDbContext` is a `DbContext`, and `TEntity` is a class, to perform queries and retrieve the data.

You can inherit it in your own class, or use extension methods.

``` c#
var dbContextOptions = new DbContextOptionsBuilder<BookStoreContext>()
                            .UseInMemoryDatabase(databaseName: "BookStoreContext")
                            .Options;

var bookStoreContext = new BookStoreContext(dbContextOptions);

ReadRepository<BookStoreContext, Book> bookReadRepository;

bookReadRepository = new DefaultReadRepository<BookStoreContext, Book>(bookStoreContext);

var book = bookReadRepository.FindById(1);
```

### 1.1 Using IQuery for custom queries.

For complex and custom queries, you can use the QueryOptions class to specify your query.

``` c#
var queryOptions = new QueryOptions<Author>
{
    Where = a => a.Books.Any(),
    Sort = new SortOptions<Author>
    {
        OrderBy = a => a.FirstName
    },
    Includes = new Expression<Func<Author, object>>[]
    {
        a => a.Books
    }
};

IQuery query = authorReadRepository.Query(queryOptions);

var firstAuthor = await query.FirstAsync();
var lastAuthor = await query.LastAsync();
var filteredAuthors await query.ToListAsync();

```

> You can use the `QueryBuilder` class to construct the query specification.

``` c#
var queryOptions = QueryBuilder.For<Author>()
                               .Where(a => a.Books.Count() == 3)
                               .Include(a => a.Books)
                               .OrderBy(a => a.FirstName)
                               .ThenBy(a => a.LastName)
                               .Descending()
                               .Build();

var authors = authorReadRepository.Find(queryOptions);
```

The `MosaicSolutions.GenericRepository.Repositories.Read` namespace only contains interfaces for query operations, that is, they do not change the state of the database.

## 1. Write Repository

Use the  `WriteRepository<TDbContext, TEntity>` class where `TDbContext` is a `DbContext` and `TEntity` is a class, to perform operations that change the state of the data.

## <> with :heart: and [VSCode](https://code.visualstudio.com)
