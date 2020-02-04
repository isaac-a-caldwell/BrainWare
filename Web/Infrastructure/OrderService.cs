namespace Web.Infrastructure
{
    using System.Collections.Generic;
    using System.Data;
    using Models;

    public class OrderService
    {
        public List<Order> GetFullOrdersForCompany(int companyId)
        {
            var database = new Database();
            var orders = GetOrdersForCompany(database, companyId);
            var orderProducts = GetOrderProductsJoinedByProducts(database);

            foreach (var order in orders)
            {
                foreach (var orderProduct in orderProducts)
                {
                    if (orderProduct.OrderId != order.OrderId)
                        continue;

                    order.OrderProducts.Add(orderProduct);
                    order.OrderTotal += (orderProduct.Price * orderProduct.Quantity);
                }
            }

            return orders;
        }

        internal List<Order> GetOrdersForCompany(Database database, int companyId)
        {
            // Get the orders
            var sqlQuery =
                $"SELECT c.name, o.description, o.order_id FROM company c INNER JOIN [order] o on c.company_id=o.company_id where o.company_id={companyId}";

            var reader = database.ExecuteReader(sqlQuery);

            var values = new List<Order>();

            while (reader.Read())
            {
                var record = (IDataRecord)reader;

                values.Add(new Order()
                {
                    //column names are better than ordinal numbers for readability
                    CompanyName = (string)record["name"],
                    Description = (string)record["description"],
                    OrderId = (int)record["order_id"],
                    OrderProducts = new List<OrderProduct>()
                });

            }

            reader.Close();

            return values;
        }
        internal List<OrderProduct> GetOrderProductsJoinedByProducts(Database database)
        {
            //Get the order products
            var sqlQuery =
                "SELECT op.price, op.order_id, op.product_id, op.quantity, p.name, p.price as product_price FROM orderproduct op INNER JOIN product p on op.product_id=p.product_id";

            var reader = database.ExecuteReader(sqlQuery);

            var values = new List<OrderProduct>();

            while (reader.Read())
            {
                var record = (IDataRecord)reader;

                values.Add(new OrderProduct()
                {
                    //column names are better than ordinal numbers for readability
                    OrderId = (int)record["order_id"],
                    ProductId = (int)record["product_id"],
                    Price = (decimal)record["price"],
                    Quantity = (int)record["quantity"],
                    Product = new Product()
                    {
                        Name = (string)record["name"],
                        Price = (decimal)record["product_price"],
                    }
                });
            }
            reader.Close();

            return values;
        }
    }
}