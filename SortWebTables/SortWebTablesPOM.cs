using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Learning.SortWebTables
{
    public class SortWebTablesPOM
    {
        public void goToWebPage(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/seleniumPractise/#/offers");
        }
        public IWebElement getPageSizeDropdown(IWebDriver driver)
        {
            IWebElement pageSizeDropdown = driver.FindElement(By.Id("page-menu"));
            return pageSizeDropdown;
        }
        public SelectElement getDropdown(IWebDriver driver)
        {
            SelectElement dropdown = new SelectElement(getPageSizeDropdown(driver));
            return dropdown;
        }
        public void selectMaximumItemsPerPage(SelectElement dropdown)
        {
            dropdown.SelectByText("20");
        }
        public string getItemsPerPage(SelectElement dropdown)
        {
            IWebElement selectedOption = dropdown.SelectedOption;
            return selectedOption.Text;
        }
        public void validateMaximumItemsPerPage(IWebDriver driver, SelectElement dropdown)
        {
            Assert.That(getItemsPerPage(dropdown), Is.EqualTo("20"));
        }
        public ReadOnlyCollection<IWebElement> getVeggiesWebElementList(IWebDriver driver)
        {
            ReadOnlyCollection<IWebElement> veggiesWebElementList = driver.FindElements(By.XPath("//tr/td[1]"));
            return veggiesWebElementList;
        }
        public ArrayList GetVeggies(IWebDriver driver)
        {
            var veggies = new ArrayList();
            ReadOnlyCollection<IWebElement> veggiesWebElementList = getVeggiesWebElementList(driver);
            foreach (IWebElement veggie in veggiesWebElementList)
            {
                veggies.Add(veggie.GetAttribute("innerHTML"));
            }
            return veggies;
        }
        public void ClickSortVeggies(IWebDriver driver)
        {
            driver.FindElement(By.CssSelector(".sort-icon.sort-descending")).Click();
        }
    }
}
