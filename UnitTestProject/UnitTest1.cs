using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScanDemo.Models;
using ScanDemo.Repositories;
using ScanDemo.Services;
using ScanDemo.ViewModels;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void MockAddItemAsync()
        {
            // Arrange
            MockDataStore mock = new MockDataStore();
            Item item = new Item { Id = "999", Text = "First item", Description = "This is an item description." };

            // Act
            var result = mock.AddItemAsync(item).Result;

            // Assert
            if (result != true)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void MockUpdateItemAsync()
        {
            // Arrange
            MockDataStore mock = new MockDataStore();
            Item item = new Item { Id = "999", Text = "Other item", Description = "This is an other item description." };

            // Act
            var result = mock.UpdateItemAsync(item).Result;

            // Assert
            if (result != true)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void MockDeleteItemAsync()
        {
            // Arrange
            MockDataStore mock = new MockDataStore();

            // Act avec id = "1"
            var result = mock.DeleteItemAsync("999").Result;

            // Assert
            if (result != true)
            {
                Assert.Fail();
            }
        }
        
        [TestMethod]
        public void MockGetItemAsync()
        {
            // Arrange
            MockDataStore mock = new MockDataStore();
            Item item = new Item { Id = "666", Text = "First item", Description = "This is an item description." };
            mock.AddItemAsync(item);

            // Act avec id = "1"
            var result = mock.GetItemAsync("666").Result;

            // Assert
            if (result.Id != "666")
            {
                Assert.Fail();
            }
        }
    }
}
