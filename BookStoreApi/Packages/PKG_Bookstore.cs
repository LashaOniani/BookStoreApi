using BookStoreApi.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace BookStoreApi.Packages
{
    public interface IPKG_Bookstore
    {
        public List<BookModel> get_all_books();
        public BookModel get_book_by_id(int id);
        public void add_book(BookModel book);
        public void update_book(BookModel book);
        public void delete_book(int id);


    }
    public class PKG_Bookstore : PKG_Base, IPKG_Bookstore
    {
        IConfiguration configuration;

        public PKG_Bookstore (IConfiguration configuration) : base(configuration) { }

        public void add_book(BookModel book)
        {
            OracleConnection oracleConnection = new OracleConnection();
            oracleConnection.ConnectionString = ConnectionStr;
            oracleConnection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oracleConnection;
            cmd.CommandText = "olerning.pkg_LO_BOOKSTORE_BOOKS.add_book";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_book_name", OracleDbType.Varchar2).Value = book.Book_name;
            cmd.Parameters.Add("p_author", OracleDbType.Varchar2).Value = book.author;
            cmd.Parameters.Add("p_price", OracleDbType.Double).Value = book.Price;
            cmd.Parameters.Add("p_quantity", OracleDbType.Int32).Value = book.Quantity;

            cmd.ExecuteNonQuery();

            oracleConnection.Close();
        }

        public BookModel get_book_by_id(int id)
        {
            
            OracleConnection oracleConnection = new OracleConnection();
            oracleConnection.ConnectionString = ConnectionStr;
            oracleConnection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oracleConnection;
            cmd.CommandText = "olerning.pkg_LO_BOOKSTORE_BOOKS.select_all_book";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_resault", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                BookModel book = new BookModel();
                book.Id = int.Parse(reader["id"].ToString());
                book.Book_name = reader["BOOK_NAME"].ToString();
                book.author = reader["AUTHOR"].ToString();
                book.Price = double.Parse(reader["PRICE"].ToString());
                book.Quantity = int.Parse(reader["QUANTITY"].ToString());

                oracleConnection.Close();
                return book;
            }
            

            return null; 
        }
        public void update_book(BookModel book) 
        {
            OracleConnection oracleConnection = new OracleConnection();
            oracleConnection.ConnectionString = ConnectionStr;
            oracleConnection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oracleConnection;
            cmd.CommandText = "olerning.pkg_LO_BOOKSTORE_BOOKS.update_book";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("ID", OracleDbType.Int32).Value = book.Id;    
            cmd.Parameters.Add("p_book_name", OracleDbType.Varchar2).Value = book.Book_name;
            cmd.Parameters.Add("p_author", OracleDbType.Varchar2).Value = book.author;
            cmd.Parameters.Add("p_price", OracleDbType.Double).Value = book.Price;

            cmd.ExecuteNonQuery();



            oracleConnection.Close();
        }
        public List<BookModel> get_all_books()
        {
            List<BookModel> bookList = new List<BookModel>();
            OracleConnection oracleConnection = new OracleConnection();
            oracleConnection.ConnectionString = ConnectionStr;
            oracleConnection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oracleConnection;
            cmd.CommandText = "olerning.pkg_LO_BOOKSTORE_BOOKS.select_all_book";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_resault", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                BookModel book = new BookModel();
                book.Id = int.Parse(reader["id"].ToString());
                book.Book_name = reader["BOOK_NAME"].ToString();
                book.author = reader["AUTHOR"].ToString();
                book.Price = double.Parse(reader["PRICE"].ToString());
                book.Quantity = int.Parse(reader["QUANTITY"].ToString());

                bookList.Add(book);
            }

            oracleConnection.Close();
            return bookList;
        }

        public void delete_book(int id)
        {
            OracleConnection oracleConnection = new OracleConnection();
            oracleConnection.ConnectionString = ConnectionStr;
            oracleConnection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oracleConnection;
            cmd.CommandText = "olerning.pkg_LO_BOOKSTORE_BOOKS.delete_book";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = id;

            cmd.ExecuteNonQuery();

            oracleConnection.Close();
        }



    }
}
