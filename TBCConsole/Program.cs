using BL;
using Model;
using OrderManagementApp.BLL;
using OrderManagementApp.DAL;
using Spectre.Console;
using DAL;

namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;user id=root;password=nguyen6797;port=3306;database=OrderDB;IgnoreCommandTransaction=true;";
                                                    DataAccess dataAccess = new DataAccess(connectionString);
                                                    
                                                    OrderManager orderManager = new OrderManager(dataAccess);


            short mainChoose = 0, imChoose;
            string[] mainMenu = { "Search Tobacco", "Create Order", "Canel Order" };
            string[] imMenu = { "Get By Tobacco Id", "Get All Tobaccos", "Search By Tobacco Name", "Exit" };
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
                            Console.Clear();
                            switch (mainChoose)
                            {
                                case 1:
                                    do
                                    {

                                        imChoose = Menu("                       Tobacco Management", imMenu);
                                        switch (imChoose)
                                        {
                                            case 1:
                                                Console.Write("\nInput Tobacco Id: ");
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
                                                        Console.WriteLine("There is no Tobacco with id " + tabaccoId);
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
                                                Console.WriteLine("\nTobacco Count: " + lst.Count());
                                                Console.ReadKey();
                                                break;
                                            case 3:
                                                lst = ibl.GetByName("I");
                                                Console.WriteLine("\nTobacco Count By Name: " + lst.Count());
                                                break;

                                        }
                                    } while (imChoose != imMenu.Length);
                                    break;
                                case 2:

                                                    
                                                    Console.WriteLine("---- Create New Order ----");

                                                    List<Customer> customers = orderManager.GetAllCustomers();
                                                    Console.WriteLine("Customers:");
                                                    foreach (var customer in customers)
                                                    {
                                                        Console.WriteLine($"{customer.CustomerId}. {customer.CustomerName}");
                                                    }
                                                    Console.Write("Select Customer (ID): ");
                                                    int selectedCustomerId = int.Parse(Console.ReadLine());

                                                    Customer selectedCustomer = customers.FirstOrDefault(c => c.CustomerId == selectedCustomerId);
                                                    if (selectedCustomer == null)
                                                    {
                                                        Console.WriteLine("Customer not found.");
                                                    }
                                                    else
                                                    {
                                                        
                                                        List<Tabacco> tabaccos = orderManager.GetAllTabaccos();
                                                        Console.WriteLine("Tobaccos:");
                                                        foreach (var tabacco in tabaccos)
                                                        {
                                                            Console.WriteLine($"{tabacco.TabaccoId}. {tabacco.TabaccoName}");
                                                        }

                                                        List<OrderDetail> orderDetails = new List<OrderDetail>();

                                                        bool addMoreProducts = true;

                                                        while (addMoreProducts)
                                                        {
                                                            Console.Write("Select Tobacco (ID): ");
                                                            int selectedTabaccoId = int.Parse(Console.ReadLine());

                                                            Tabacco selectedTabacco = tabaccos.FirstOrDefault(t => t.TabaccoId == selectedTabaccoId);
                                                            if (selectedTabacco == null)
                                                            {
                                                                Console.WriteLine("Tobacco not found.");
                                                            }
                                                            else
                                                            {
                                                                Console.Write("Quantity: ");
                                                                int quantity = int.Parse(Console.ReadLine());

                                                                orderDetails.Add(new OrderDetail
                                                                {
                                                                    TabaccoId = selectedTabaccoId,
                                                                    Quantity = quantity
                                                                });

                                                                Console.Write("Do you want to add more products? (y/n): ");
                                                                string userInput = Console.ReadLine().ToLower();
                                                                addMoreProducts = userInput == "y" || userInput == "yes";
                                                            }
                                                        }
                                                    decimal totalAmount = 0;

                                                    foreach (var orderDetail in orderDetails)
                                                    {
                                                        Tabacco selectedTabacco = tabaccos.FirstOrDefault(t => t.TabaccoId == orderDetail.TabaccoId);
                                                        if (selectedTabacco != null)
                                                        {
                                                            totalAmount += selectedTabacco.TabaccoPrice * orderDetail.Quantity;
                                                        }
                                                    }

                                                    Order newOrder = new Order
                                                    {
                                                        CustomerId = selectedCustomerId,
                                                        CustomerName = selectedCustomer.CustomerName,
                                                        SellerId = 1,
                                                        OrderDate = DateTime.Now,
                                                        OrderStatus = "Processing",
                                                        OrderDetails = orderDetails
                                                    };

                                                    orderManager.AddOrder(newOrder);

                                                    Console.WriteLine("Order created successfully!");
                                    
                                                        Console.ReadKey();
                                                        Console.Clear();
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
                                                        Console.WriteLine("        Address: VTC Online Building, 18 D. Tam Trinh, Mai Dong, Hai Ba Trung, Ha Noi");
                                                        Console.WriteLine("        Phone: 000001");
                                    Console.WriteLine("\n\n\nSeller : Vu Cong Nguyen");
                                
                                    Customer selectedCustomerr = customers.FirstOrDefault(c => c.CustomerId == selectedCustomerId);

                                    if (selectedCustomerr != null)
                                    {
                                        Console.WriteLine($"Customer: {selectedCustomerr.CustomerName}");
                                        Console.WriteLine($"Address: {selectedCustomerr.CustomerAddress}");
                                        Console.WriteLine($"Phone: {selectedCustomerr.CustomerPhone}");
                                    }


                                    Console.WriteLine($"Seller ID: {newOrder.SellerId}");
                                    Console.WriteLine($"Date: {newOrder.OrderDate}");

                                    foreach (var orderDetail in newOrder.OrderDetails)
                                    {
                                        Console.WriteLine($"Tobaccos: {orderDetail.TabaccoId}, Quantity: {orderDetail.Quantity}");
                                    }
                                    // var table = new Table();
                                    // // table.AddColumn("Order Id ");
                                    // table.AddColumn("Customer ID ");
                                    // table.AddColumn("Seller Id ");
                                    // table.AddColumn("Date ");
                                    
                                    // table.AddColumn("Tabacco ");
                                    // table.AddColumn("Quantity ");
                                    // // table.AddColumn("Total Amount ");
                                    // foreach (var orderDetail in newOrder.OrderDetails)
                                    // {
                                    //     table.AddRow(newOrder.CustomerId.ToString(), newOrder.SellerId.ToString(), newOrder.OrderDate.ToString(), orderDetail.TabaccoId.ToString(), orderDetail.Quantity.ToString());
                                    // }

                                    // AnsiConsole.Render(table);
                                    
                                    Console.WriteLine($"Total Amount: {totalAmount} VND");



            
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
                                    Console.ReadKey();}
                                   
                                    break;
                                case 3:
                                Console.WriteLine("---- Cancel Order ----");

                                List<Order> allOrders = orderManager.GetAllOrders();

                                if (allOrders.Count == 0)
                                {
                                    Console.WriteLine("There are no orders to cancel.");
                                }
                                else
                                {
                                    Console.WriteLine("Orders:");
                                    foreach (var order in allOrders)
                                    {
                                        Console.WriteLine($"{order.OrderId}. Customer: {order.CustomerName} Status: {order.OrderStatus}");
                                    }

                                    Console.Write("Select Order to Cancel (ID): ");
                                    int selectedOrderId = int.Parse(Console.ReadLine());

                                    Order selectedOrder = allOrders.FirstOrDefault(o => o.OrderId == selectedOrderId);
                                    if (selectedOrder == null)
                                    {
                                        Console.WriteLine("Order not found.");
                                    }
                                    else
                                    {
                                        Console.Write($"Are you sure you want to cancel the order for customer {selectedOrder.CustomerName}? (y/n): ");
                                        string userInput = Console.ReadLine().ToLower();
                                        if (userInput == "y" || userInput == "yes")
                                        {
                                            bool success = orderManager.CancelOrder(selectedOrder);
                                            if (success)
                                            {
                                                Console.WriteLine("Order cancelled successfully!");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Failed to cancel the order.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Order cancellation aborted.");
                                        }
                                    }
                                }

                                Console.ReadKey();
                                Console.Clear();
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