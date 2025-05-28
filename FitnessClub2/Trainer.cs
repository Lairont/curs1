using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    class Trainer
    {
        public int Id;
        public string FIO;
        public string Spec;

        public static Trainer AddTrainer()
        {
            Console.Write("ID: "); int id = int.Parse(Console.ReadLine());
            Console.Write("ФИО: "); string fio = Console.ReadLine();
            Console.Write("Специализация: "); string spec = Console.ReadLine();
            return new Trainer { Id = id, FIO = fio, Spec = spec };
        }

        public void Print() => Console.WriteLine($"[{Id}] {FIO}, Спец: {Spec}");

        public static void Delete(List<Trainer> list)
        {
            Console.Write("ID для удаления: ");
            int id = int.Parse(Console.ReadLine());
            list.RemoveAll(t => t.Id == id);
        }

        public static List<Trainer> LoadFromFile(string basePath)
        {
            string path = Path.Combine(basePath, "Trainer.txt");
            return File.Exists(path)
                ? File.ReadAllLines(path).Select(line =>
                {
                    var p = line.Split(';');
                    return new Trainer { Id = int.Parse(p[0]), FIO = p[1], Spec = p[2] };
                }).ToList()
                : new List<Trainer>();
        }

        public static void SaveToFile(List<Trainer> list, string basePath)
        {
            string path = Path.Combine(basePath, "Trainer.txt");
            File.WriteAllLines(path, list.Select(t => $"{t.Id};{t.FIO};{t.Spec}"));
        }
    }
}
