using System;
using System.Text;

namespace TechRepairService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool running = true;

            while (running)
            {
                Console.WriteLine("=== Сервіс ремонту техніки ===");
                Console.WriteLine("1 — довідка про програму та сутність");
                Console.WriteLine("2 — розрахунок вартості ремонту (табулювання)");
                Console.WriteLine("3 — таблиця виконаних ремонтів (майстри / дні)");
                Console.WriteLine("0 — вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAbout();
                        Pause();
                        break;

                    case "2":
                        ShowRepairCostTable();
                        Pause();
                        break;

                    case "3":
                        ShowRepairsMatrixTable();
                        Pause();
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("Роботу завершено. До побачення!");
                        break;

                    default:
                        Console.WriteLine("Помилка: такого пункту немає в меню. Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу для повернення в меню...");
            Console.ReadKey(true);
            Console.Clear();
        }

        static void ShowAbout()
        {
            Console.WriteLine("=== Довідка про застосунок ===");
            Console.WriteLine("Тема №26: Сервіс ремонту техніки");
            Console.WriteLine("Сутності: Користувач, Заявка (головна), Статус (довідник).");
            Console.WriteLine("\nПоля головної сутності «Заявка» (8 полів):");
            Console.WriteLine("  1. Номер заявки (int)");
            Console.WriteLine("  2. Клієнт (string)");
            Console.WriteLine("  3. Пристрій (string)");
            Console.WriteLine("  4. Несправність (string)");
            Console.WriteLine("  5. Дата прийому (DateTime)");
            Console.WriteLine("  6. Дата видачі (DateTime)");
            Console.WriteLine("  7. Вартість (double)");
            Console.WriteLine("  8. Код статусу (int, посилання на довідник)");
            Console.WriteLine("\nПідсумкова величина: середній термін ремонту за типами пристроїв.");
        }

        static void ShowRepairCostTable()
        {
            Console.WriteLine("=== Таблиця вартості ремонту (табулювання) ===");

            double from = 0.5;
            double to = 5.0;
            double step = 0.5;

            Console.WriteLine($"{"Час (год)",12} | {"Вартість (грн)",16}");
            Console.WriteLine(new string('-', 31));

            for (double hours = from; hours <= to + 1e-9; hours += step)
            {
                double cost = 350.0 + 420.0 * hours - 15.0 * Math.Pow(hours, 1.2);
                Console.WriteLine($"{hours,12:F2} | {cost,16:F2}");
            }
        }

        static void ShowRepairsMatrixTable()
        {
            Console.WriteLine("=== Звіт: кількість відремонтованих пристроїв ===");

            Console.Write("Введіть кількість майстрів (рядків, 1..15): ");
            if (!int.TryParse(Console.ReadLine(), out int rows) || rows <= 0 || rows > 15)
            {
                Console.WriteLine("Помилка: введіть ціле додатне число від 1 до 15.");
                return;
            }

            Console.Write("Введіть кількість робочих днів (стовпців, 1..10): ");
            if (!int.TryParse(Console.ReadLine(), out int cols) || cols <= 0 || cols > 10)
            {
                Console.WriteLine("Помилка: введіть ціле додатне число від 1 до 10.");
                return;
            }

            Console.WriteLine();

            Console.Write($"{"Майстер \\ День",-16}");
            for (int col = 1; col <= cols; col++)
            {
                Console.Write($"{"День " + col,9}");
            }
            Console.WriteLine($"{"Разом",10}");

            int totalWidth = 16 + cols * 9 + 10;
            Console.WriteLine(new string('-', totalWidth));

            int grandTotal = 0;

            for (int row = 1; row <= rows; row++)
            {
                Console.Write($"{"Майстер №" + row,-16}");
                int rowSum = 0;

                for (int col = 1; col <= cols; col++)
                {
                    int repairs = (row * 2 + col) % 5 + 1;
                    rowSum += repairs;
                    Console.Write($"{repairs,9}");
                }

                grandTotal += rowSum;
                Console.WriteLine($"{rowSum,10}");
            }

            Console.WriteLine(new string('-', totalWidth));

            Console.Write($"{"Всього за день",-16}");
            for (int col = 1; col <= cols; col++)
            {
                int colSum = 0;
                for (int row = 1; row <= rows; row++)
                {
                    colSum += (row * 2 + col) % 5 + 1;
                }
                Console.Write($"{colSum,9}");
            }
            Console.WriteLine($"{grandTotal,10}");
        }
    }
}