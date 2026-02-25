using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Kontur.BigLibrary.Service.Contracts;
using Kontur.BigLibrary.Service.Events;
using Kontur.BigLibrary.Service.Exceptions;
using Kontur.BigLibrary.Service.Services.BookService;
using Kontur.BigLibrary.Service.Services.BookService.Repository;
using Kontur.BigLibrary.Service.Services.EventService.Repository;
using Kontur.BigLibrary.Service.Services.ImageService.Repository;
using Kontur.BigLibrary.Tests.Core.Helpers;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace Kontur.BigLibrary.Tests.Integration.BookServiceTests;

[NonParallelizable]
public class BookServiceMockTest2
{
    private static readonly IServiceProvider container = new ContainerForMockTests().Build();

    private readonly IBookService bookService = container.GetRequiredService<IBookService>();
    private readonly IBookRepository bookRepository = container.GetRequiredService<IBookRepository>();
    private readonly IImageRepository imageRepository = container.GetRequiredService<IImageRepository>();
    private readonly IEventRepository eventRepository = container.GetRequiredService<IEventRepository>();

    [SetUp]
    public void SetUp()
    {
        imageRepository.GetAsync(Arg.Any<int>(), CancellationToken.None)
            .Throws(new NotImplementedException());
        bookRepository.GetRubricAsync(Arg.Any<int>(), CancellationToken.None)
            .Throws(new NotImplementedException());
        bookRepository.SaveBookAsync(Arg.Any<Book>(), Arg.Any<CancellationToken>())
            .Throws(new NotImplementedException());
    }

    [Test]
    public async Task SaveBookAsync_ReturnSameBook_WhenSaveCorrectBook()
    {
        imageRepository.GetAsync(Arg.Any<int>(), CancellationToken.None)
            .Returns(_ => Task.FromResult(new Image() { Id = IntGenerator.Get() }));
        bookRepository.GetRubricAsync(Arg.Any<int>(), CancellationToken.None)
            .Returns(_ => Task.FromResult(new Rubric() { Id = 1 }));
        bookRepository.SaveBookAsync(Arg.Any<Book>(), Arg.Any<CancellationToken>())
            .Returns(x => Task.FromResult(x[0] as Book));
        var book = container.GetRequiredService<BookBuilder>().WithId(1).Build();
        var result = await bookService.SaveBookAsync(book, CancellationToken.None);

        using (new AssertionScope())
        {
            result.Should().BeEquivalentTo(book);
            await bookRepository.Received(1)
                .SaveBookIndexAsync(book.Id!.Value, Arg.Any<string>(), Arg.Any<string>(),
                    Arg.Any<CancellationToken>());
            await bookRepository.Received(1)
                .SaveBookAsync(book, Arg.Any<CancellationToken>());

            var @event = result.CreateChangedEvent();
            await eventRepository.Received(1).SaveAsync(Arg.Is<ChangedEvent>(x => x.Source == @event.Source),
                Arg.Any<CancellationToken>());
            // или
            await eventRepository.Received(1).SaveAsync(Arg.Any<ChangedEvent>(),
                Arg.Any<CancellationToken>());
        }
    }

    [Test]
    public void SaveBookAsync_ReturnError_WhenUnknownRubric()
    {
        bookRepository.GetRubricAsync(Arg.Any<int>(), CancellationToken.None)
            .Returns(Task.FromResult<Rubric>(null));
        var book = container.GetRequiredService<BookBuilder>().Build();

        Assert.CatchAsync<ValidationException>(
            async () => await bookService.SaveBookAsync(book, CancellationToken.None));
    }

    [Test]
    public void SaveBookAsync_ReturnError_WhenUnknownImage()
    {
        bookRepository.GetRubricAsync(Arg.Any<int>(), CancellationToken.None)
            .Returns(Task.FromResult<Rubric>(null));
        imageRepository.GetAsync(Arg.Any<int>(), CancellationToken.None)
            .Returns(Task.FromResult<Image>(null));
        var book = container.GetRequiredService<BookBuilder>().Build();

        Assert.CatchAsync<ValidationException>(
            async () => await bookService.SaveBookAsync(book, CancellationToken.None));
    }
}