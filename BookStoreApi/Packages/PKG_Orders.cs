using BookStoreApi.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace BookStoreApi.Packages
{
    public interface IPKG_Orders
    {
        public void add_oreder(OrderModel order);
        public List<OrderModel> get_orders();
        public List<OrderModel> get_users_orders();
        public List<OrderModel> get_user_orders(string customer);
        public List<OrderModel> get_user_each_orders(OrderModel customer);
        public void update_order_status(OrderModel order);

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


        public List<OrderModel> get_users_orders()
        {
            List<OrderModel> orders = new List<OrderModel>();
            OracleConnection oracleConnection = new OracleConnection();
            oracleConnection.ConnectionString = ConnectionStr;
            oracleConnection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oracleConnection;
            cmd.CommandText = "olerning.pkg_lo_bookstore_orders.get_users_orders";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_resault", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                OrderModel order = new OrderModel();
                order.Customer = reader["CUSTOMER"].ToString();
                order.Quantity = int.Parse(reader["ORDERS_COUNT"].ToString());
                orders.Add(order);
            }

            oracleConnection.Close();

            return orders;
        }

        public List<OrderModel> get_user_orders(string customer)
        {
            List<OrderModel> orders = new List<OrderModel>();
            OracleConnection oracleConnection = new OracleConnection();
            oracleConnection.ConnectionString = ConnectionStr;
            oracleConnection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oracleConnection;
            cmd.CommandText = "olerning.pkg_lo_bookstore_orders.get_user_orders";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_resault", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("p_customer", OracleDbType.Varchar2).Value = customer;
            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                OrderModel order = new OrderModel();

                order.Customer = reader["customer"].ToString();
                order.Quantity = int.Parse(reader["orders_count"].ToString());
                order.Order_date = DateTime.Parse(reader["order_date"].ToString());
                order.Order_status = int.Parse(reader["status"].ToString());


                orders.Add(order);
            }

            oracleConnection.Close();

            return orders;
        }

        public List<OrderModel> get_user_each_orders(OrderModel customer)
        {
            List<OrderModel> orders = new List<OrderModel>();
            OracleConnection oracleConnection = new OracleConnection();
            oracleConnection.ConnectionString = ConnectionStr;
            oracleConnection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oracleConnection;
            cmd.CommandText = "olerning.pkg_lo_bookstore_orders.get_user_orders_each_order";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_resault", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("p_date", OracleDbType.Date).Value = customer.Order_date;
            cmd.Parameters.Add("p_customer", OracleDbType.Varchar2).Value = customer.Customer;

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                OrderModel order = new OrderModel();

                order.Customer = reader["customer"].ToString();
                order.Quantity = int.Parse(reader["quantity"].ToString());
                order.Book_author = reader["book_author"].ToString();
                order.Book_name = reader["book_name"].ToString();
                order.Order_total = int.Parse(reader["order_price"].ToString());
                order.Order_status = int.Parse(reader["status"].ToString());

                orders.Add(order);
            }

            oracleConnection.Close();

            return orders;
        }


        public void update_order_status(OrderModel order)
        {
            OracleConnection oracleConnection = new OracleConnection();
            oracleConnection.ConnectionString = ConnectionStr;
            oracleConnection.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oracleConnection;
            cmd.CommandText = "olerning.pkg_lo_bookstore_orders.update_order_status";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_status_number", OracleDbType.Int32).Value = order.Order_status;
            cmd.Parameters.Add("p_date", OracleDbType.Date).Value = order.Order_date;
            cmd.ExecuteNonQuery();
            oracleConnection.Close();
        }
    }
}
