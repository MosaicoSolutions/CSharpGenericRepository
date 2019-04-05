// using System.Linq;
// using Bogus;
// using FluentAssertions;
// using MosaicoSolutions.GenericRepository.Repositories.Write.Transactions;
// using MosaicoSolutions.GenericRepository.Test.Data.Contexts;
// using MosaicoSolutions.GenericRepository.Test.Data.Entities;
// using Xunit;

// namespace MosaicoSolutions.GenericRepository.Test.WriteRepository
// {
//     public class TransactionTaskTest
//     {
//         private TransactionTaskManager<BookStoreContext> transactionTaskManager;

//         public TransactionTaskTest()
//         {
//             transactionTaskManager = new TransactionTaskManager<BookStoreContext>(() => BookStoreContext.SqlServer());
//         }

//         [Fact]
//         public void UseTransactionTask()
//         {
//             var fakeAuthors = new Faker<Author>()
//                 .RuleFor(a => a.FirstName, f => f.Name.FirstName())
//                 .RuleFor(a => a.LastName, f => f.Name.LastName());

//             var newAuthor = fakeAuthors.Generate(1).FirstOrDefault();
//             transactionTaskManager.Enqueue(authorWriteRepository.InsertAsTransactionTask(newAuthor));

//             transactionTaskManager.Enqueue(dbContext => 
//             {
//                 var author = fakeAuthors.Generate(1).FirstOrDefault();

//                 bookStoreContext.Add(author);
//                 dbContext.SaveChanges();

//                 var book = new Book
//                 {
//                     AuthorId = author.AuthorId,
//                     Title = "Using Transaction Tasks"
//                 };

//                 dbContext.Book.Add(book);
//                 dbContext.SaveChanges();
//             });

//             transactionTaskManager.PerformTransaction();
//             TransactionTask<BookStoreContext> task = dbContext => 
//             {

//             };
//             task = task.EndWithSaveChanges();

//             bookStoreContext.Author.Any(a => a.AuthorId == newAuthor.AuthorId).Should().BeTrue();
//             bookStoreContext.Book.Any(b => b.Title == "Using Transaction Tasks").Should().BeTrue();
//         }
//     }
// }