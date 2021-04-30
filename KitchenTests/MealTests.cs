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
        public void ShekleinMealTest()
        {
            //Arrange
            var recipes = Program.Recipes;
            var timeToEat = TimeForEat.Party;
            var productCount = new Dictionary<ProductEnum, int>();
            foreach (var product in Enum.GetValues<ProductEnum>()) {
                productCount.Add(product, product==ProductEnum.Banana?50:0);
            };
            var dishes = new Dictionary<Dish, bool>(); 
            dishes.Add(Dish.Box, true);
            
            var people = 5;

            //Act
            var foundedRecipes = Program.ReturnReliableRecipes(recipes, timeToEat, dishes);
            var finalRecipes = Program.CalculatePortions(foundedRecipes, productCount, people);

            //Assert
            if (finalRecipes.Any())
                Assert.AreEqual(Program.Recipes[2], finalRecipes[0]);
            else
                Assert.Fail();
        }
    }
}