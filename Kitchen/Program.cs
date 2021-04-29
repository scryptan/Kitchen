using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kitchen
{
    class Program
    {
        private static List<Recipe> Recipes = new List<Recipe>()
        {
            new Recipe()
            {
                Name = "Шашлык",
                Time = TimeForEat.Party | TimeForEat.Lunch,
                Dishes = new List<Dish>
                {
                    Dish.Skewer,
                    Dish.Plate
                },
                ProductCount = new Dictionary<ProductEnum, int>
                {
                    {ProductEnum.Meat, 3},
                    {ProductEnum.Salt, 100},
                    {ProductEnum.Pepper, 50},
                    {ProductEnum.Mayo, 2}
                }
            },
            new Recipe()
            {
                Name = "Русский салат",
                Time = TimeForEat.Lunch,
                Dishes = new List<Dish>
                {
                    Dish.Plate,
                    Dish.Spoon
                },
                ProductCount = new Dictionary<ProductEnum, int>
                {
                    {ProductEnum.Cucumber, 3},
                    {ProductEnum.Tomato, 3},
                    {ProductEnum.Salt, 100},
                    {ProductEnum.Mayo, 1},
                }
            },
            new Recipe()
            {
                Name = "Cалат имени А.А.Шеклейна",
                Time = TimeForEat.Party,
                Dishes = new List<Dish>
                {
                    Dish.Box,
                },
                ProductCount = new Dictionary<ProductEnum, int>
                {
                    {ProductEnum.Banana, 50},
                }
            },
            new Recipe()
            {
                Name = "Лёгонкой завтрак",
                Time = TimeForEat.Breakfast,
                Dishes = new List<Dish>
                {
                    Dish.Plate,
                },
                ProductCount = new Dictionary<ProductEnum, int>
                {
                    {ProductEnum.Apple, 1},
                    {ProductEnum.Banana, 1},
                }
            },
            new Recipe()
            {
                Name = "Яищница",
                Time = TimeForEat.Breakfast,
                Dishes = new List<Dish>
                {
                    Dish.Plate,
                    Dish.Pan
                },
                ProductCount = new Dictionary<ProductEnum, int>
                {
                    {ProductEnum.Egg, 3},
                    {ProductEnum.Salt, 100},
                    {ProductEnum.Mayo, 1},
                }
            },
            new Recipe()
            {
                Name = "Набор бурбербродов",
                Time = TimeForEat.Lunch,
                Dishes = new List<Dish>
                {
                    Dish.Plate,
                },
                ProductCount = new Dictionary<ProductEnum, int>
                {
                    {ProductEnum.Bread, 4},
                    {ProductEnum.Salt, 100},
                    {ProductEnum.Mayo, 1},
                    {ProductEnum.Cucumber, 2},
                    {ProductEnum.Tomato, 1}
                }
            },
            new Recipe()
            {
                Name = "Шарлотка",
                Time = TimeForEat.Dinner,
                Dishes = new List<Dish>
                {
                    Dish.Plate,
                    Dish.Pot
                },
                ProductCount = new Dictionary<ProductEnum, int>
                {
                    {ProductEnum.Apple, 4},
                    {ProductEnum.Sugar, 100},
                    {ProductEnum.Salt, 20},
                    {ProductEnum.Egg, 4},
                    {ProductEnum.Flour, 300},
                }
            },

            new Recipe()
            {
                Name = "Чебурек",
                Time = TimeForEat.Dinner,
                Dishes = new List<Dish>
                {
                    Dish.Plate,
                    Dish.Pan
                },
                ProductCount = new Dictionary<ProductEnum, int>
                {
                    {ProductEnum.Salt, 100},
                    {ProductEnum.Mayo, 1},
                    {ProductEnum.Meat, 2},
                    {ProductEnum.Egg, 4},
                    {ProductEnum.Flour, 300},
                }
            },
        };

        static void Main()
        {
            var productCount = new Dictionary<ProductEnum, int>();

            foreach (var product in Enum.GetValues<ProductEnum>())
            {
                Console.WriteLine($"Сколько у вас {product} в целых штуках");
                if (!int.TryParse(Console.ReadLine(), out var count) || count < 0)
                    count = 0;

                productCount.Add(product, count);
            }

            var dishes = new Dictionary<Dish, bool>();
            foreach (var dish in Enum.GetValues<Dish>())
            {
                Console.WriteLine($"У вас есть {dish}? (y/n) при неверном вводе считаем что нет");
                if (Console.ReadLine()?.ToLower() == "y")
                    dishes.Add(dish, true);
            }

            var timeDict = Enum.GetValues<TimeForEat>().ToDictionary(x => (int) x);
            Console.WriteLine("На какое время хотите рецепт?");
            foreach (var (number, time) in timeDict)
                Console.WriteLine($"{number}) {time}");

            if (!Enum.TryParse<TimeForEat>(Console.ReadLine(), true, out var timeForEat))
                timeForEat = TimeForEat.Breakfast;

            Console.WriteLine(
                "Введите количество персон для еды (в целых штуках, при неправильнтом вводе будет выбран 1)");
            if (!int.TryParse(Console.ReadLine(), out var peopleToEat))
                peopleToEat = 1;

            var recipes = Recipes
                .Where(x => x.Time == timeForEat)
                .Where(x => x.Dishes.All(c => dishes.ContainsKey(c) && dishes[c]))
                .ToList();
            var countSorted = recipes
                .Where(c => c.ProductCount.All(x =>
                    productCount.ContainsKey(x.Key) && productCount[x.Key] * peopleToEat >= x.Value))
                .ToList();
            
            if (countSorted.Any())
            {
                Console.WriteLine("По вашим данным подходят эти рецепты:");

                foreach (var recipe in countSorted)
                    Console.WriteLine(recipe);
            }
            else
            {
                Console.WriteLine("Нет подходящих рецептов :(");
                foreach (var recipe in recipes)
                {
                    Console.WriteLine($"Для рецепта {recipe.Name} вам понадобится:");
                    foreach (var product in recipe.ProductCount)
                    {
                        Console.WriteLine($"{product.Key}: {product.Value} у.е");
                    }
                }
            }
        }
    }

    class Recipe
    {
        public string Name { get; set; }
        public Dictionary<ProductEnum, int> ProductCount { get; set; } = new();
        public List<Dish> Dishes { get; set; } = new();
        public TimeForEat Time { get; set; }

        public override string ToString()
        {
            var dishesStr = string.Join(", ", Dishes);
            var productStr = new StringBuilder();
            foreach (var i in ProductCount)
                productStr.AppendLine($"    {i.Key}: {i.Value}");

            return $"{Name}\nВремя еды:\n   {Time}\nПосуда:\n   {dishesStr}\nИнгридиенты:\n{productStr}";
        }
    }

    enum Dish
    {
        Pot,
        Pan,
        Blade,
        Spoon,
        Skewer,
        Plate,
        Box
    }

    [Flags]
    enum TimeForEat
    {
        Breakfast,
        Lunch,
        Dinner,
        Party
    }

    enum ProductEnum
    {
        Meat,
        Apple,
        Banana,
        Salt,
        Pepper,
        Sugar,
        Egg,
        Salad,
        Cucumber,
        Tomato,
        Mayo,
        Bread,
        Flour
    }
}