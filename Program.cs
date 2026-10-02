using System;

namespace EmployeeAssignment
{
    class Program
    {
        // The Main method where the application starts running
        static void Main(string[] args)
        {
            // Instantiate the first Employee object and assign values to its properties
            Employee employee1 = new Employee()
            {
                Id = 101,
                FirstName = "John",
                LastName = "Doe"
            };

            // Instantiate the second Employee object with the same ID to test equality
            Employee employee2 = new Employee()
            {
                Id = 101,
                FirstName = "Jane",
                LastName = "Smith"
            };

            // Instantiate a third Employee object with a different ID to test inequality
            Employee employee3 = new Employee()
            {
                Id = 102,
                FirstName = "Alex",
                LastName = "Jones"
            };

            // Display the details of all created employees in the console
            Console.WriteLine($"Employee 1: ID = {employee1.Id}, Name = {employee1.FirstName} {employee1.LastName}");
            Console.WriteLine($"Employee 2: ID = {employee2.Id}, Name = {employee2.FirstName} {employee2.LastName}");
            Console.WriteLine($"Employee 3: ID = {employee3.Id}, Name = {employee3.FirstName} {employee3.LastName}");
            Console.WriteLine(new string('-', 50));

            // Compare employee1 and employee2 using the overloaded "==" operator
            Console.WriteLine("Comparing Employee 1 and Employee 2 using '==':");
            if (employee1 == employee2)
            {
                Console.WriteLine("Result: The employees are EQUAL (IDs match).");
            }
            else
            {
                Console.WriteLine("Result: The employees are NOT EQUAL.");
            }

            Console.WriteLine(); // Blank line for clean spacing

            // Compare employee1 and employee3 using the overloaded "!=" operator
            Console.WriteLine("Comparing Employee 1 and Employee 3 using '!=':");
            if (employee1 != employee3)
            {
                Console.WriteLine("Result: The employees are NOT EQUAL (IDs are different).");
            }
            else
            {
                Console.WriteLine("Result: The employees are EQUAL.");
            }

            // Keep the console window open until a user key is pressed
            Console.ReadLine();
        }
    }
}
