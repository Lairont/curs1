using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    class Program
    {
        static string ClientFile = @"D:\\215\\FitnessClub\\txt\\Client.txt";
        static string TrainerFile = @"D:\\215\\FitnessClub\\txt\\Trainer.txt";
        static string AbonementFile = @"D:\\215\\FitnessClub\\txt\\Abonement.txt";
        static string WorkoutFile = @"D:\\215\\FitnessClub\\txt\\Workout.txt";

        static void Main(string[] args)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(ClientFile));

            List<Client> clients = Client.LoadFromFile(Path.GetDirectoryName(ClientFile));
            List<Trainer> trainers = Trainer.LoadFromFile(Path.GetDirectoryName(TrainerFile));
            List<Abonement> abonements = Abonement.LoadFromFile(Path.GetDirectoryName(AbonementFile));
            List<Workout> workouts = Workout.LoadFromFile(Path.GetDirectoryName(WorkoutFile));

            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=============================================================");
                Console.WriteLine("                   СИСТЕМА ФИТНЕС-КЛУБА                      ");
                Console.WriteLine("=============================================================");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[1] Меню клиентов");
                Console.WriteLine("[2] Меню тренеров");
                Console.WriteLine("[3] Меню абонементов");
                Console.WriteLine("[4] Меню тренировок");
                Console.WriteLine("[S] Сохранить данные");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=============================================================");
                Console.ResetColor();

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1: ClientMenu(clients, abonements); break;
                    case ConsoleKey.D2: TrainerMenu(trainers, workouts); break;
                    case ConsoleKey.D3: AbonementMenu(abonements); break;
                    case ConsoleKey.D4: WorkoutMenu(workouts, clients, trainers); break;
                    case ConsoleKey.S:
                        SaveAll(clients, trainers, abonements, workouts);
                        break;
                    case ConsoleKey.Escape:
                        run = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неверная команда. Нажмите любую клавишу...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void SaveAll(List<Client> clients, List<Trainer> trainers, List<Abonement> abonements, List<Workout> workouts)
        {
            Client.SaveToFile(clients, Path.GetDirectoryName(ClientFile));
            Trainer.SaveToFile(trainers, Path.GetDirectoryName(TrainerFile));
            Abonement.SaveToFile(abonements, Path.GetDirectoryName(AbonementFile));
            Workout.SaveToFile(workouts, Path.GetDirectoryName(WorkoutFile));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nДанные успешно сохранены! Нажмите любую клавишу...");
            Console.ResetColor();
            Console.ReadKey();
        }

        static void ClientMenu(List<Client> clients, List<Abonement> abonements)
        {
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=================== МЕНЮ КЛИЕНТОВ ===================");
                Console.ResetColor();
                Console.WriteLine("[1] Добавить клиента");
                Console.WriteLine("[2] Список всех клиентов");
                Console.WriteLine("[3] Активные клиенты");
                Console.WriteLine("[4] Доход за месяц");
                Console.WriteLine("[0] Назад");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("====================================================");
                Console.ResetColor();
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1': clients.Add(Client.AddClient()); break;
                    case '2': foreach (var c in clients) c.Print(); break;
                    case '3': Client.ShowActive(clients, abonements); break;
                    case '4': Client.ShowIncome(clients, abonements); break;
                    case '0': loop = false; continue;
                    default: Console.WriteLine("Неверный ввод"); break;
                }
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        static void TrainerMenu(List<Trainer> trainers, List<Workout> workouts)
        {
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=================== МЕНЮ ТРЕНЕРОВ ===================");
                Console.ResetColor();
                Console.WriteLine("[1] Добавить тренера");
                Console.WriteLine("[2] Список тренеров");
                Console.WriteLine("[3] Удалить тренера");
                Console.WriteLine("[4] Тренеры с групповыми тренировками");
                Console.WriteLine("[0] Назад");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("====================================================");
                Console.ResetColor();
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1': trainers.Add(Trainer.AddTrainer()); break;
                    case '2': foreach (var t in trainers) t.Print(); break;
                    case '3': Trainer.Delete(trainers); break;
                    case '4': Workout.ShowGroupTrainers(workouts, trainers); break;
                    case '0': loop = false; continue;
                    default: Console.WriteLine("Неверный ввод"); break;
                }
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        static void AbonementMenu(List<Abonement> abonements)
        {
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================ МЕНЮ АБОНЕМЕНТОВ =================");
                Console.ResetColor();
                Console.WriteLine("[1] Добавить абонемент");
                Console.WriteLine("[2] Список абонементов");
                Console.WriteLine("[0] Назад");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("====================================================");
                Console.ResetColor();
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1': abonements.Add(Abonement.AddAbonement()); break;
                    case '2': foreach (var ab in abonements) ab.Print(); break;
                    case '0': loop = false; continue;
                    default: Console.WriteLine("Неверный ввод"); break;
                }
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        static void WorkoutMenu(List<Workout> workouts, List<Client> clients, List<Trainer> trainers)
        {
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================== МЕНЮ ТРЕНИРОВОК ==================");
                Console.ResetColor();
                Console.WriteLine("[1] Добавить тренировку");
                Console.WriteLine("[2] Все тренировки");
                Console.WriteLine("[3] Посещаемость по дню");
                Console.WriteLine("[4] Популярные тренировки");
                Console.WriteLine("[5] Персональные клиенты");
                Console.WriteLine("[0] Назад");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("====================================================");
                Console.ResetColor();
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1': workouts.Add(Workout.AddWorkout(clients, trainers)); break;
                    case '2': foreach (var w in workouts) w.Print(); break;
                    case '3': Workout.ShowAttendance(workouts); break;
                    case '4': Workout.ShowPopular(workouts); break;
                    case '5': Workout.ShowPersonalClients(workouts, clients); break;
                    case '0': loop = false; continue;
                    default: Console.WriteLine("Неверный ввод"); break;
                }
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }
    }
}


