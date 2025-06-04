using System;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Pobierz rok ze standardowego wejścia
        int year = int.Parse(Console.ReadLine());

        // Pobierz i załaduj XML
        string xmlInput = "";
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            xmlInput += line;
        }
        XDocument doc = XDocument.Parse(xmlInput);

        // Zapytanie LINQ
        var result = from customer in doc.Descendants("Customer")
            where (from order in customer.Element("Orders").Elements("Order")
                let orderDate = DateTime.Parse(order.Element("OrderDate").Value)
                where orderDate.Year == year
                select order).Any()
            orderby customer.Element("Country").Value,
                customer.Element("City").Value,
                customer.Element("CompanyName").Value
            select customer.Element("CompanyName").Value;

        // Wypisz wyniki
        if (!result.Any())
        {
            Console.WriteLine("empty");
        }
        else
        {
            foreach (var companyName in result)
            {
                Console.WriteLine(companyName);
            }
        }
    }
}