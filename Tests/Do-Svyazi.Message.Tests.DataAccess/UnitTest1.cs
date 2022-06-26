using Do_Svyazi.Message.DataAccess;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Do_Svyazi.Message.Tests.DataAccess;

public class Tests
{
    private MessageDatabaseContext _context = null!;

    [SetUp]
    public void Setup()
    {
        var optionBuilder = new DbContextOptionsBuilder<MessageDatabaseContext>()
            .UseSqlite("Filename=test.db")
            .UseLazyLoadingProxies();
        
        _context = new MessageDatabaseContext(optionBuilder.Options);
    }

    [Test]
    public void DatabaseCreationTest_DatabaseCreated_NoExceptionThrown()
    {
        Assert.DoesNotThrow(() => _context.Database.EnsureCreated());
    }
}