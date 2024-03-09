using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace волки
{
    internal class Program
    {
        class Animal
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Score { get; set; }
            public bool IsMale { get; set; }
        }

        class Progra
        {
            static Random random = new Random();

            static void Main()
            {
                int size = 20;
                List<Animal> rabbits = new List<Animal>();
                List<Animal> wolves = new List<Animal>();
                List<Animal> sheWolves = new List<Animal>();

                // Инициализация начальной популяции
                Initia(rabbits, wolves, sheWolves);

                // Моделирование изменения популяции в течение 100 шагов
                for (int step = 0; step < 100; step++)
                {
                    MoveRabbits(rabbits, size);
                    ReproduceRabbits(rabbits);

                    MoveWolves(wolves, rabbits, size);
                    MoveWolves(sheWolves, rabbits, size);

                    HuntAndReproduce(wolves, rabbits, sheWolves);
                    HuntAndReproduce(sheWolves, rabbits, wolves);

                    DisplayPopulation(step, rabbits, wolves, sheWolves);
                    Console.WriteLine();
                }
            }

            static void Initia(List<Animal> rabbits, List<Animal> wolves, List<Animal> sheWolves)
            {
                // Инициализация начальной популяции
                for (int i = 0; i < 10; i++)
                {
                    rabbits.Add(new Animal { X = random.Next(20), Y = random.Next(20) });
                    wolves.Add(new Animal { X = random.Next(20), Y = random.Next(20), Score = 1 });
                    sheWolves.Add(new Animal { X = random.Next(20), Y = random.Next(20), Score = 1 });
                }
            }

            static void MoveRabbits(List<Animal> rabbits, int size)
            {
                // Движение кроликов
                foreach (var rabbit in rabbits)
                {
                    int newX = rabbit.X + random.Next(-1, 2);
                    int newY = rabbit.Y + random.Next(-1, 2);
                    if (newX >= 0 && newX < size && newY >= 0 && newY < size)
                    {
                        rabbit.X = newX;
                        rabbit.Y = newY;
                    }
                }
            }

            static void ReproduceRabbits(List<Animal> rabbits)
            {
                // Размножение кроликов
                int initialCount = rabbits.Count;
                for (int i = 0; i < initialCount; i++)
                {
                    if (random.NextDouble() < 0.2)
                    {
                        rabbits.Add(new Animal { X = rabbits[i].X, Y = rabbits[i].Y });
                    }
                }
            }

            static void MoveWolves(List<Animal> wolves, List<Animal> rabbits, int size)
            {
                // Движение волков и волчиц
                foreach (var wolf in wolves)
                {
                    int newX = wolf.X + random.Next(-1, 2);
                    int newY = wolf.Y + random.Next(-1, 2);
                    if (newX >= 0 && newX < size && newY >= 0 && newY < size)
                    {
                        wolf.X = newX;
                        wolf.Y = newY;
                    }
                }
            }

            static void HuntAndReproduce(List<Animal> predators, List<Animal> preys, List<Animal> partners)
            {
                // Охота и размножение
                foreach (var predator in predators)
                {
                    foreach (var prey in preys)
                    {
                        if (predator.X == prey.X && predator.Y == prey.Y)
                        {
                            predator.Score++;
                            preys.Remove(prey);
                            break;
                        }
                    }

                    foreach (var partner in partners)
                    {
                        if (predator.X == partner.X && predator.Y == partner.Y && preys.Count == 0)
                        {
                            predators.Add(new Animal { X = predator.X, Y = predator.Y, IsMale = random.NextDouble() < 0.5 });
                            break;
                        }
                    }
                }
            }

            static void DisplayPopulation(int step, List<Animal> rabbits, List<Animal> wolves, List<Animal> sheWolves)
            {
                // Вывод информации о популяции
                Console.WriteLine($"Шаг {step}: Кролики - {rabbits.Count}, Волки - {wolves.Count}, Волчицы - {sheWolves.Count}");
            }
        }
    }
}
