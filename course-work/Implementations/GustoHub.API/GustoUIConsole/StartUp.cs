using GustoHub.Data.ViewModels.POST;
using GustoHub.Data.ViewModels.PUT;
using GustoUIConsole.Services;

namespace GustoUIConsole
{
    internal class StartUp
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("In order to use the application you must provide an API Key.");
            Console.WriteLine("Please, enter a valid API Key:");

            string apiKey = Console.ReadLine();

            if (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("Sorry, you cannot use the service.");
                return;
            }

            ApiServiceBase.SetApiKey(apiKey);

            while (true)
            {
                Console.Clear();                
                Console.WriteLine("=== GustoHub API Client ===");
                Console.WriteLine("1. Categories");
                Console.WriteLine("2. Customers");
                Console.WriteLine("3. Dishes");
                Console.WriteLine("4. Orders");
                Console.WriteLine("5. Employees");
                Console.WriteLine("6. Users");
                Console.WriteLine("7. API Keys");
                Console.WriteLine("0. Exit");
                Console.Write("Choose module: ");

                switch (Console.ReadLine())
                {
                    case "1": await HandleCategories(); break;
                    case "2": await HandleCustomers(); break;
                    case "3": await HandleDishes(); break;
                    case "4": await HandleOrders(); break;
                    case "5": await HandleEmployees(); break;
                    case "6": await HandleUsers(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static async Task HandleCategories()
        {
            var service = new CategoryService();
            Console.WriteLine("\n=== Categories ===");
            Console.WriteLine("1. List all");
            Console.WriteLine("2. Get by name");
            Console.WriteLine("3. Create");
            Console.WriteLine("4. Update");
            Console.WriteLine("5. Delete");
            Console.Write("Operation: ");

            switch (Console.ReadLine())
            {
                case "1":
                    await service.GetAllCategories();
                    break;
                case "2":
                    Console.Write("Category name: ");
                    await service.GetCategoryByName(Console.ReadLine());
                    break;
                case "3":
                    Console.Write("Category name: ");
                    await service.CreateCategory(new POSTCategoryDto { Name = Console.ReadLine() });
                    break;
                case "4":
                    Console.Write("Category ID: ");
                    var id = int.Parse(Console.ReadLine());
                    Console.Write("New name: ");
                    await service.UpdateCategory(id, new PUTCategoryDto { Name = Console.ReadLine() });
                    break;
                case "5":
                    Console.Write("Category ID: ");
                    await service.DeleteCategory(int.Parse(Console.ReadLine()));
                    break;
            }
        }
        private static async Task HandleCustomers()
        {
            var service = new CustomerService();
            Console.WriteLine("\n=== Customers Management ===");
            Console.WriteLine("1. List all customers");
            Console.WriteLine("2. Get customer by name");
            Console.WriteLine("3. Create customer");
            Console.WriteLine("4. Update customer");
            Console.WriteLine("5. Delete customer");
            Console.Write("Choose operation: ");

            switch (Console.ReadLine())
            {
                case "1":
                    await service.GetAllCustomers();
                    break;
                case "2":
                    var name = ReadRequiredString("Enter customer name: ");
                    await service.GetCustomerByName(name);
                    break;
                case "3":
                    var newCustomer = new POSTCustomerDto
                    {
                        Name = ReadRequiredString("Name: "),
                        Email = ReadRequiredString("Email: "),
                        Phone = ReadOptionalString("Phone: ")
                    };
                    await service.CreateCustomer(newCustomer);
                    break;
                case "4":
                    var updateId = ReadRequiredString("Customer ID to update: ");
                    var updateData = new PUTCustomerDto
                    {
                        Name = ReadRequiredString("New name: "),
                        Email = ReadRequiredString("New email: "),
                        Phone = ReadOptionalString("New phone: ")
                    };
                    await service.UpdateCustomer(updateId, updateData);
                    break;
                case "5":
                    var deleteId = ReadRequiredString("Customer ID to delete: ");
                    await service.DeleteCustomer(deleteId);
                    break;
            }
        }
        private static async Task HandleDishes()
        {
            var service = new DishService();
            Console.WriteLine("\n=== Dishes Management ===");
            Console.WriteLine("1. List all dishes");
            Console.WriteLine("2. Get dish by name");
            Console.WriteLine("3. Create dish");
            Console.WriteLine("4. Update dish");
            Console.WriteLine("5. Delete dish");
            Console.Write("Choose operation: ");

            switch (Console.ReadLine())
            {
                case "1":
                    await service.GetAllDishes();
                    break;
                case "2":
                    var name = ReadRequiredString("Enter dish name: ");
                    await service.GetDishByName(name);
                    break;
                case "3":
                    var newDish = new POSTDishDto
                    {
                        Name = ReadRequiredString("Name: "),
                        Price = ReadDecimal("Price: "),
                        CategoryId = ReadInt("Category ID: ")
                    };
                    await service.CreateDish(newDish);
                    break;
                case "4":
                    var updateId = ReadInt("Dish ID to update: ");
                    var updateData = new PUTDishDto
                    {
                        Name = ReadRequiredString("New name: "),
                        Price = ReadDecimal("New price: "),
                        CategoryId = ReadInt("New category ID: ")
                    };
                    await service.UpdateDish(updateId, updateData);
                    break;
                case "5":
                    var deleteId = ReadInt("Dish ID to delete: ");
                    await service.DeleteDish(deleteId);
                    break;
            }
        }
        private static async Task HandleOrders()
        {
            var service = new OrderService();
            Console.WriteLine("\n=== Orders Management ===");
            Console.WriteLine("1. List all orders");
            Console.WriteLine("2. Get order by date");
            Console.WriteLine("3. Create order");
            Console.WriteLine("4. Update order");
            Console.WriteLine("5. Delete order");
            Console.Write("Choose operation: ");

            switch (Console.ReadLine())
            {
                case "1":
                    await service.GetAllOrders();
                    break;
                case "2":
                    var date = ReadRequiredString("Enter date (YYYY-MM-DD): ");
                    await service.GetOrderByDate(date);
                    break;
                case "3":
                    var newOrder = new POSTOrderDto
                    {
                        CustomerId = ReadOptionalString("Customer ID (optional): "),
                        EmployeeId = ReadOptionalString("Employee ID (optional): "),
                        CompletionDate = ReadOptionalString("Completion date (optional): ")
                    };
                    await service.CreateOrder(newOrder);
                    break;
                case "4":
                    var updateId = ReadInt("Order ID to update: ");
                    var updateData = new PUTOrderDto
                    {
                        OrderDate = ReadRequiredString("Order date: "),
                        CompletionDate = ReadOptionalString("Completion date: "),
                        CustomerId = ReadOptionalString("Customer ID: "),
                        EmployeeId = ReadOptionalString("Employee ID: ")
                    };
                    await service.UpdateOrder(updateId, updateData);
                    break;
                case "5":
                    var deleteId = ReadInt("Order ID to delete: ");
                    await service.DeleteOrder(deleteId);
                    break;
            }
        }
        private static async Task HandleEmployees()
        {
            var service = new EmployeeService();
            Console.WriteLine("\n=== Employees Management ===");
            Console.WriteLine("1. List active employees");
            Console.WriteLine("2. List inactive employees");
            Console.WriteLine("3. Get employee by name");
            Console.WriteLine("4. Create employee");
            Console.WriteLine("5. Update employee");
            Console.WriteLine("6. Activate employee");
            Console.WriteLine("7. Deactivate employee");
            Console.Write("Choose operation: ");

            switch (Console.ReadLine())
            {
                case "1":
                    await service.GetActiveEmployees();
                    break;
                case "2":
                    await service.GetInactiveEmployees();
                    break;
                case "3":
                    var name = ReadRequiredString("Enter employee name: ");
                    await service.GetEmployeeByName(name);
                    break;
                case "4":
                    var newEmployee = new POSTEmployeeDto
                    {
                        Name = ReadRequiredString("Name: "),
                        Title = ReadRequiredString("Title: "),
                        HireDate = DateTime.Now.ToString("yyyy-MM-dd")
                    };

                    var userId = ReadGuid("User ID to make an Employee: ");

                    await service.CreateEmployee(newEmployee, userId.ToString());
                    break;
                case "5":
                    var updateId = ReadRequiredString("Employee ID to update: ");
                    var updateData = new PUTEmployeeDto
                    {
                        Name = ReadRequiredString("New name: "),
                        Title = ReadRequiredString("New title: "),
                        HireDate = ReadRequiredString("Hire date (YYYY-MM-DD): ")
                    };
                    await service.UpdateEmployee(updateId, updateData);
                    break;
                case "6":
                    var activateId = ReadRequiredString("Employee ID to activate: ");
                    await service.ActivateEmployee(activateId);
                    break;
                case "7":
                    var deactivateId = ReadRequiredString("Employee ID to deactivate: ");
                    await service.DeactivateEmployee(deactivateId);
                    break;
            }
        }
        private static async Task HandleUsers()
        {
            var service = new UserService();
            Console.WriteLine("\n=== Users Management ===");
            Console.WriteLine("1. Get user by ID");
            Console.WriteLine("2. Create user");
            Console.WriteLine("3. Update user");
            Console.WriteLine("4. Verify user");
            Console.Write("Choose operation: ");

            switch (Console.ReadLine())
            {
                case "1":
                    var userId = ReadRequiredString("Enter user ID: ");
                    await service.GetUserById(userId);
                    break;
                case "2":
                    var newUser = new POSTUserDto
                    {
                        Username = ReadRequiredString("Username: "),
                        Password = ReadRequiredString("Password: ")
                    };
                    await service.CreateUser(newUser);
                    break;
                case "3":
                    var updateId = ReadRequiredString("User ID to update: ");
                    var updateData = new PUTUserDto
                    {
                        IsVerified = ReadBool("Verified (true/false): "),
                        Role = ReadOptionalString("Role: ")
                    };
                    await service.UpdateUser(updateId, updateData);
                    break;
                case "4":
                    var verifyId = ReadRequiredString("User ID to verify: ");
                    await service.VerifyUser(verifyId);
                    break;
            }
        }

        private static string ReadRequiredString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input)) return input;
                Console.WriteLine("This field is required!");
            }
        }
        private static string ReadOptionalString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
        private static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out var result)) return result;
                Console.WriteLine("Invalid decimal value!");
            }
        }
        private static int ReadInt(string prompt = "")
        {
            while (true)
            {
                if (!string.IsNullOrEmpty(prompt)) Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out var result)) return result;
                Console.WriteLine("Invalid integer value!");
            }
        }
        private static bool ReadBool(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine().ToLower();
                if (input == "true") return true;
                if (input == "false") return false;
                Console.WriteLine("Must be 'true' or 'false'!");
            }
        }
        private static Guid ReadGuid(string prompt = "")
        {
            while (true)
            {
                if (!string.IsNullOrEmpty(prompt)) Console.Write(prompt);
                if (Guid.TryParse(Console.ReadLine(), out var result)) return result;
                Console.WriteLine("Invalid GUID format!");
            }
        }

    }
}
