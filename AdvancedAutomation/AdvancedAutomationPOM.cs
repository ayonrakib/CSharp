using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Learning.AdvancedAutomation
{
    public class AdvancedAutomationPOM
    {
        public IWebElement GetIcon(IWebDriver driver)
        {
            IWebElement icon = driver.FindElement(By.ClassName("logoClass"));
            return icon;
        }
        public IWebElement GetNameInputBox(IWebDriver driver)
        {
            IWebElement nameInputBox = driver.FindElement(By.Id("name"));
            return nameInputBox;
        }
        public void SendKeys(IWebElement inputBox, string inputText)
        {
            inputBox.SendKeys(inputText);
        }
        public IWebElement GetAlertButton(IWebDriver driver)
        {
            IWebElement alertButton = driver.FindElement(By.XPath("//input[@onclick='displayAlert()']"));
            return alertButton;
        }
        public IWebElement GetAutoCompleteInputBox(IWebDriver driver)
        {
            IWebElement autoCompleteInputBox = driver.FindElement(By.Id("autocomplete"));
            return autoCompleteInputBox;
        }
        public IList<IWebElement> GetSuggestions(IWebDriver driver, string locator)
        {
            IList<IWebElement> suggestions = driver.FindElements(By.ClassName(locator));
            return suggestions;
        }
    }
}
