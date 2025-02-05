using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Employee_Management_System
{
    class Employee
    {

        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public DateTime DateHired { get; set; }
        public decimal Salary { get; set; }

        public override string ToString()
        {
            return $"ID: {EmployeeID}, Name: {FirstName} {LastName}, DOB: {DateOfBirth.ToShortDateString()}, Email: {Email}, Position: {Position}, Hired: {DateHired.ToShortDateString()}, Salary: {Salary}";
        }
    }

    class Program
    {
        static List<Employee> employees = new List<Employee>();
        static int nextEmployeeId = 1; // To auto-generate IDs

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nEmployee Management System");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. View Employees");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        ViewEmployees();
                        break;
                    case "3":
                        UpdateEmployee();
                        break;
                    case "4":
                        DeleteEmployee();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }


        // CRUD Operations

        static void AddEmployee()
        {
            try
            {
                Employee newEmployee = new Employee();
                newEmployee.EmployeeID = nextEmployeeId++; // Auto-generate ID

                Console.Write("First Name: ");
                newEmployee.FirstName = Console.ReadLine();
                Console.Write("Last Name: ");
                newEmployee.LastName = Console.ReadLine();

                DateTime dob;
                while (true)
                {
                    Console.Write("Date of Birth (yyyy-mm-dd): ");
                    if (DateTime.TryParse(Console.ReadLine(), out dob) && CalculateAge(dob) >= 18)
                    {
                        newEmployee.DateOfBirth = dob;
                        break;
                    }
                    Console.WriteLine("Invalid date of birth or age less than 18.");
                }

                while (true)
                {
                    Console.Write("Email: ");
                    string email = Console.ReadLine();
                    if (IsValidEmail(email))
                    {
                        newEmployee.Email = email;
                        break;
                    }
                    Console.WriteLine("Invalid email format.");
                }

                Console.Write("Position: ");
                newEmployee.Position = Console.ReadLine();

                DateTime hireDate;
                Console.Write("Date Hired (yyyy-mm-dd): ");
                while (!DateTime.TryParse(Console.ReadLine(), out hireDate))
                {
                    Console.WriteLine("Invalid date format.");
                }
                newEmployee.DateHired = hireDate;


                decimal salary;
                while (true)
                {
                    Console.Write("Salary: ");
                    if (decimal.TryParse(Console.ReadLine(), out salary) && salary > 0)
                    {
                        newEmployee.Salary = salary;
                        break;
                    }
                    Console.WriteLine("Invalid salary (must be a positive number).");
                }

                employees.Add(newEmployee);
                Console.WriteLine("Employee added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee: {ex.Message}");
            }

        }

        static int CalculateAge(DateTime dob)
        {
            int age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.Month < dob.Month || (DateTime.Now.Month == dob.Month && DateTime.Now.Day < dob.Day))
            {
                age--;
            }
            return age;
        }


        static void ViewEmployees()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees found.");
            }
            else
            {
                foreach (var emp in employees)
                {
                    Console.WriteLine(emp);
                }
            }
        }

        static void UpdateEmployee()
        {
            Console.Write("Enter Employee ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var employee = employees.Find(e => e.EmployeeID == id);
                if (employee != null)
                {
                    // ... (Get updated information from the user – similar to AddEmployee, but pre-fill with existing data)
                    Console.WriteLine("Enter new details (or press Enter to keep current value):");

                    Console.Write($"First Name ({employee.FirstName}): ");
                    string firstName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(firstName)) employee.FirstName = firstName;

                    Console.Write($"Last Name ({employee.LastName}): ");
                    string lastName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(lastName)) employee.LastName = lastName;

                    // ... (Similar prompts for other fields like DOB, Email, Position, Salary)
                    DateTime dob;
                    Console.Write($"Date of Birth ({employee.DateOfBirth.ToShortDateString()}): ");
                    string dobStr = Console.ReadLine();
                    if (DateTime.TryParse(dobStr, out dob) && CalculateAge(dob) >= 18)
                    {
                        employee.DateOfBirth = dob;
                    }
                    else if (!string.IsNullOrEmpty(dobStr))
                    {
                        Console.WriteLine("Invalid date of birth or age less than 18.");
                    }

                    Console.Write($"Email ({employee.Email}): ");
                    string email = Console.ReadLine();
                    if (IsValidEmail(email))
                    {
                        employee.Email = email;
                    }
                    else if (!string.IsNullOrEmpty(email))
                    {
                        Console.WriteLine("Invalid email format.");
                    }

                    Console.Write($"Position ({employee.Position}): ");
                    string position = Console.ReadLine();
                    if (!string.IsNullOrEmpty(position)) employee.Position = position;

                    decimal salary;
                    Console.Write($"Salary ({employee.Salary}): ");
                    string salaryStr = Console.ReadLine();
                    if (decimal.TryParse(salaryStr, out salary) && salary > 0)
                    {
                        employee.Salary = salary;
                    }
                    else if (!string.IsNullOrEmpty(salaryStr))
                    {
                        Console.WriteLine("Invalid salary (must be a positive number).");
                    }




                    Console.WriteLine("Employee updated successfully.");
                }
                else
                {
                    Console.WriteLine("Employee not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Employee ID.");
            }
        }

        static void DeleteEmployee()
        {
            Console.Write("Enter Employee ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var employee = employees.Find(e => e.EmployeeID == id);
                if (employee != null)
                {
                    employees.Remove(employee);
                    Console.WriteLine("Employee deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Employee not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Employee ID.");
            }
        }

        static bool IsValidEmail(string email)
        {
            // Basic email validation using regular expression (you can use a more robust regex)
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
    