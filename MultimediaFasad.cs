using System;

namespace PracticalWork
{
    public class TV {
        public void On() => Console.WriteLine("ТВ включен");
        public void Off() => Console.WriteLine("ТВ выключен");
    }

    public class AudioSystem {
        public void On() => Console.WriteLine("Звук включен");
        public void SetVolume(int vol) => Console.WriteLine($"Громкость: {vol}");
    }

    public class GameConsole {
        public void On() => Console.WriteLine("Консоль готова");
        public void StartGame(string game) => Console.WriteLine($"Играем в {game}");
    }

    public class HomeTheaterFacade {
        private TV _tv = new TV();
        private AudioSystem _audio = new AudioSystem();
        private GameConsole _console = new GameConsole();

        public void StartMovie() { _tv.On(); _audio.On(); }
        public void StartGame(string game) { _tv.On(); _console.On(); _console.StartGame(game); }
    }
}