//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using ScanDemo.Models;
//using ScanDemo.Services;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ScanDemo.Services.Tests
//{
//    [TestClass()]
//    public class MockDataStoreTests
//    {
//        readonly Mock<IDataStore<Item>> DataStore = new Mock<IDataStore<Item>>();
//        [TestMethod]
//        public void TestAddItemAsync()
//        {
//            // Arrange
//            DataStore mock = new DataStore();
//            // TODO : ajouter les nouvelles variables à Item.cs et modifier l'objet ci-dessous
//            // On crée un nouvel item
//            Item item = new Item { Id = 999, Titre = "First item", Pourcentage = 4, Code = "4444", Adresse = "nimes", DateDeFin = "20-01-2000" };

//            // Act avec id = "999"
//            var result = mock.AddItemAsync(item).Result;

//            // Assert
//            Assert.IsFalse(result);
//        }

//        [TestMethod]
//        public void TestUpdateItemAsync()
//        {
//            // Arrange
//            DataStore mock = new DataStore();
//            // TODO : ajouter les nouvelles variables à Item.cs et modifier l'objet ci-dessous
//            // On crée un nouvel item avec le meme id
//            Item item = new Item { Id = "999", Text = "Other item", Description = "This is an other item description." };

//            // Act avec id = "999"
//            var result = mock.UpdateItemAsync(item).Result;

//            // Assert
//            Assert.Fail(result);
//        }

//        [TestMethod]
//        public void TestGetItemAsync()
//        {
//            // Arrange
//            DataStore mock = new DataStore();

//            // On crée un nouvel item
//            Item item = new Item { Id = "777", Text = "An other item", Description = "This is again an other item description." };
//            var addItem = mock.AddItemAsync(item).Result;

//            // Assert
//            if (addItem != true)
//            {
//                Assert.Fail();
//            }

//            var getResult = mock.GetItemAsync("777").Result;

//            // Assert
//            if (getResult.Id != "777")
//            {
//                Assert.Fail();
//            }

//        }

//        [TestMethod]
//        public void TestGetAllItems()
//        {
//            // Arrange
//            DataStore mock = new DataStore();

//            // Act
//            var result = mock.GetAllItems();

//            // Assert
//            if (result.Count == 0)
//            {
//                Assert.Fail();
//            }
//        }

//        [TestMethod]
//        public void TestDeleteItemAsync()
//        {
//            // Arrange
//            DataStore mock = new DataStore();

//            // Act avec id = "999"
//            var result = mock.DeleteItemAsync(999).Result;

//            // Assert
//            if (result)
//            {
//                Assert.Fail();
//            }
//        }

//        // Tests d'intégration
//        [TestMethod]
//        public void TestIntegrationTest()
//        {
//            // Arrange
//            DataStore mock = new DataStore();

//            // METHOD 1 : ADD() 
//            // TODO : ajouter les nouvelles variables à Item.cs et modifier l'objet ci-dessous
//            // On crée un nouvel item
//            Item addItem = new Item { Id = "999", Text = "First item", Description = "This is an item description." };

//            // Act avec id = "999"
//            var addResult = mock.AddItemAsync(addItem).Result;

//            // Assert
//            if (addResult != true)
//            {
//                Assert.Fail();
//            }

//            // METHOD 2 : UPDATE()
//            // TODO : ajouter les nouvelles variables à Item.cs et modifier l'objet ci-dessous
//            // On crée un nouvel item avec le meme id
//            Item updItem = new Item { Id = "999", Text = "Other item", Description = "This is an other item description." };
//            // Act avec id = "999"
//            var updResult = mock.UpdateItemAsync(updItem).Result;

//            // Assert
//            if (updResult != true)
//            {
//                Assert.Fail();
//            }

//            // METHOD 3 : GET()
//            Act avec id = "999" que l'on à modifier juste avant
//            var getResult = mock.GetItemAsync("999").Result;

//            // Assert
//            if (getResult.Id != "999")
//            {
//                Assert.Fail();
//            }

//            // METHOD 4 : GETALL()
//            // Act
//            var getAllResult = mock.GetAllItems();

//            // Assert
//            if (getAllResult.Result.Count == 0)
//            {
//                Assert.Fail();
//            }

//            // METHOD 5 : DELETE()
//            // Act avec id = "999"
//            var delResult = mock.DeleteItemAsync("999").Result;

//            // Assert
//            if (delResult != true)
//            {
//                Assert.Fail();
//            }


//        }
//    }
//}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScanDemo.Models;
using ScanDemo.Services;
using System.Collections.Generic;

namespace ScanDemoTests
{
    [TestClass]
    public class ScanDemoTests
    {
        readonly List<Item> items;

        [TestMethod]
        public void TestAddItemAsync()
        {
            // Arrange
            DataStore mock = new DataStore();

            // On crée un nouvel item
            Item item = new Item { Id = 999, Titre = "first item", Pourcentage = 4, Code = "4444", Adresse = "Paris", DateDeFin = "20-04-2021" };

            // Act avec id = "999"
            var result = mock.AddItemAsync(item).Result;

            // Assert
            if (result != true)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestUpdateItemAsync()
        {
            // Arrange
            DataStore mock = new DataStore();

            // On crée un nouvel item avec le meme id
            Item item = new Item { Id = 999, Titre = "other item", Pourcentage = 5, Code = "5556", Adresse = "Montpelllier", DateDeFin = "20-05-2021" };

            // Act avec id = "999"
            var result = mock.UpdateItemAsync(item).Result;

            // Assert
            if (result != true)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestGetItemAsync()
        {
            // Arrange
            DataStore mock = new DataStore();

            // On crée un nouvel item
            Item item = new Item { Id = 999, Titre = "an other item", Pourcentage = 6, Code = "6666", Adresse = "Marseille", DateDeFin = "20-06-2021" };

            // Act
            var additem = mock.AddItemAsync(item).Result;

            // assert
            if (additem != true)
            {
                Assert.Fail();
            }

            // Act
            var result = mock.GetItemByCodeAsync("6666").Result;

            // Assert
            if (result.Code != "6666")
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void TestGetAllItems()
        {
            // Arrange
            DataStore mock = new DataStore();

            // Act
            var result = mock.GetAllItems().Result;

            // Assert
            if (result.Count == 0)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestDeleteTtemAsync()
        {
            // Arrange
            DataStore mock = new DataStore();

            // Act avec id = "999"
            var result = mock.DeleteItemAsync(999).Result;

            // Assert
            if (result)
            {
                Assert.Fail();
            }
        }

        // Tests d'intégration
        [TestMethod]
        public void TestIntegration()
        {
            // Arrange
            DataStore mock = new DataStore();

            // Method 1 : ADD() 
            // On crée un nouvel item
            Item addItem = new Item { Id = 777, Titre = "again an other item", Pourcentage = 7, Code = "7777", Adresse = "Toulouse", DateDeFin = "20-07-2021" };

            // Act avec id = "999"
            var addResult = mock.AddItemAsync(addItem).Result;

            // Assert
            if (addResult != true)
            {
                Assert.Fail();
            }

            // Method 2 : UPDATE()
            // On crée un nouvel item avec le meme id
            Item updItem = new Item { Id = 777, Titre = "blblblbl an other item", Pourcentage = 8, Code = "8888", Adresse = "Bordeaux", DateDeFin = "20-08-2021" };

            // Act avec id = "999"
            var updResult = mock.UpdateItemAsync(updItem).Result;

            // Assert
            if (updResult != true)
            {
                Assert.Fail();
            }

            // Method 3 : GET()
            // Act avec id = "999" que l'on à modifier juste avant
            var getResult = mock.GetItemByCodeAsync("8888").Result;

            // Assert
            if (getResult.Code != "8888")
            {
                Assert.Fail();
            }

            // Method 4 : GETALL()
            // Act
            var getAllResult = mock.GetAllItems();

            // Assert
            if (getAllResult.Result.Count == 0)
            {
                Assert.Fail();
            }

            // Method 5 : DELETE()
            // Act avec id = "999"
            var delResult = mock.DeleteItemAsync(999).Result;

            // Assert
            if (delResult != true)
            {
                Assert.Fail();
            }
        }
    }
}