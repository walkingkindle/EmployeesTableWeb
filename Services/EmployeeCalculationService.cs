using EmployeesTableWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace EmployeesTableWeb.Services
{
    public class EmployeeCalculationService
    {
        public static void calculateTotalHours(List<Employee> employees)
        {
            // Calculate total hours worked for each employee
            foreach (var employee in employees)
            {
                // Calculate time difference between end and start time
                TimeSpan difference = employee.EndTimeUtc - employee.StarTimeUtc;

                // Convert total minutes to hours
                double totalMinutes = difference.TotalMinutes;
                employee.totalHours = (int)(totalMinutes / 60);

                // Calculate remaining minutes
                int remainingMinutes = (int)(totalMinutes % 60);

            }
        }




        public static Dictionary<string, double> WorkPerEmployeePercentage(List<Employee> employees)
        {
            // Calculate total hours worked by all employees
            double totalTime = employees.Sum(emp => emp.totalHours);

            // Initialize dictionary to store percentages
            Dictionary<string, double> percentages = new Dictionary<string, double>();
            foreach (var employee in employees)
            {
                if (employee.EmployeeName != null)
                {
                    // Initialize percentage value for each employee
                    if (!percentages.ContainsKey(employee.EmployeeName))
                    {
                        percentages[employee.EmployeeName] = 0;
                    }
                    // Calculate percentage of total work for each employee
                    percentages[employee.EmployeeName] += employee.totalHours / totalTime * 100;
                }
            }

            return percentages;
        }
        // Calculate the total hours worked by each employee
        public static Dictionary<string, int> CalculateEmployeeHours(List<Employee> employees)
        {
            // Initialize dictionary to store total hours worked by each employee
            Dictionary<string, int> employeeWorkHours = new Dictionary<string, int>();
            foreach (var employee in employees)
            {
                if (employee.EmployeeName != null)
                {
                    // Update total hours for existing employees
                    if (employeeWorkHours.ContainsKey(employee.EmployeeName))
                    {
                        employeeWorkHours[employee.EmployeeName] += employee.totalHours;
                    }
                    // Add new employees to the dictionary
                    else
                    {
                        employeeWorkHours[employee.EmployeeName] = employee.totalHours;
                    }
                }
            }
            // Order the dictionary by total hours worked in descending order
            var sortedEmployeeWorkHours = employeeWorkHours.OrderByDescending(kv => kv.Value)
                                              .ToDictionary(kv => kv.Key, kv => kv.Value);
            return sortedEmployeeWorkHours;
        }

    }
}

