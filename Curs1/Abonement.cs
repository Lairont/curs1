using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Curs1
{
    class Abonement
    {
        public int Id;
        public string Type;
        public decimal Cost;
        public int DurationDays;

        public static Abonement AddAbonement()
        {
            Console.Write("ID: "); int id = int.Parse(Console.ReadLine());
            Console.Write("Тип: "); string type = Console.ReadLine();
            Console.Write("Цена: "); decimal cost = decimal.Parse(Console.ReadLine());
            Console.Write("Срок (дней): "); int days = int.Parse(Console.ReadLine());
            return new Abonement { Id = id, Type = type, Cost = cost, DurationDays = days };
        }

        public void Print() => Console.WriteLine($"[{Id}] {Type} — {Cost} руб. ({DurationDays} дней)");

        public static List<Abonement> LoadFromFile() =>
            File.Exists("abonements.txt")
                ? File.ReadAllLines("abonements.txt").Select(line =>
                {
                    var p = line.Split(';');
                    return new Abonement { Id = int.Parse(p[0]), Type = p[1], Cost = decimal.Parse(p[2]), DurationDays = int.Parse(p[3]) };
                }).ToList()
                : new List<Abonement>();

        public static void SaveToFile(List<Abonement> list) =>
            File.WriteAllLines("abonements.txt", list.Select(a => $"{a.Id};{a.Type};{a.Cost};{a.DurationDays}"));
    }
}