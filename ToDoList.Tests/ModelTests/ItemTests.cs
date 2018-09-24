using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System.Collections.Generic;
using System;

namespace ToDoList.Tests
{

  [TestClass]
  public class ItemTests : IDisposable
  {
    public void Dispose()
    {
      Item.DeleteAll();
    }
    public ItemTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
    }
    [TestMethod]
    public void GetAll_DbStartsEmpty_0()
    {
      //Arrange
      //Act
      int result = Item.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
    {
      // Arrange, Act
      Item firstItem = new Item("Mow the lawn", "2/2/2012", 1);
      Item secondItem = new Item("Mow the lawn", "2/2/2012", 1);

      // Assert
      Assert.AreEqual(firstItem, secondItem);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ItemList()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn", "2/2/2012", 1);

      //Act
      testItem.Save();
      List<Item> result = Item.GetAll();
      List<Item> testList = new List<Item>{testItem};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_FindsItemInDatabase_Item()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn", "2/2/2012", 1);
      testItem.Save();

      //Act
      Item foundItem = Item.Find(testItem.id);

      //Assert
      Assert.AreEqual(testItem, foundItem);
    }

    [TestMethod]
    public void AddCategory_AddsCategoryToItem_CategoryList()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn", "mow the lawn", 0);
      testItem.Save();

      Category testCategory = new Category("Home stuff");
      testCategory.Save();

      //Act
      testItem.AddCategory(testCategory);

      List<Category> result = testItem.GetCategories();
      List<Category> testList = new List<Category>{testCategory};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetCategories_ReturnsAllItemCategories_CategoryList()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn", "", 0);
      testItem.Save();

      Category testCategory1 = new Category("Home stuff");
      testCategory1.Save();

      Category testCategory2 = new Category("Work stuff");
      testCategory2.Save();

      //Act
      testItem.AddCategory(testCategory1);
      List<Category> result = testItem.GetCategories();
      List<Category> testList = new List<Category> {testCategory1};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
  }
}
