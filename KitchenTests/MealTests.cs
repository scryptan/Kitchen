using NUnit.Framework;
using System.Collections.Generic;
using Kitchen;
using System;
using System.Linq;

namespace KitchenTests
{
    public class MealTests
    {
        [Test]
        public void SingleRecipe3PersonsTest()
        {
            //Declare
            var recipes = Program.Recipes;
            var timeToEat = TimeForEat.Party;
            var productCount = ValuesDictionaryFiller(new Dictionary<ProductEnum, int>(),0);
            var dishes = new Dictionary<Dish, bool>();

            //Valuing
            dishes.Add(Dish.Box, true);
            productCount[ProductEnum.Banana] += 200;

            var people = 3;

            //Act
            var foundedRecipes = Program.ReturnReliableRecipes(recipes, timeToEat, dishes);
            var finalRecipes = Program.CalculatePortions(foundedRecipes, productCount, people);

            //Assert
            Assert.AreEqual(Program.Recipes[2], finalRecipes.FirstOrDefault());
        }

        [Test]
        public void CoupleRecipesReturningTest()
        {
            //Declare
            var recipes = Program.Recipes;
            var timeToEat = TimeForEat.Lunch;
            var productCount = ValuesDictionaryFiller( new Dictionary<ProductEnum, int>(),0);
            var dishes = new Dictionary<Dish, bool>();
            var people = 2;
            var testResult = new List<Recipe>();

            //Valuing
            dishes.Add(Dish.Plate, true);
            dishes.Add(Dish.Spoon, true);
            productCount[ProductEnum.Cucumber] += 10;
            productCount[ProductEnum.Tomato] += 8;
            productCount[ProductEnum.Salt] += 400;
            productCount[ProductEnum.Mayo] += 4;
            productCount[ProductEnum.Bread] += 8;

            testResult.Add(Program.Recipes[1]);
            testResult.Add(Program.Recipes[5]);
            //Act
            var foundedRecipes = Program.ReturnReliableRecipes(recipes, timeToEat, dishes);
            var finalRecipes = Program.CalculatePortions(foundedRecipes, productCount, people);

            //Assert
            Assert.AreEqual(testResult, finalRecipes);
        }

        [Test]
        public void NoIngredientsTest()
        {
            //Declare
            var recipes = Program.Recipes;
            var timeToEat = TimeForEat.Lunch;
            var productCount = ValuesDictionaryFiller(new Dictionary<ProductEnum, int>(),0);
            var dishes = new Dictionary<Dish, bool>();
            var people = 2;
            var testResult = new List<Recipe>();

            //Valuing
            dishes.Add(Dish.Plate, true);
            dishes.Add(Dish.Spoon, true);

            //Act
            var foundedRecipes = Program.ReturnReliableRecipes(recipes, timeToEat, dishes);
            var finalRecipes = Program.CalculatePortions(foundedRecipes, productCount, people);

            //Assert
            Assert.AreEqual(testResult, finalRecipes);
        }

        [Test]
        public void NoReliableDishesTest()
        {
            //Declare
            var recipes = Program.Recipes;
            var timeToEat = TimeForEat.Lunch;
            var productCount = ValuesDictionaryFiller(new Dictionary<ProductEnum, int>(),0);
            var dishes = new Dictionary<Dish, bool>();
            var people = 2;
            var testResult = new List<Recipe>();

            //Valuing
            dishes.Add(Dish.Pan, true);
            dishes.Add(Dish.Pot, true);
            productCount[ProductEnum.Cucumber] += 10;
            productCount[ProductEnum.Tomato] += 8;
            productCount[ProductEnum.Salt] += 400;
            productCount[ProductEnum.Mayo] += 4;
            productCount[ProductEnum.Bread] += 8;

            //Act
            var foundedRecipes = Program.ReturnReliableRecipes(recipes, timeToEat, dishes);
            var finalRecipes = Program.CalculatePortions(foundedRecipes, productCount, people);

            //Assert
            Assert.AreEqual(testResult, finalRecipes);
        }

        [Test]
        public void AllBreakfastRecipesReturningTest()
        {
            //Declare
            var recipes = Program.Recipes;
            var timeToEat = TimeForEat.Breakfast;
            var productCount = ValuesDictionaryFiller(new Dictionary<ProductEnum, int>(), 1000);
            var dishes = new Dictionary<Dish, bool>();
            var people = 3;
            var testResult = new List<Recipe>();

            //Valuing
            foreach (var dish in Enum.GetValues<Dish>())
                dishes.Add(dish, true);

            foreach (var recipe in recipes)
                if (recipe.Time.Equals(TimeForEat.Breakfast))
                    testResult.Add(recipe);

            //Act
            var foundedRecipes = Program.ReturnReliableRecipes(recipes, timeToEat, dishes);
            var finalRecipes = Program.CalculatePortions(foundedRecipes, productCount, people);

            //Assert
            Assert.AreEqual(testResult, finalRecipes);
        }



        private Dictionary<ProductEnum, int> ValuesDictionaryFiller(Dictionary<ProductEnum, int> dict, int value)
        {
            foreach (var product in Enum.GetValues<ProductEnum>())
            {
                dict.Add(product, value);
            };
            return dict;
        }
    }
}