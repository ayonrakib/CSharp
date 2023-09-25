using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using AngleSharp.Dom;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using AngleSharp.Html.Dom;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections;

namespace Selenium.Learning.SortWebTables
{
    public class SortWebTablesSteps
    {
        IWebDriver driver;
        ReadOnlyCollection<string>? tabs;
        WebDriverWait wait;
        readonly SortWebTablesPOM sortWebTablesPOM = new SortWebTablesPOM();
        [SetUp]
        public void Setup()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/seleniumPractise/#/offers");
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("page-menu")));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        [Test]
        public void ValidateURL()
        {
            Assert.That(driver.Url, Is.EqualTo("https://rahulshettyacademy.com/seleniumPractise/#/offers"));
        }

        [Test]
        public void SortTable()
        {
            SelectElement dropdown = sortWebTablesPOM.getDropdown(driver);
            dropdown.SelectByText("20");
            sortWebTablesPOM.ClickSortVeggies(driver);
        }
        [Test]
        public void ValidateMaximumItemsPerPage()
        {
            SelectElement dropdown = sortWebTablesPOM.getDropdown(driver);
            sortWebTablesPOM.selectMaximumItemsPerPage(dropdown);
            Assert.That(sortWebTablesPOM.getItemsPerPage(dropdown), Is.EqualTo("20"));
        }
        [Test]
        public void ValidateVeggies()
        {
            var veggies = sortWebTablesPOM.GetVeggies(driver);
            var originalVeggies = new ArrayList() { "Wheat", "Tomato", "Strawberry", "Rice", "Potato" };
            Assert.That(veggies, Is.EqualTo(originalVeggies));
        }
        [Test]
        public void ValidateSortedVeggies()
        {
            SelectElement dropdown = sortWebTablesPOM.getDropdown(driver);
            sortWebTablesPOM.selectMaximumItemsPerPage(dropdown);
            var veggies = sortWebTablesPOM.GetVeggies(driver);
            veggies.Sort();
            sortWebTablesPOM.ClickSortVeggies(driver);
            var sortedVeggies = sortWebTablesPOM.GetVeggies(driver);
            Assert.That(veggies, Is.EqualTo(sortedVeggies));
        }
        [TearDown]
        public void CloseBrowser()
        {
            tabs = driver.WindowHandles;
            if (tabs.Count == 1)
            {
                driver.Close();
            }
            else
            {
                driver.Quit();
            }
        }
    }
}
