using System;
using PracticalWork;

class Program
{
    static void Main()
    {
        // Сценарий 1: Фасад
        Console.WriteLine("--- Домашнее задание: Фасад ---");
        HomeTheaterFacade home = new HomeTheaterFacade();
        home.StartMovie();
        home.StartGame("Spider-Man");

        // Сценарий 2: Компоновщик
        Console.WriteLine("\n--- Домашнее задание: Компоновщик ---");
        MyDirectory root = new MyDirectory("Root");
        MyDirectory music = new MyDirectory("Music");
        
        music.Add(new MyFile("Song.mp3", 5000));
        root.Add(music);
        root.Add(new MyFile("Config.ini", 1));

        root.Display(0);
        Console.WriteLine($"\nОбщий размер: {root.GetSize()} KB");

        Console.ReadKey();
    }
}