using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace EmployeesTableWeb.Services
{
    public class PieChart
    {
         public static void CreateAndSavePieChart(Dictionary<string, double> percentages)
        {
            // Create a new Chart control
            Chart chart = new Chart();
            chart.Size = new Size(600, 400);

            // Add a chart area
            chart.ChartAreas.Add(new ChartArea("MainChartArea"));

            // Add a series to the chart
            Series series = new Series("Series1");
            series.ChartType = SeriesChartType.Pie;

            // Calculate the total percentage
            double totalPercentage = percentages.Values.Sum();

            foreach (var kvp in percentages)
            {
                // Calculate the percentage value for each employee
                double employeePercentage = kvp.Value / totalPercentage * 100;

                // Add the data point to the series
                series.Points.AddXY($"{kvp.Key} ({employeePercentage:F2}%)", employeePercentage);
            }
            chart.Series.Add(series);

            // Customize chart appearance
            chart.Series["Series1"]["PieLabelStyle"] = "Outside";
            chart.Series["Series1"]["PieLineColor"] = "Black";

            // Rotate the axis labels vertically
            chart.ChartAreas["MainChartArea"].AxisX.LabelStyle.Angle = -90;

            // Save the chart as an image file
            string filepath = "piechart.png";
            chart.SaveImage(filepath, ChartImageFormat.Png);

            // Dispose of the chart to release resources
            chart.Dispose();

            Console.WriteLine($"Pie chart saved as: {Path.GetFullPath(filepath)}");
        }
        
    }
}

