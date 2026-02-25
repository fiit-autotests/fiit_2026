using Kontur.BigLibrary.Tests.Core.Helpers.StringGenerator;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Pages;
using Kontur.BigLibrary.Tests.UI.PW.PlaywrightCore;

namespace Kontur.BigLibrary.Tests.UI.PW.PlaywrightTests;

[WithAuth]
public class BookListPageTests : TestBase
{
    [Test]
    public async Task AddBook_SuccessTest()
    {
        var bookName = StringGenerator.GetRandomString(10);
        var bookAuthor = StringGenerator.GetRandomString(10);
        var book = TestData.CreateBook(bookName, bookAuthor);

        var booksPage = await Navigation.GoToPageAsync<MainPage>();

        var bookModal = await booksPage.AddBookButton.ClickAndOpenModalAsync<AddBookModal>();
        await bookModal.WaitVisibleAsync();
        
        await bookModal.NameInput.FillAsync(bookName);
        await bookModal.DescriptionInput.FillAsync(StringGenerator.GetRandomString(10));
        await bookModal.AuthorInput.FillAsync(bookAuthor);
        await bookModal.RubricDropdown.SelectByText("Администрирование");
        await bookModal.UploadImage.SetInputFilesAsync(TestData.ValidImagePath);
        await bookModal.AddBookSubmit.ClickAsync();
        
        await bookModal.WaitInvisibleAsync();
        await booksPage.RefreshAsync();
    
        var bookViewPage = await booksPage.BookList.GetBookItem(bookName).BookLink.ClickAndOpenPageAsync<BookPage>();
        
        await bookViewPage.BookName.CheckTextAsync(bookName);
        await bookViewPage.BookAuthor.CheckTextAsync(bookAuthor);
        await bookViewPage.BookDescription.CheckTextAsync(book.Description);
        await bookViewPage.FreeState.WaitVisibleAsync();
    }
}