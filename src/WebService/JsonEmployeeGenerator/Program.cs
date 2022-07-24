using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/* 
Migrate the console application to .NET 6.
Use var everywhere, for the sake of consistency.
Give variables more meaningful names (e => employee, i => line etc.).
Get rid of unnecessary empty lines.
*/
namespace JsonEmployeeGenerator
{
    // Make the class public.
    class Program
    {
        // Make the Main method public and remove unused args.
        // Make the method async Task<int> instead of void.
        static void Main(string[] args)
        {
            // You can declare all roles and teams in a separate Constants class or introduce an enum for teams.
            // Get rid of unnecessary empty line.
            string[] roles = new string[] { "Junior Developer", "Semi Senior Developer", "Senior Developer", "Principal", "Team Leader" };
            string[] teams = new string[] { "Platform", "Sales", "Billing", "Mirage" };

            // Get rid of unnecessary empty line.
            var generator = new Random();
            // You can use StreamReader and await for the operation.
            // Validate if resulting string array is null or empty before you proceed.
            // Traditionally, variables in C# are named using camel case (not snake case).
            var all_lines_in_file = File.ReadAllLines("employees.txt").ToArray();
            var employees = new List<JsonEmployee>();
            // Employees will be used in relational databases which use primary keys that start from 1. Please, change i to 1.
            // Rename 'i' to something meaningful like 'line'.
            for (int i = 0; i < all_lines_in_file.Length; i++)
            {
                // You can initialize the object with an object initializer instead of the empty constructor + set properties.
                // For the conditional statements (if (i < 11)) you can use ternary operators inside the object initializer.
                // Rename 'e' to something more meaningful, like 'employee'.
                JsonEmployee e = new JsonEmployee();
                e.Id = i;
                e.Name = all_lines_in_file[i].Split('\t')[0];
                e.SurName = all_lines_in_file[i].Split('\t')[1];
                e.Email = all_lines_in_file[i].Split('\t')[2];
                e.Age = generator.Next(18, 66);
                // Personally, I prefer to leave an empty line before a conditional statement or a loop.
                // You can use ternary operators for e.Role, e.ManagerId and e.Teams.
                if (i < 11)
                {
                    // See line 20
                    e.Role = "Manager";
                    e.Teams = new List<string>();
                }
                else
                {
                    e.ManagerId = generator.Next(11);
                    e.Role = roles[generator.Next(4)];
                    int count = generator.Next(1, 4);
                    var employeeTeams = new List<string>();
                    // This will generate identical teams for some employees, like "Sales", "Sales", "Billing".
                    // You can use HashSet for employeeTeams or think of another mechanism to avoid this issue.
                    for (int j = 0; j < count; ++j)
                    {
                        employeeTeams.Add(teams[generator.Next(4)]);
                    }
                    e.Teams = employeeTeams;
                }

                employees.Add(e);
            }
            // You can use StreamWriter and await for the operation.
            // Another approach would be to use StringBuilder and AppendLine.
            // When you get the whole string ready, then you could create the whole file at once, asynchronously.
            var jsonFile = File.CreateText("employees.json");
            jsonFile.WriteLine("[");

            for (int i = 0; i < employees.Count; ++i)
            {
                var jsonEmployee = employees[i];
                // Move this variable outside the bounds of the for loop. No need to be initialized every time.
                // Rename 'str' to something more meaningful.
                string str =
                    "{{\"Id\":{7},\"ManagerId\":{0},\"Age\":{1},\"Teams\":[{2}],\"Role\":\"{3}\",\"Email\":\"{4}\",\"SurName\":\"{5}\",\"Name\":\"{6}\"}}";
                // Use block for this if statement in order to be consistent.
                if (i != employees.Count - 1)
                    str += ",";
                // formattedEmployees?
                // All parameters passed to the method should be either all on a separate line (incl. str) or should be on a single line with the method itself.
                var formattedEmployeed = string.Format(str,
                    jsonEmployee.ManagerId.HasValue ? jsonEmployee.ManagerId.ToString() : "null",
                    jsonEmployee.Age,
                    string.Join(",", jsonEmployee.Teams.Select(x => "\"" + x + "\"")),
                    jsonEmployee.Role,
                    jsonEmployee.Email,
                    jsonEmployee.SurName,
                    jsonEmployee.Name,
                    jsonEmployee.Id);
                jsonFile.WriteLine(formattedEmployeed);
            }
            // Personally, I prefer to leave an empty line between a conditional statement or a loop and the code that follows.
            jsonFile.WriteLine("]");
            // You can use StreamWriter and await for the operation.
            jsonFile.Flush();
        }
        // Get rid of unnecessary empty line.
    }

    // Move to a separate file.
    internal class JsonEmployee
    {
        public string Name { get; set; }
        // Rename to Surname.
        public string SurName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
        public int? ManagerId { get; set; }
        public List<string> Teams { get; set; }
        // Classes having an 'Id' property usually start with it. Move up.
        public int Id { get; set; }
    }
}
