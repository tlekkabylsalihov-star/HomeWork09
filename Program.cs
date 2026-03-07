using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticalWork
{
    // =========================================================
    // 1. ПАТТЕРН "КОМАНДА" (Smart Home)
    // =========================================================
    public interface ICommand {
        void Execute();
        void Undo();
    }

    public class Light {
        public void On() => Console.WriteLine("Свет включен");
        public void Off() => Console.WriteLine("Свет выключен");
    }

    public class TV {
        public void On() => Console.WriteLine("Телевизор включен");
        public void Off() => Console.WriteLine("Телевизор выключен");
    }

    public class LightOnCommand : ICommand {
        private Light _light;
        public LightOnCommand(Light light) => _light = light;
        public void Execute() => _light.On();
        public void Undo() => _light.Off();
    }

    public class MacroCommand : ICommand {
        private List<ICommand> _commands;
        public MacroCommand(List<ICommand> commands) => _commands = commands;
        public void Execute() => _commands.ForEach(c => c.Execute());
        public void Undo() {
            foreach (var cmd in Enumerable.Reverse(_commands)) cmd.Undo();
        }
    }

    public class RemoteControl {
        private Stack<ICommand> _history = new Stack<ICommand>();
        public void SetCommand(ICommand command) {
            command.Execute();
            _history.Push(command);
        }
        public void UndoButton() {
            if (_history.Count > 0) _history.Pop().Undo();
            else Console.WriteLine("История пуста!");
        }
    }

    // =========================================================
    // 2. ПАТТЕРН "ШАБЛОННЫЙ МЕТОД" (Отчеты)
    // =========================================================
    public abstract class ReportGenerator {
        public void GenerateReport() {
            CollectData();
            FormatData();
            if (CustomerWantsSave()) Save();
        }
        protected void CollectData() => Console.WriteLine("Сбор данных...");
        protected abstract void FormatData();
        protected virtual void Save() => Console.WriteLine("Отчет сохранен в файл.");
        protected virtual bool CustomerWantsSave() => true; 
    }

    public class PdfReport : ReportGenerator {
        protected override void FormatData() => Console.WriteLine("Форматирование в PDF...");
    }

    public class ExcelReport : ReportGenerator {
        protected override void FormatData() => Console.WriteLine("Форматирование в Excel...");
        protected override void Save() => Console.WriteLine("Excel-файл успешно экспортирован.");
    }

    // =========================================================
    // 3. ПАТТЕРН "ПОСРЕДНИК" (Чат)
    // =========================================================
    public interface IMediator {
        void SendMessage(string msg, User sender, string channel);
    }

    public class ChatMediator : IMediator {
        private Dictionary<string, List<User>> _channels = new Dictionary<string, List<User>>();

        public void AddToChannel(string channel, User user) {
            if (!_channels.ContainsKey(channel)) _channels[channel] = new List<User>();
            _channels[channel].Add(user);
            Console.WriteLine($"{user.Name} вошел в канал: {channel}");
        }

        public void SendMessage(string msg, User sender, string channel) {
            if (_channels.ContainsKey(channel)) {
                foreach (var u in _channels[channel].Where(u => u != sender))
                    u.Receive(msg, sender.Name);
            }
        }
    }

    public abstract class User {
        protected IMediator _mediator;
        public string Name { get; }
        public User(IMediator m, string n) { _mediator = m; Name = n; }
        public abstract void Send(string msg, string channel);
        public abstract void Receive(string msg, string from);
    }

    public class ChatUser : User {
        public ChatUser(IMediator m, string n) : base(m, n) {}
        public override void Send(string msg, string channel) => _mediator.SendMessage(msg, this, channel);
        public override void Receive(string msg, string from) => Console.WriteLine($"{Name} получил от {from}: {msg}");
    }

    // =========================================================
    // ЗАПУСК ПРОГРАММЫ
    // =========================================================
    class Program {
        static void Main() {
            Console.WriteLine("--- ТЕСТ: КОМАНДА ---");
            var remote = new RemoteControl();
            var light = new Light();
            var macro = new MacroCommand(new List<ICommand> { new LightOnCommand(light) });
            remote.SetCommand(macro);
            remote.UndoButton();

            Console.WriteLine("\n--- ТЕСТ: ШАБЛОН ---");
            new ExcelReport().GenerateReport();

            Console.WriteLine("\n--- ТЕСТ: ПОСРЕДНИК ---");
            var chat = new ChatMediator();
            var u1 = new ChatUser(chat, "Алексей");
            var u2 = new ChatUser(chat, "Мария");
            chat.AddToChannel("Work", u1);
            chat.AddToChannel("Work", u2);
            u1.Send("Привет всем!", "Work");
        }
    }
}