using BookStoreApi.Models;
using BookStoreApi.Packages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IPKG_Bookstore books_package;
        public BookController(IPKG_Bookstore books_package) 
        {
            this.books_package = books_package;
        }

        [HttpPost]
        public IActionResult SaveBoook(BookModel book)
        {
            try
            {
                if (book != null)
                {
                    books_package.add_book(book);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "წიგნი არ შეიძლება იყოს ცარიელი");
                }
                return StatusCode(StatusCodes.Status200OK, "წიგნი წარმატებით დაემატა");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "შეცდომა მოხდა მოგვიანებით ცადეთ");
            }
        }

        [HttpPut] 
        public IActionResult UpdateBook(BookModel book)
        {
            try
            {
                if (book != null) books_package.update_book(book);
                else return StatusCode(StatusCodes.Status400BadRequest, "წიგნი არ შეიძლება იყოს ცარიელი");
                return StatusCode(StatusCodes.Status200OK, "წიგნი წარმატებით განახლდა");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "შეცდომა მოხდა მოგვიანებით ცადეთ");
            }
        }

        [HttpGet]
        public IActionResult GetAllBook()
        {
            try
            {
                var books = books_package.get_all_books();
                return Ok(books);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "შეცდომა მოხდა მოგვიანებით ცადეთ");
            }
        }

        [HttpDelete]
        public IActionResult DeleteBook(int book_id) 
        {
            try
            {
                var book = books_package.get_book_by_id(book_id);
                if(book != null)
                {
                    books_package.delete_book(book_id);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "გთხოვთ მიუთთოთ ვალიდური წიგნი");

                }
                return StatusCode(StatusCodes.Status200OK, "წიგნი წარმატებით წაიშალა");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "შეცდომა მოხდა მოგვიანებით ცადეთ");
            }
        }


    }
}
