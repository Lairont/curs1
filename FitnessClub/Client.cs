using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    class Client
    {
        public int Id;
        public string FIO;
        public string Phone;
        public int AbonementId;
        public int Visits;
        public DateTime StartDate;

        public static Client AddClient()
        {
            Console.Write("ID: "); int id = int.Parse(Console.ReadLine());
            Console.Write("ФИО: "); string name = Console.ReadLine();
            Console.Write("Телефон: "); string phone = Console.ReadLine();
            Console.Write("ID абонемента: "); int abId = int.Parse(Console.ReadLine());
            Console.Write("Посещений: "); int visits = int.Parse(Console.ReadLine());
            Console.Write("Дата начала (дд.мм.гггг): "); DateTime date = DateTime.Parse(Console.ReadLine());
            return new Client { Id = id, FIO = name, Phone = phone, AbonementId = abId, Visits = visits, StartDate = date };
        }

        public void Print() =>
            Console.WriteLine($"[{Id}] {FIO}, {Phone}, Абонемент: {AbonementId}, С {StartDate.ToShortDateString()} ({Visits} посещений)");

        public static void ShowActive(List<Client> clients, List<Abonement> abonements)
        {
            Console.WriteLine("Активные клиенты:");
            foreach (var c in clients)
            {
                var ab = abonements.FirstOrDefault(a => a.Id == c.AbonementId);
                if (ab != null && (DateTime.Now - c.StartDate).TotalDays <= ab.DurationDays)
                    c.Print();
            }
        }

        public static void ShowIncome(List<Client> clients, List<Abonement> abonements)
        {
            decimal total = 0;
            foreach (var c in clients)
            {
                var ab = abonements.FirstOrDefault(a => a.Id == c.AbonementId);
                if (ab != null && (DateTime.Now - c.StartDate).Days <= 30)
                    total += ab.Cost;
            }
            Console.WriteLine($"Доход за месяц: {total} руб.");
        }

        public static List<Client> LoadFromFile(string basePath)
        {
            string path = Path.Combine(basePath, "Client.txt");
            return File.Exists(path)
                ? File.ReadAllLines(path).Select(line =>
                {
                    var p = line.Split(';');
                    return new Client
                    {
                        Id = int.Parse(p[0]),
                        FIO = p[1],
                        Phone = p[2],
                        AbonementId = int.Parse(p[3]),
                        Visits = int.Parse(p[4]),
                        StartDate = DateTime.Parse(p[5])
                    };
                }).ToList()
                : new List<Client>();
        }

        public static void SaveToFile(List<Client> list, string basePath)
        {
            string path = Path.Combine(basePath, "Client.txt");
            File.WriteAllLines(path, list.Select(c => $"{c.Id};{c.FIO};{c.Phone};{c.AbonementId};{c.Visits};{c.StartDate:yyyy-MM-dd}"));
        }
    }
}
