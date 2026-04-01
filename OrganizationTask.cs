using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticalWork
{
    public abstract class OrganizationComponent {
        public string Name { get; set; }
        public abstract void Display(int depth);
        public abstract decimal GetSalary();
        public abstract int GetCount();
    }

    public class Employee : OrganizationComponent {
        public string Position { get; set; }
        private decimal _salary;

        public Employee(string name, string position, decimal salary) {
            Name = name;
            Position = position;
            _salary = salary;
        }

        public override void Display(int depth) => 
            Console.WriteLine(new string('-', depth) + " " + Name + " (" + Position + ")");

        public override decimal GetSalary() => _salary;
        public override int GetCount() => 1;
    }

    public class Department : OrganizationComponent {
        private List<OrganizationComponent> _children = new List<OrganizationComponent>();

        public Department(string name) { Name = name; }

        public void Add(OrganizationComponent component) => _children.Add(component);

        public override void Display(int depth) {
            Console.WriteLine(new string('+', depth) + " Отдел: " + Name);
            foreach (var child in _children) child.Display(depth + 2);
        }

        public override decimal GetSalary() => _children.Sum(c => c.GetSalary());
        public override int GetCount() => _children.Sum(c => c.GetCount());
    }
}