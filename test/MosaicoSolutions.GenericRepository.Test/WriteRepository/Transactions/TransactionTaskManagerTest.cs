using System.Linq;
using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Repositories.Write.Transactions;
using MosaicoSolutions.GenericRepository.Repositories.Write.Transactions.Manager;
using MosaicoSolutions.GenericRepository.Repositories.Write.Transactions.Manager.Interfaces;
using MosaicoSolutions.GenericRepository.Test.Data.Contexts;
using MosaicoSolutions.GenericRepository.Test.Data.Entities;
using Xunit;

namespace MosaicoSolutions.GenericRepository.Test.WriteRepository.Transactions
{
    public class TransactionTaskManagerTest
    {
        private readonly ITransactionTaskManager transactionTaskManager;
        private readonly ITransactionalRepository<Author> authorTransactionalRepository;
        private readonly ITransactionalRepository<Book> bookTransactionalRepository;
        private Faker<Author> fakerAuthor;
        private Faker<Book> fakerBook;

        public TransactionTaskManagerTest()
        {
            transactionTaskManager = new TransactionTaskManager(BookStoreContext.SqlServerExpress);
            authorTransactionalRepository = new TransactionalRepository<Author>();
            bookTransactionalRepository = new TransactionalRepository<Book>();

            fakerAuthor = new Faker<Author>()
                            .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                            .RuleFor(a => a.LastName, f => f.Name.LastName());

            fakerBook = new Faker<Book>()
                        .RuleFor(b => b.Title, f => f.Lorem.Word());
        }

        [Fact]
        public void InsertUsingTransaction()
        {
            var newAuthor = fakerAuthor.Generate();

            var insertTask = authorTransactionalRepository.InsertAsTransactionTask(newAuthor);

            var transanctionTaskResult = transactionTaskManager.UseTransaction(insertTask);

            transanctionTaskResult.Success.Should().BeTrue();
        }

        [Fact]
        public void InsertAndUpdateBooksUsingTransaction()
        {
            var newAuthor = fakerAuthor.Generate();

            var transactionTask = new TransactionTask(dbContext => 
            {
                var authorDbSet = dbContext.Set<Author>();
                var bookDbSet = dbContext.Set<Book>();

                authorDbSet.Add(newAuthor);
                var authorsInserted = dbContext.SaveChanges();

                var firstAuthor = authorDbSet.FirstOrDefault();
                var booksFirstAuthor = bookDbSet.AsTracking().Where(b => b.AuthorId == firstAuthor.AuthorId).ToList();

                booksFirstAuthor.ForEach(b => b.AuthorId = newAuthor.AuthorId);
                var booksUpdated = dbContext.SaveChanges();
            });

            var transanctionTaskResult = transactionTaskManager.UseTransaction(transactionTask);

            transanctionTaskResult.Success.Should().BeTrue();
        }

        [Fact]
        public void RemoveAuthorWithBooksUsingTransaction()
        {
            var transactionTask = new TransactionTask(dbContext => 
            {
                var firstAuthor = dbContext.Set<Author>().FirstOrDefault(a => a.Books.Any());

                dbContext.Remove(firstAuthor);

                dbContext.SaveChanges();
            });

            var transanctionTaskResult = transactionTaskManager.UseTransaction(transactionTask);

            transanctionTaskResult.Failure.Should().BeTrue();
            transanctionTaskResult.Exception.Should().BeAssignableTo<DbUpdateException>();
        }
    }
}