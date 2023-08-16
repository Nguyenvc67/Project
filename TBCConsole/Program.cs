using BL;
using Model;
using OrderManagementApp.BLL;
using OrderManagementApp.DAL;
using Spectre.Console;

namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {


            short mainChoose = 0, imChoose;
            string[] mainMenu = { "Search Tabacco", "Create Order", "Exit" };
            string[] imMenu = { "Get By Tabacco Id", "Get All Tabaccos", "Search By Tabacco Name", "Exit" };
            TabaccoBL ibl = new TabaccoBL();
            StaffBL sta = new StaffBL();
            string[] Login = { "login" };
            List<Tabacco> lst = new List<Tabacco>();
            do
            {
                StaffBL staffBL = new StaffBL();
                Staff? orderStaff;
                bool active = true;
                while (active = true)
                {
                    string UserName;
                    Console.WriteLine(@"
████████╗ ██████╗ ██████╗  █████╗  ██████╗ ██████╗  ██████╗ 
╚══██╔══╝██╔═══██╗██╔══██╗██╔══██╗██╔════╝██╔═══██╗██╔═══██╗
   ██║   ██║   ██║██████╔╝███████║██║     ██║   ██║██║   ██║
   ██║   ██║   ██║██╔══██╗██╔══██║██║     ██║   ██║██║   ██║
   ██║   ╚██████╔╝██████╔╝██║  ██║╚██████╗╚██████╔╝╚██████╔╝
   ╚═╝    ╚═════╝ ╚═════╝ ╚═╝  ╚═╝ ╚═════╝ ╚═════╝  ╚═════╝ 
        ███████╗████████╗ ██████╗ ██████╗ ███████╗                  
        ██╔════╝╚══██╔══╝██╔═══██╗██╔══██╗██╔════╝                  
        ███████╗   ██║   ██║   ██║██████╔╝█████╗                    
        ╚════██║   ██║   ██║   ██║██╔══██╗██╔══╝                    
        ███████║   ██║   ╚██████╔╝██║  ██║███████╗                  
        ╚══════╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝
                    ");
                    Console.Write("User Name : ");
                    UserName = Console.ReadLine() ?? "";
                    if (UserName == "0")
                    {
                        active = false;
                        break;
                    }
                    else
                    {
                        orderStaff = staffBL.Login(UserName);
                    }
                    Console.Clear();
                    if (orderStaff != null)
                    {

                        while (true)
                        {
                            mainChoose = Menu("                         Order Management", mainMenu);
                            switch (mainChoose)
                            {
                                case 1:
                                    do
                                    {

                                        imChoose = Menu("                       Tabacco Management", imMenu);
                                        switch (imChoose)
                                        {
                                            case 1:
                                                Console.Write("\nInput Tabacco Id: ");
                                                int tabaccoId;
                                                if (Int32.TryParse(Console.ReadLine(), out tabaccoId))
                                                {
                                                    Tabacco i = ibl.GetTabaccoById(tabaccoId);
                                                    if (i != null)
                                                    {

                                                        Console.WriteLine("Tabacco Name: " + i.TabaccoName);
                                                        Console.Write("Tabacco Price: " + i.TabaccoPrice);
                                                        Console.WriteLine(" VND");
                                                        Console.WriteLine("Manufactory: " + i.Manufactory);
                                                        Console.WriteLine("Amount: " + i.Amount);
                                                        Console.WriteLine("Tabacco Pack: " + i.TabaccoPack);
                                                        Console.WriteLine("Tabacco Date: " + i.TabaccoDate);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("There is no Tabacco with id " + tabaccoId);
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Your Choose is wrong!");
                                                }
                                                Console.WriteLine("\n    Press Enter key to back menu...");
                                                Console.ReadLine();
                                                break;
                                            case 2:

                                                lst = ibl.GetAll();
                                                Console.WriteLine("\nTabacco Count: " + lst.Count());
                                                Console.ReadKey();
                                                break;
                                            case 3:
                                                lst = ibl.GetByName("I");
                                                Console.WriteLine("\nTabacco Count By Name: " + lst.Count());
                                                break;

                                        }
                                    } while (imChoose != imMenu.Length);
                                    break;
                                case 2:

                                    string connectionString = "server=localhost;user id=root;password=nguyen6797;port=3306;database=OrderDB;IgnoreCommandTransaction=true;"; // Thay bằng chuỗi kết nối thực tế của bạn
                                    DataAccess dataAccess = new DataAccess(connectionString);
                                    OrderManager orderManager = new OrderManager(dataAccess);
                                    Console.WriteLine("---- Create New Order ----");

                                    List<Customer> customers = orderManager.GetAllCustomers();
                                    Console.WriteLine("Customers:");
                                    foreach (var customer in customers)
                                    {
                                        Console.WriteLine($"{customer.CustomerId}. {customer.CustomerName}");
                                    }
                                    Console.Write("Select Customer (ID): ");
                                    int selectedCustomerId = int.Parse(Console.ReadLine());

                                    List<Tabacco> tabaccos = orderManager.GetAllTabaccos();
                                    Console.WriteLine("Tabaccos:");
                                    foreach (var tabacco in tabaccos)
                                    {
                                        Console.WriteLine($"{tabacco.TabaccoId}. {tabacco.TabaccoName}");
                                    }
                                    Console.Write("Select Tabacco (ID): ");
                                    int selectedTabaccoId = int.Parse(Console.ReadLine());

                                    Console.Write("Quantity: ");
                                    int quantity = int.Parse(Console.ReadLine());

                                    Order newOrder = new Order
                                    {
                                        CustomerId = selectedCustomerId,
                                        SellerId = 1,
                                        OrderDate = DateTime.Now,
                                        OrderStatus = "Pending"
                                    };

                                    OrderDetail newOrderDetail = new OrderDetail
                                    {
                                        TabaccoId = selectedTabaccoId,
                                        Quantity = quantity
                                    };

                                    newOrder.OrderDetails = new List<OrderDetail> { newOrderDetail };

                                    orderManager.AddOrder(newOrder);

                                    Console.WriteLine("Order created successfully!");
                                    Console.ReadKey();
                                    Console.Clear();
                                    var table = new Table();
                                    table.AddColumn("Order Id ");
                                    table.AddColumn("Customer ID ");
                                    table.AddColumn("Seller Id ");
                                    table.AddColumn("Date ");
                                    table.AddColumn("Status ");
                                    table.AddColumn("Tabacco ");
                                    table.AddColumn("Quantity ");
                                    foreach (var orderDetail in newOrder.OrderDetails)
                                    {
                                        table.AddRow(newOrder.OrderId.ToString(), newOrder.CustomerId.ToString(), newOrder.SellerId.ToString(), newOrder.OrderDate.ToString(), newOrder.OrderStatus, orderDetail.TabaccoId.ToString(), orderDetail.Quantity.ToString());
                                    }

                                    AnsiConsole.Render(table);

                                    // var detailsTable = new Table();
                                    // detailsTable.AddColumn("Tabacco: ");
                                    // detailsTable.AddColumn("Quantity: ");

                                    // foreach (var orderDetail in newOrder.OrderDetails)
                                    // {
                                    //     detailsTable.AddRow(orderDetail.TabaccoId.ToString(), orderDetail.Quantity.ToString());
                                    // }

                                    // AnsiConsole.Render(detailsTable);


                                    // Console.WriteLine($"Order Id: {newOrder.OrderId}");
                                    // Console.WriteLine($"Customer: {newOrder.CustomerId}");
                                    // Console.WriteLine($"Seller: {newOrder.SellerId}");
                                    // Console.WriteLine($"Date: {newOrder.OrderDate}");
                                    // Console.WriteLine($"Status: {newOrder.OrderStatus}");

                                    // foreach (var orderDetail in newOrder.OrderDetails)
                                    // {
                                    //     Console.WriteLine($"Tabaccos: {orderDetail.TabaccoId}, Quantity: {orderDetail.Quantity}");
                                    // }
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    Console.ReadKey();
                                    break;

                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid Username or Password !");
                    }

                }
            } while (mainChoose != mainMenu.Length);
        }

        private static short Menu(string title, string[] menuTabaccos)
        {
            string logo = @"==========================================================================================


████████╗ ██████╗ ██████╗  █████╗  ██████╗ ██████╗  ██████╗ 
╚══██╔══╝██╔═══██╗██╔══██╗██╔══██╗██╔════╝██╔═══██╗██╔═══██╗
   ██║   ██║   ██║██████╔╝███████║██║     ██║   ██║██║   ██║
   ██║   ██║   ██║██╔══██╗██╔══██║██║     ██║   ██║██║   ██║
   ██║   ╚██████╔╝██████╔╝██║  ██║╚██████╗╚██████╔╝╚██████╔╝
   ╚═╝    ╚═════╝ ╚═════╝ ╚═╝  ╚═╝ ╚═════╝ ╚═════╝  ╚═════╝ 
        ███████╗████████╗ ██████╗ ██████╗ ███████╗                  
        ██╔════╝╚══██╔══╝██╔═══██╗██╔══██╗██╔════╝                  
        ███████╗   ██║   ██║   ██║██████╔╝█████╗                    
        ╚════██║   ██║   ██║   ██║██╔══██╗██╔══╝                    
        ███████║   ██║   ╚██████╔╝██║  ██║███████╗                  
        ╚══════╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝  
                                        
                                       ";
            short choose = 0;
            Console.WriteLine($"\n" + logo);
            string line = "==========================================================================================";
            Console.WriteLine(line);
            Console.WriteLine(" " + title);
            Console.WriteLine(line);
            for (int i = 0; i < menuTabaccos.Length; i++)
            {
                Console.WriteLine(" " + (i + 1) + ". " + menuTabaccos[i]);
            }
            Console.WriteLine(line);
            do
            {
                Console.Write("Your choice: ");
                try
                {
                    choose = Int16.Parse(Console.ReadLine() ?? "");
                }
                catch
                {
                    Console.WriteLine("Your Choose is wrong!");
                    continue;
                }
            } while (choose <= 0 || choose > menuTabaccos.Length);
            return choose;
        }

    }
}