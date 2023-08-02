using BL;
using Model;

//class Program
//{
//      static void Main()
//      {
//         CustomerBL cusBL = new CustomerBL();
//         Customer? cus = new Customer();
//         cus = cusBL.GetById(2);
//         if (cus != null)
//             Console.WriteLine(cus.CustomerName);
// TabaccoBL idal = new TabaccoBL();
// Tabacco? tab = new Tabacco();
// tab = idal.GetTabaccoById(1);
// if (tab != null)
// Console.WriteLine(tab.TabaccoName);
//      }
//  }

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
            CustomerBL cbl = new CustomerBL();
            OrderBL obl = new OrderBL();
            StaffBL sta = new StaffBL();
            string[] Login = {"login"};
            List<Tabacco> lst;
            do
            {
                StaffBL staffBL = new StaffBL();
                Staff? orderStaff;
                bool active = true;
                while (active = true)
                {
                string UserName;
                Console.WriteLine(@"
████████  █████  ██████   █████   ██████  ██████  ██████      ███████ ████████  ██████  ██████  ███████ 
   ██    ██   ██ ██   ██ ██   ██ ██      ██      ██    ██     ██         ██    ██    ██ ██   ██ ██      
   ██    ███████ ██████  ███████ ██      ██      ██    ██     ███████    ██    ██    ██ ██████  █████   
   ██    ██   ██ ██   ██ ██   ██ ██      ██      ██    ██          ██    ██    ██    ██ ██   ██ ██      
   ██    ██   ██ ██████  ██   ██  ██████  ██████  ██████      ███████    ██     ██████  ██   ██ ███████
                    ");
                Console.Write("User Name : ");
                UserName = Console.ReadLine()??"";
                if(UserName == "0")
                {
                    active = false;
                    break;
                }
                else
                {
                    orderStaff = staffBL.Login(UserName);
                }
                    Console.Clear();
                if(orderStaff != null)
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
                                                Console.WriteLine("Tabacco Price: " + i.TabaccoPrice);
                                                Console.WriteLine("Amount: " + i.Amount);
                                                Console.WriteLine("Tabacco datetime: " + i.TabaccoDate);
                                                Console.WriteLine("Tabacco pack: "+ i.Pack);
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
                                        Console.WriteLine("\nTabacco Count: " + lst.Count);
                                        break;
                                    case 3:
                                        lst = ibl.GetByName("I");
                                        Console.WriteLine("\nTabacco Count By Name: " + lst.Count);
                                        break;
                                        
                                }
                            } while (imChoose != imMenu.Length);
                            break;
                            case 2:
                            // order.OrderCustomer = new Customer { CustmerId = 1, CustomerName = "Nguyen Xuan Sinh", CustomerAddress = "Hanoi" };
                                Order order = new Order();
                                order.TabaccosList.Add(ibl.GetTabaccoById(2));
                                order.TabaccosList[0].Amount = 1;
                                order.TabaccosList.Add(ibl.GetTabaccoById(3));
                                order.TabaccosList[1].Amount = 2;
                                Console.WriteLine("Create Order: " + (obl.CreateOrder(order) ? "completed!" : "not complete!"));
                        // Customer c = new Customer {CustomerName="Nguyen Thi N", CustomerAddress="Ha Tay"};
                        // Console.WriteLine("Customer ID: " + cbl.AddCustomer(c));
                                break;
                            case 3:
                                
                            
                                
                                break;
                            
                        }
                        
                    }}
                    else
                    {
                        Console.WriteLine("\nInvalid Username or Password !");
                    }
                    
                }
            }while (mainChoose != mainMenu.Length);
        }

        private static short Menu(string title, string[] menuTabaccos)
        {
            string logo = @"============================================================================================


████████  █████  ██████   █████   ██████  ██████  ██████      ███████ ████████  ██████  ██████  ███████ 
   ██    ██   ██ ██   ██ ██   ██ ██      ██      ██    ██     ██         ██    ██    ██ ██   ██ ██      
   ██    ███████ ██████  ███████ ██      ██      ██    ██     ███████    ██    ██    ██ ██████  █████   
   ██    ██   ██ ██   ██ ██   ██ ██      ██      ██    ██          ██    ██    ██    ██ ██   ██ ██      
   ██    ██   ██ ██████  ██   ██  ██████  ██████  ██████      ███████    ██     ██████  ██   ██ ███████  
                                        
                                       ";
            short choose = 0;
            Console.WriteLine($"\n"+logo);
            string line = "============================================================================================";
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
                    choose = Int16.Parse(Console.ReadLine()??"");
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