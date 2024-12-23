using BookStoreApi.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace BookStoreApi.Packages
{
    public interface IPKG_Orders
    {
        public void add_oreder(OrderModel order);
        public List<OrderModel> get_orders();

    }
    public class PKG_Orders : PKG_Base, IPKG_Orders
    {
        IConfiguration configuration;

        public PKG_Orders(IConfiguration configuration) : base(configuration) { }

        public void add_oreder(OrderModel order)
        {
            OracleConnection oracleConnection = new OracleConnection();
            oracleConnection.ConnectionString = ConnectionStr;
            oracleConnection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oracleConnection;
            cmd.CommandText = "olerning.pkg_lo_bookstore_orders.add_order";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_book_id", OracleDbType.Int32).Value = order.BookId;
            cmd.Parameters.Add("p_quantity", OracleDbType.Int32).Value = order.Quantity;
            cmd.Parameters.Add("p_customer", OracleDbType.Varchar2).Value = order.Customer;

            cmd.ExecuteNonQuery();

            oracleConnection.Close();
        }

        public List<OrderModel> get_orders()
        {
            List<OrderModel> orders = new List<OrderModel>();
            OracleConnection oracleConnection = new OracleConnection();
            oracleConnection.ConnectionString = ConnectionStr;
            oracleConnection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oracleConnection;
            cmd.CommandText = "olerning.pkg_lo_bookstore_orders.get_all_orders";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_resault", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            { 
                OrderModel order = new OrderModel();
                order.Id = int.Parse(reader["ID"].ToString());
                order.Quantity = int.Parse(reader["QUANTITY"].ToString());
                order.Book_name = reader["BOOK_NAME"].ToString();
                order.Order_total = int.Parse(reader["ORDER_PRICE"].ToString());
                order.Customer = reader["CUSTOMER"].ToString();
                orders.Add(order);
            }

            oracleConnection.Close();

            return orders;
        }
    }
}
