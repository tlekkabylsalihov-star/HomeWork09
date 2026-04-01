using System;

namespace PracticalWork
{
    public class RoomBookingSystem {
        public void BookRoom() => Console.WriteLine("Номер забронирован.");
        public void CheckAvailability() => Console.WriteLine("Проверка доступности...");
        public void CancelBooking() => Console.WriteLine("Бронь номера отменена.");
    }

    public class RestaurantSystem {
        public void ReserveTable() => Console.WriteLine("Стол в ресторане забронирован.");
        public void OrderFood() => Console.WriteLine("Еда заказана.");
        public void CallTaxi() => Console.WriteLine("Такси вызвано.");
    }

    public class EventManagementSystem {
        public void BookConferenceHall() => Console.WriteLine("Конференц-зал забронирован.");
        public void OrderEquipment() => Console.WriteLine("Оборудование заказано.");
    }

    public class CleaningService {
        public void ScheduleCleaning() => Console.WriteLine("Уборка внесена в расписание.");
        public void PerformCleaning() => Console.WriteLine("Выполняется уборка.");
    }

    public class HotelFacade {
        private RoomBookingSystem _rooms = new RoomBookingSystem();
        private RestaurantSystem _restaurant = new RestaurantSystem();
        private EventManagementSystem _events = new EventManagementSystem();
        private CleaningService _cleaning = new CleaningService();

        public void BookFullService() {
            _rooms.BookRoom();
            _restaurant.OrderFood();
            _cleaning.ScheduleCleaning();
        }

        public void OrganizeEvent() {
            _events.BookConferenceHall();
            _events.OrderEquipment();
            _rooms.BookRoom();
        }

        public void ReserveTableWithTaxi() {
            _restaurant.ReserveTable();
            _restaurant.CallTaxi();
        }

        public void CancelAndClean() {
            _rooms.CancelBooking();
            _cleaning.PerformCleaning();
        }
    }
}