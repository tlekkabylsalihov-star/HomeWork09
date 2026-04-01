using System;
using PracticalWork;

class Program
{
    static void Main()
    {
        Console.WriteLine("--- Тест Фасада ---");
        HotelFacade hotel = new HotelFacade();
        hotel.BookFullService(); // Бронирование с услугами [cite: 28]
        hotel.ReserveTableWithTaxi(); // Ресторан + такси [cite: 30]

        Console.WriteLine("\n--- Тест Компоновщика ---");
        Department headOffice = new Department("Головной офис");
        Department itDept = new Department("IT Отдел");
        
        itDept.Add(new Employee("Алексей", "Разработчик", 500000));
        headOffice.Add(itDept);
        headOffice.Add(new Employee("Иван", "Директор", 1000000));

        headOffice.Display(1);
        Console.WriteLine("\nОбщий бюджет: " + headOffice.GetSalary()); // [cite: 40]
        Console.WriteLine("Общий штат: " + headOffice.GetCount()); // [cite: 41]

        Console.ReadKey();
    }
}