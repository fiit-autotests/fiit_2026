using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using FluentAssertions;
using Kontur.BigLibrary.Service.Contracts;
using Kontur.BigLibrary.Service.Services.BookService;
using Kontur.BigLibrary.Service.Services.ImageService;
using Microsoft.Extensions.DependencyInjection;
using Kontur.BigLibrary.Tests.Core.Helpers;
using Kontur.BigLibrary.Tests.Integration.BookServiceTests;
using NUnit.Framework;

namespace Kontur.BigLibrary.Tests.Integration;

public class CompareDocsTests
{
    private static readonly IServiceProvider container = new ContainerForBdTests().Build();
    private static readonly IBookService bookService = container.GetRequiredService<IBookService>();
    private static readonly IImageService imageService = container.GetRequiredService<IImageService>();
    private int imageId;

    [SetUp]
    public void SetUp()
    {
        var image = imageService
            .SaveAsync(new Image {Id = 1, Data = Array.Empty<byte>()}, new CancellationToken())
            .GetAwaiter().GetResult();
        imageId = image.Id!.Value;
    }
    
    [Test, Description("№1 Содержимое файла больше 0")]
    public async Task Should_NotBeEmpty()
    {
        // Создадим книгу
        
        // Экспортируем книги в xml

        // Проверим, что выгруженные данные не пустые
    }

    [Test, Description("№2 Проверка конкретных значений в файле (русское название книги)")]
    public async Task Should_Have_ExpectedInfo_RussianName()
    {
        // Создадим книгу с русскоязычным названием (Books.RussianBook)
        
        // Экспортируем книги в xml
        
        // Проверим название книги
    }
    
    [Test, Description("№3 Проверка схемы xml в случае отсутствия книг в выгрузке")]
    public async Task Should_NotContainBook_When_NoData()
    {
        // Создадим книгу

        // Экспортируем книги в xml с фильтром, по которому заранее известно, что нет книг

        // Проверим схему xml
    }

    [Test, Description("№4 Проверка количества выгруженных книг")]
    public async Task Should_Have_ExpectedCountOfBooks()
    {
        // Создадим книги

        // Экспортируем книги в xml с фильтром limit = 4
        
        // Проверим количество экспортированных книг с помощью .HaveElement("Book", Exactly.Times(4));

        // Альтернативный способ: проверим количество экспортированных книг с помощью Regex
    }
    
    [Test, Description("№5 Проверка всех данных выгруженной книги (XDocument-эталон)")]
    public async Task Should_Be_ExpectedXML()
    {
        // Создадим книгу

        // Экспортируем книги в xml с фильтром = названием книги
            
        // Создадим эталонный XDocument
        
        // Проверим все данные в выгруженном xml, сравнивая с эталонным XDocument с помощью BeEquivalentTo
    }
    
    [Test, Description("№6 Проверка всех данных выгруженной книги (approval-подход в тестировании; файл-эталон)")]
    public async Task Should_Be_ExpectedXML_File()
    {
        // Создадим книги
        for (var i = 0; i < 100; i++)
        {
            await imageService
                .SaveAsync(new Image { Id = i, Data = Array.Empty<byte>() }, new CancellationToken())
                .ConfigureAwait(false);
            await bookService.SaveBookAsync(
                new BookBuilder().WithId(i).WithName($"ExpectedXML_File {i}").WithAuthor($"Default author {i}").WithImage(i)
                    .Build(), CancellationToken.None);
        }

        // Экспортируем книги в xml с фильтром = названием книги
        
        // Создадим эталонный XDocument из файла (approval-подход)
        
        // Проверим все данные в выгруженном xml, сравнивая с эталонным XDocument с помощью BeEquivalentTo
    }

    private async Task<Book> CreateBook()
    {
        var book = new BookBuilder().WithImage(imageId).Build(); //создание книги
        await bookService.SaveBookAsync(book, CancellationToken.None); //Сохранение книги в БД
        return book;
    }
    
    private async Task<Book> CreateBook(string name = null)
    {
        var book = new BookBuilder().WithName(name).WithImage(imageId).Build(); //создание книги
        await bookService.SaveBookAsync(book, CancellationToken.None); //Сохранение книги в БД
        return book;
    }
    
    private BookFilter CreateFilter(string query = "", string rubric = "", int? limit = 10, bool isBusy = false,
        BookOrder order = BookOrder.ByLastAdding, int offset = 0)
    {
        return new()
        {
            Query = query,
            RubricSynonym = rubric,
            IsBusy = isBusy,
            Limit = limit,
            Order = order,
            Offset = offset
        };
    }
}
