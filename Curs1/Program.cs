// Program.cs — версия в стиле "Hotel/Client/Employee" для темы "Фитнес-клуб"
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Curs1;

namespace FitnessClub
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Client> clients = new List<Client>();
            List<Trainer> trainers = new List<Trainer>();
            List<Abonement> abonements = new List<Abonement>();
            List<Workout> workouts = new List<Workout>();

            bool flag = true;
            while (flag)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("ПЕРВОЕ МЕНЮ — Загрузка и добавление данных");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("[1] Загрузить данные из файлов");
                Console.WriteLine("[2] Добавить клиента");
                Console.WriteLine("[3] Добавить тренера");
                Console.WriteLine("[4] Добавить абонемент");
                Console.WriteLine("[5] Добавить тренировку");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[6] Выйти из первого меню");
                Console.WriteLine("[7] Выйти из программы и сохранить все данные");
                Console.ResetColor();

                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1': LoadAll(clients, trainers, abonements, workouts); break;
                    case '2': clients.Add(Client.AddClient()); break;
                    case '3': trainers.Add(Trainer.AddTrainer()); break;
                    case '4': abonements.Add(Abonement.AddAbonement()); break;
                    case '5': workouts.Add(Workout.AddWorkout(clients, trainers)); break;
                    case '6': flag = false; break;
                    case '7': SaveAll(clients, trainers, abonements, workouts); return;
                }
            }

            bool mainMenu = true;
            while (mainMenu)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("ГЛАВНОЕ МЕНЮ — Работа с системой");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("[1] Клиенты");
                Console.WriteLine("[2] Тренеры");
                Console.WriteLine("[3] Тренировки");
                Console.WriteLine("[4] Абонементы");
                Console.WriteLine("[5] Выйти из программы");
                Console.ResetColor();

                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1': ClientMenu(clients, abonements); break;
                    case '2': TrainerMenu(trainers, workouts); break;
                    case '3': WorkoutMenu(workouts, clients); break;
                    case '4': AbonementMenu(abonements); break;
                    case '5': SaveAll(clients, trainers, abonements, workouts); mainMenu = false; break;
                }
            }
        }

        static void LoadAll(List<Client> clients, List<Trainer> trainers, List<Abonement> abonements, List<Workout> workouts)
        {
            clients.AddRange(Client.LoadFromFile());
            trainers.AddRange(Trainer.LoadFromFile());
            abonements.AddRange(Abonement.LoadFromFile());
            workouts.AddRange(Workout.LoadFromFile());
            Console.WriteLine("Данные загружены."); Console.ReadKey();
        }

        static void SaveAll(List<Client> clients, List<Trainer> trainers, List<Abonement> abonements, List<Workout> workouts)
        {
            Client.SaveToFile(clients);
            Trainer.SaveToFile(trainers);
            Abonement.SaveToFile(abonements);
            Workout.SaveToFile(workouts);
            Console.WriteLine("Данные сохранены."); Console.ReadKey();
        }

        static void ClientMenu(List<Client> clients, List<Abonement> abonements)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Клиенты:");
                Console.WriteLine("[1] Список всех клиентов");
                Console.WriteLine("[2] Активные клиенты");
                Console.WriteLine("[3] Доход за месяц");
                Console.WriteLine("[4] Назад");
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1': foreach (var c in clients) c.Print(); break;
                    case '2': Client.ShowActive(clients, abonements); break;
                    case '3': Client.ShowIncome(clients, abonements); break;
                    case '4': return;
                }
                Console.ReadKey();
            }
        }

        static void TrainerMenu(List<Trainer> trainers, List<Workout> workouts)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Тренеры:");
                Console.WriteLine("[1] Список тренеров");
                Console.WriteLine("[2] Групповые тренировки");
                Console.WriteLine("[3] Удалить тренера");
                Console.WriteLine("[4] Назад");
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1': foreach (var t in trainers) t.Print(); break;
                    case '2': Workout.ShowGroupTrainers(workouts, trainers); break;
                    case '3': Trainer.Delete(trainers); break;
                    case '4': return;
                }
                Console.ReadKey();
            }
        }

        static void WorkoutMenu(List<Workout> workouts, List<Client> clients)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Тренировки:");
                Console.WriteLine("[1] Все тренировки");
                Console.WriteLine("[2] Средняя посещаемость в день");
                Console.WriteLine("[3] Популярные тренировки");
                Console.WriteLine("[4] Персональные клиенты");
                Console.WriteLine("[5] Назад");
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1': foreach (var w in workouts) w.Print(); break;
                    case '2': Workout.ShowAttendance(workouts); break;
                    case '3': Workout.ShowPopular(workouts); break;
                    case '4': Workout.ShowPersonalClients(workouts, clients); break;
                    case '5': return;
                }
                Console.ReadKey();
            }
        }

        static void AbonementMenu(List<Abonement> abonements)
        {
            Console.Clear();
            foreach (var ab in abonements) ab.Print();
            Console.ReadKey();
        }
    }
}

