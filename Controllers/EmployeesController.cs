using EmployeesTableWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using EmployeesTableWeb.Services;


namespace EmployeesWebApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public EmployeesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            string key = "vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";
            string apiUrl = $"https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code={key}";

            try
            {
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var jsonData = await response.Content.ReadAsStringAsync();
                var employees = JsonSerializer.Deserialize<List<Employee>>(jsonData);
                EmployeeCalculationService.calculateTotalHours(employees);
                Dictionary<string, int> calculatedHours = EmployeeCalculationService.CalculateEmployeeHours(employees);
                var percentages = EmployeeCalculationService.WorkPerEmployeePercentage(employees);
                PieChart.CreateAndSavePieChart(percentages);

                if (calculatedHours!= null && calculatedHours.Any())
                {
                    
                    return View(calculatedHours);
                }
                else
                {
                    return View("Error", "No Employee Data found.");
                }
            }
            catch (HttpRequestException ex)
            {
                return View("Error", $"Error fetching data: {ex.Message}");
            }
        }
    }
}
