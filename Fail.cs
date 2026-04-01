using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticalWork
{
    public abstract class FileSystemComponent {
        public string Name { get; set; } = string.Empty;
        public abstract void Display(int depth);
        public abstract int GetSize();
    }

    public class MyFile : FileSystemComponent {
        private int _size;
        public MyFile(string name, int size) { Name = name; _size = size; }
        public override void Display(int depth) => 
            Console.WriteLine(new string(' ', depth) + " Файл: " + Name + " (" + _size + " KB)");
        public override int GetSize() => _size;
    }

    public class MyDirectory : FileSystemComponent {
        private List<FileSystemComponent> _components = new List<FileSystemComponent>();
        public MyDirectory(string name) { Name = name; }
        
        public void Add(FileSystemComponent component) {
            if (!_components.Any(c => c.Name == component.Name)) _components.Add(component);
        }

        public override void Display(int depth) {
            Console.WriteLine(new string(' ', depth) + "+ Папка: " + Name);
            foreach (var c in _components) c.Display(depth + 2);
        }
        public override int GetSize() => _components.Sum(c => c.GetSize());
    }
}