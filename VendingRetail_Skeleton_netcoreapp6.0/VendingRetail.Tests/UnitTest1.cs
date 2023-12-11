using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class Tests
    {

        [Test]
        public void FillWaterTank_WhenTankIsNotFull_ShouldReturnSuccessMessage()
        {
            // Arrange
            CoffeeMat coffeeMat = new CoffeeMat(1000, 3);

            // Act
            string result = coffeeMat.FillWaterTank();

            // Assert
            Assert.AreEqual("Water tank is filled with 1000ml", result);
        }

        [Test]
        public void FillWaterTank_WhenTankIsFull_ShouldReturnAlreadyFullMessage()
        {
            // Arrange
            CoffeeMat coffeeMat = new CoffeeMat(1000, 3);
            coffeeMat.FillWaterTank(); // Fill the tank

            // Act
            string result = coffeeMat.FillWaterTank();

            // Assert
            Assert.AreEqual("Water tank is already full!", result);
        }

        [Test]
        public void AddDrink_WhenNotExceedingButtonCount_ShouldReturnTrue()
        {
            // Arrange
            CoffeeMat coffeeMat = new CoffeeMat(1000, 3);

            // Act
            bool result = coffeeMat.AddDrink("Latte", 2.5);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AddDrink_WhenExceedingButtonCount_ShouldReturnFalse()
        {
            // Arrange
            CoffeeMat coffeeMat = new CoffeeMat(1000, 1);
            coffeeMat.AddDrink("Latte", 2.5);

            // Act
            bool result = coffeeMat.AddDrink("Espresso", 1.8);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void BuyDrink_WhenSufficientWaterAndDrinkAvailable_ShouldReturnBillMessage()
        {
            // Arrange
            CoffeeMat coffeeMat = new CoffeeMat(1000, 3);
            coffeeMat.FillWaterTank(); // Ensure there is enough water
            coffeeMat.AddDrink("Latte", 2.5);

            // Act
            string result = coffeeMat.BuyDrink("Latte");

            // Assert
            Assert.IsTrue(result.Contains("Your bill is"));
        }

        [Test]
        public void BuyDrink_WhenInsufficientWater_ShouldReturnOutOfWaterMessage()
        {
            // Arrange
            CoffeeMat coffeeMat = new CoffeeMat(50, 3); // Set a low water capacity
            coffeeMat.FillWaterTank(); // Ensure the tank is not full
            coffeeMat.AddDrink("Latte", 2.5);

            // Act
            string result = coffeeMat.BuyDrink("Latte");

            // Assert
            Assert.AreEqual("CoffeeMat is out of water!", result);
        }

        [Test]
        public void BuyDrink_WhenDrinkNotAvailable_ShouldReturnNotAvailableMessage()
        {
            // Arrange
            CoffeeMat coffeeMat = new CoffeeMat(1000, 3);
            coffeeMat.FillWaterTank(); // Ensure there is enough water
            coffeeMat.AddDrink("Latte", 2.5);

            // Act
            string result = coffeeMat.BuyDrink("Espresso");

            // Assert
            Assert.AreEqual("Espresso is not available!", result);
        }

        [Test]
        public void CollectIncome_ShouldReturnCorrectIncomeAndReset()
        {
            // Arrange
            CoffeeMat coffeeMat = new CoffeeMat(1000, 3);
            coffeeMat.FillWaterTank(); // Ensure there is enough water
            coffeeMat.AddDrink("Latte", 2.5);
            coffeeMat.BuyDrink("Latte"); // Make a purchase

            // Act
            double collectedIncome = coffeeMat.CollectIncome();

            // Assert
            Assert.AreEqual(2.5, collectedIncome);
            Assert.AreEqual(0, coffeeMat.Income); // Ensure income is reset to 0
        }
    }
}