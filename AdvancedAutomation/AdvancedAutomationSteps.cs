using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using SeleniumExtras.WaitHelpers;

namespace Selenium.Learning.AdvancedAutomation
{
    public class AdvancedAutomationSteps
    {
        IWebDriver driver;
        WebDriverWait wait;
        readonly AdvancedAutomationPOM advancedAutomationPOM = new AdvancedAutomationPOM();
        ReadOnlyCollection<string>? tabs;
        public static applicationBodyId = "carousel-example-generic";
        [SetUp]
        public void Setup()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/AutomationPractice/");

            wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(200));
            wait.PollingInterval = TimeSpan.FromSeconds(1);
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("gf-BIG")));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        [Test]
        public void AssertURL()
        {
            Assert.That(driver.Url, Is.EqualTo("https://rahulshettyacademy.com/AutomationPractice/"));
        }
        [Test]
        public void ValidateIcon()
        {
            IWebElement icon = advancedAutomationPOM.GetIcon(driver);
            Assert.That(icon.GetAttribute("src"), Is.EqualTo("https://rahulshettyacademy.com/AutomationPractice/images/rs_logo.png"));
        }
        [Test]
        public void ValidateName()
        {
            IWebElement nameInputBox = advancedAutomationPOM.GetNameInputBox(driver);
            advancedAutomationPOM.SendKeys(nameInputBox, "Rakib");
            Assert.That(nameInputBox.GetAttribute("value"), Is.EqualTo("Rakib"));
        }
        [Test]
        public void ValidateClickAlertButton()
        {
            IWebElement alertButton = advancedAutomationPOM.GetAlertButton(driver);
            alertButton.Click();
            String alertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();
            Assert.That(alertText, Is.EqualTo("Hello , share this practice page and share your knowledge"));
        }
        [Test]
        public void SendNameAndValidateClickAlertButton()
        {
            IWebElement nameInputBox = advancedAutomationPOM.GetNameInputBox(driver);
            advancedAutomationPOM.SendKeys(nameInputBox, "Rakib");
            IWebElement alertButton = advancedAutomationPOM.GetAlertButton(driver);
            alertButton.Click();
            String alertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();
            Assert.That(alertText, Is.EqualTo("Hello Rakib, share this practice page and share your knowledge"));
        }
        [Test]
        public void ValidateAutoSuggestionSendKeys()
        {
            IWebElement autoCompleteInputBox = advancedAutomationPOM.GetAutoCompleteInputBox(driver);
            advancedAutomationPOM.SendKeys(autoCompleteInputBox, "ind");
            Thread.Sleep(3000);
            Assert.That(autoCompleteInputBox.GetAttribute("value"), Is.EqualTo("ind"));
        }
        [Test]
        public void ValidateAutoSuggestionSelectedInput()
        {
            IWebElement autoSuggestionInputBox = advancedAutomationPOM.GetAutoCompleteInputBox(driver);
            advancedAutomationPOM.SendKeys(autoSuggestionInputBox, "ind");
            Thread.Sleep(5000);
            IList<IWebElement> suggestions = advancedAutomationPOM.GetSuggestions(driver, "ui-menu-item-wrapper");
            foreach(IWebElement suggestion in suggestions)
            {
                TestContext.Progress.WriteLine("suggestions: "+ suggestion.Text);
                if(suggestion.Text == "India")
                {
                    suggestion.Click();
                }
            }
            Assert.That(autoSuggestionInputBox.GetAttribute("value"), Is.EqualTo("India"));
        }
        [Test]
        public void ValidateHoverOptions()
        {
            driver.Navigate().GoToUrl("https://www.rahulshettyacademy.com/");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("carousel-example-generic")));
        }
        [TearDown]
        public void CloseBrowser()
        {
            tabs = driver.WindowHandles;
            if(tabs.Count == 1)
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
