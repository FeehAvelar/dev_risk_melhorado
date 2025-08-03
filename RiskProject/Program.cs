// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using RiskProject.Domain;
using RiskProject.Domain.Interfaces;
using RiskProject.Services;
using System.Globalization;
using System.Reflection;

#region Dependency_Container
var services = new ServiceCollection();

var assembly = Assembly.GetExecutingAssembly();

var categoryTypes = assembly
    .GetTypes()
    .Where(type => typeof(ICategory).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

foreach (var type in categoryTypes)
{
    services.AddSingleton(typeof(ICategory), type);
}

services.AddScoped<TraderService>();

var serviceProvider = services.BuildServiceProvider();
#endregion


#region program
var inputedReferenceDate = new PaymentDateObjectValue(Console.ReadLine().Trim());
//var inputedReferenceDate = new PaymentDateObjectValue("12/11/2020");
var numberOfOpereations = uint.Parse(Console.ReadLine());
//var numberOfOpereations = 4;

ITrade[] trades = new ITrade[numberOfOpereations];
for (int i = 0; i < numberOfOpereations; i++)
{
    ITrade trader = new Trade(Console.ReadLine().Trim());

    trades[i] = trader;
}

//ITrade[] trades = new ITrade[] { new Trade("2000000 Private 12/29/2025"), new Trade("400000 Public 07/01/2020"), new Trade("5000000 Public 01/02/2024"), new Trade("3000000 Public 10/26/2023") };

using (var scope = serviceProvider.CreateScope())
{
    var service = scope.ServiceProvider.GetRequiredService<TraderService>(); 
    
    foreach (var trade in trades)
    {
        var tradeCategory = service.ClassifyCategory(trade, inputedReferenceDate.Value);
        Console.WriteLine(tradeCategory);
    }
}
#endregion