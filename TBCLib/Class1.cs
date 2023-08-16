// using System;

// class Invoice1
// {
//     public string InvoiceNumber { get; set; }
//     public string CustomerName { get; set; }
//     public DateTime InvoiceDate { get; set; }
//     public decimal TotalAmount { get; set; }

//     public Invoice1(string number, string customer, DateTime date, decimal amount)
//     {
//         InvoiceNumber = number;
//         CustomerName = customer;
//         InvoiceDate = date;
//         TotalAmount = amount;
//     }

//     public void PrintInvoice()
//     {
//         Console.WriteLine("Invoice Number: " + InvoiceNumber);
//         Console.WriteLine("Customer: " + CustomerName);
//         Console.WriteLine("Invoice Date: " + InvoiceDate.ToString("yyyy-MM-dd"));
//         Console.WriteLine("Total Amount: $" + TotalAmount);
//     }
// }

// class Program
// {
//     static void Main(string[] args)
//     {
//         // Tạo một đối tượng hóa đơn mới
//         Invoice1 invoice = new Invoice1("INV123", "John Doe", DateTime.Now, 500.00m);

//         // In thông tin hóa đơn
//         invoice.PrintInvoice();
//     }
// }
// class Program
// {
//     static void Main()
//     {
//         string connectionString = "server=your_server;user=your_username;password=your_password;database=your_database;";

//         using (MySqlConnection connection = new MySqlConnection(connectionString))
//         {
//             connection.Open();

//             // Tạo câu truy vấn SQL để lấy dữ liệu cần thiết cho hóa đơn
//             string query = "SELECT * FROM Customers WHERE customer_id = @CustomerId;";
            
//             using (MySqlCommand command = new MySqlCommand(query, connection))
//             {
//                 command.Parameters.AddWithValue("@CustomerId", 1); // Thay đổi giá trị theo nhu cầu
                
//                 using (MySqlDataReader reader = command.ExecuteReader())
//                 {
//                     if (reader.Read())
//                     {
//                         // Đọc dữ liệu từ các cột trong bản ghi
//                         int customerId = reader.GetInt32("customer_id");
//                         string customerName = reader.GetString("customer_name");
//                         string customerAddress = reader.GetString("customer_address");

//                         // Tiến hành tạo hóa đơn dựa trên dữ liệu đã lấy
//                         // Có thể sử dụng các thông tin này để điền vào thông tin khách hàng trong hóa đơn
//                     }
//                     else
//                     {
//                         Console.WriteLine("Customer not found.");
//                     }
//                 }
//             }

//             connection.Close();
//         }
//     }
// }
