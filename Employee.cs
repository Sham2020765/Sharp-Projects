using System;

namespace EmployeeAssignment
{
    // Defines the Employee class
    public class Employee
    {
        // Property to store the unique ID of the employee
        public int Id { get; set; }

        // Property to store the first name of the employee
        public string FirstName { get; set; }

        // Property to store the last name of the employee
        public string LastName { get; set; }

        // Overloading the "==" operator to compare two Employee objects by their Id
        public static bool operator ==(Employee emp1, Employee emp2)
        {
            // Handle null checks to avoid NullReferenceException if an object is empty
            if (ReferenceEquals(emp1, null) && ReferenceEquals(emp2, null))
            {
                return true;
            }
            if (ReferenceEquals(emp1, null) || ReferenceEquals(emp2, null))
            {
                return false;
            }

            // Return true if both IDs match, otherwise return false
            return emp1.Id == emp2.Id;
        }

        // Comparison operators must be overloaded in pairs; overloading the "!=" operator
        public static bool operator !=(Employee emp1, Employee emp2)
        {
            // Returns the exact opposite result of the "==" operator
            return !(emp1 == emp2);
        }
    }
}
