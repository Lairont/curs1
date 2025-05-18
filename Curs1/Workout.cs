using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Curs1
{
    class Workout
    {
        public int Id;
        public string Type;
        public DateTime Date;
        public int TrainerId;
        public List<int> Clients;

        public static Workout AddWorkout(List<Client> clients, List<Trainer> trainers)
        {
            Console.Write("ID: "); int id = int.Parse(Console.ReadLine());
            Console.Write("Тип (групповая/персональная): "); string type = Console.ReadLine();
            Console.Write("Дата: "); DateTime date = DateTime.Parse(Console.ReadLine());
            Console.Write("ID тренера: "); int tid = int.Parse(Console.ReadLine());
            Console.Write("Сколько клиентов: "); int count = int.Parse(Console.ReadLine());
            List<int> ids = new List<int>();
            for (int i = 0; i < count; i++)
            {
                Console.Write($"ID клиента {i + 1}: ");
                ids.Add(int.Parse(Console.ReadLine()));
            }
            return new Workout { Id = id, Type = type, Date = date, TrainerId = tid, Clients = ids };
        }

        public void Print() =>
            Console.WriteLine($"[{Id}] {Type}, {Date.ToShortDateString()}, Тренер: {TrainerId}, Клиентов: {Clients.Count}");

        public static void ShowAttendance(List<Workout> list)
        {
            Console.Write("Дата (дд.мм.гггг): ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            int count = list.Where(w => w.Date.Date == date.Date).Sum(w => w.Clients.Count);
            Console.WriteLine($"Всего посетителей: {count}");
        }

        public static void ShowPopular(List<Workout> list)
        {
            var top = list.OrderByDescending(w => w.Clients.Count).Take(3);
            foreach (var w in top) w.Print();
        }

        public static void ShowPersonalClients(List<Workout> list, List<Client> clients)
        {
            var ids = list.Where(w => w.Type.ToLower().Contains("персон"))
                .SelectMany(w => w.Clients).Distinct();
            foreach (var c in clients.Where(c => ids.Contains(c.Id))) c.Print();
        }

        public static void ShowGroupTrainers(List<Workout> list, List<Trainer> trainers)
        {
            var ids = list.Where(w => w.Type.ToLower().Contains("групп"))
                .Select(w => w.TrainerId).Distinct();
            foreach (var t in trainers.Where(t => ids.Contains(t.Id))) t.Print();
        }

        public static List<Workout> LoadFromFile() =>
            File.Exists("workouts.txt")
                ? File.ReadAllLines("workouts.txt").Select(line =>
                {
                    var p = line.Split(';');
                    return new Workout
                    {
                        Id = int.Parse(p[0]),
                        Type = p[1],
                        Date = DateTime.Parse(p[2]),
                        TrainerId = int.Parse(p[3]),
                        Clients = p[4].Split(',').Select(int.Parse).ToList()
                    };
                }).ToList()
                : new List<Workout>();

        public static void SaveToFile(List<Workout> list) =>
            File.WriteAllLines("workouts.txt", list.Select(w => $"{w.Id};{w.Type};{w.Date:yyyy-MM-dd};{w.TrainerId};{string.Join(",", w.Clients)}"));
    }
}
