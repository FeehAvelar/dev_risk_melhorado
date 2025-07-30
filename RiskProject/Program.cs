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
    services.AddScoped(typeof(ICategory), type);
}

services.AddScoped<TraderService>();

var serviceProvider = services.BuildServiceProvider();
#endregion


#region program
var inputedReferenceDate = DateTime.ParseExact(Console.ReadLine().Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

var numberOfOpereations = uint.Parse(Console.ReadLine());

ITrade[] trades = new ITrade[numberOfOpereations];
for (int i = 0; i < numberOfOpereations; i++)
{
    ITrade trader = new Trade(Console.ReadLine().Trim());

    trades[i] = trader;
}

using (var scope = serviceProvider.CreateScope())
{
    var service = scope.ServiceProvider.GetRequiredService<TraderService>(); 
    
    foreach (var trade in trades)
    {
        var tradeCategory = service.ClassifyCategory(trade, inputedReferenceDate);
        Console.WriteLine(tradeCategory);
    }
}
#endregion