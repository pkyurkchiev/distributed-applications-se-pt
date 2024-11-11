# GustoHub API

### Restaurant Orders Management API

### Overview:
The *GustoHub API* is a system designed for managing orders within a restaurant environment, developed with ASP.NET Web API and Entity Framework Core. It enables restaurant staff to track menu items, manage orders, and handle customer and employee data efficiently. The application maintains a flexible and efficient database structure, allowing for easy addition, editing, and tracking of all aspects of customer service.

The API provides a complete set of CRUD (Create, Read, Update, Delete) operations for dishes, categories, orders, employees, and customers, supporting the complex relationships between them. This system is built to be highly scalable, making it suitable for restaurants of various sizes and order volumes.

### Key Features:
1. **Dish and Category Management 🍲:**
   - Keep detailed information about all dishes on the menu, including name, price, and category.
   - Classify dishes by category, such as appetizers, main courses, desserts, and more.

2. **Order Management 📋:**
   - Create and track orders with detailed item information and quantities.
   - Support order statuses, such as new, in preparation, and completed, for smoother order tracking.

3. **Customer Management 👥:**
   - Store customer information, including name, email, and phone.
   - Maintain a history of each customer’s orders, helping build loyalty programs.

4. **Employee Management 🧑‍🍳:**
   - Record details of employees who handle orders, including position and hire date.
   - Associate orders with the employees managing them for better service tracking.

5. **Relationships (Many-to-Many and One-to-Many) 🔗:**
   - Many-to-Many relationship between orders and dishes, allowing each order to include multiple dishes, and each dish to appear in multiple orders.
   - One-to-Many relationships between orders and customers as well as orders and employees, ensuring a complete view of each order’s service details.

### Architecture and Technologies:
- **Tech Stack:** ASP.NET Web API, Entity Framework Core, SQL Server
- **Database Structure:** SQL database with six tables and multiple relationships, ensuring quick and flexible data access.
- **API Documentation:** Includes a `Postman` collection for API testing and demonstration.

### Example Use Cases:
- A **restaurant manager** can update the menu using the API, adding new dishes and categories to keep the offerings current.
- An **order processor** can create and monitor new orders, adding details for each dish and the customer who placed the order.
- A **chef** can use the order information to prepare the dishes required for each order.
- The **marketing team** can analyze data from the API to track the most ordered dishes and customer preferences.

### Benefits and Highlights:
- **Comprehensive Order and Menu Management**: The API allows seamless adding, updating, and tracking of each order, enhancing service quality.
- **Enhanced Performance**: Built on ASP.NET Core, the system is optimized for performance and easy maintenance.
- **Flexible Structure**: Designed to be extendable, the API can easily incorporate additional features, like new categories, order statuses, and more.

This API represents a modern, efficient solution for restaurant operations, offering streamlined service and enhancing customer satisfaction through better management of orders and menu items.
