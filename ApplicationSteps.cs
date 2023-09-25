using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Selenium.Learning

    // jekono interaction complete hoise kina check korte hobe
    // POM e element interaction thakbe, steps file e test steps thakbe
    // element interaction steps e rakha jabe na
    // POM will be no test pure POM functions
    // all dependencies wilkl be injected by test class
{
    public class ApplicationSteps
    {
        IWebDriver driver;
        ReadOnlyCollection<string>? tabs;
        WebDriverWait wait;
        readonly ApplicationPOM applicationPOM = new ApplicationPOM();
        [SetUp]
        public void SetUp()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("signInBtn")));
        }
        [Test]
        public void ValidateLogin()
        {
            applicationPOM.waitUntilLoginFormFound(wait);
            applicationPOM.sendValidUsername(driver, wait);
            applicationPOM.sendValidPassword(driver);
            applicationPOM.clickSignInButton(driver);
            IWebElement header = applicationPOM.waitUntilHeaderIsVisible(wait);
            Assert.That(header.GetAttribute("innerHTML"), Is.EqualTo("Shop Name"));
        }
        [Test]
        public void ValidateURL()
        {
            Assert.That(driver.Url, Is.EqualTo("https://rahulshettyacademy.com/loginpagePractise/"));
        }
        [Test]
        public void ValidateTitle()
        {
            driver.Navigate().GoToUrl("https://www.google.com");
            String title = driver.Title;
            TestContext.Progress.WriteLine("The title is: " + title);
            Assert.That(title, Is.EqualTo("Google"));
        }
        [Test]
        public void ValidateTabNumber()
        {
            tabs = driver.WindowHandles;
            Assert.That(tabs, Has.Count.EqualTo(1));
        }
        [Test]
        public void OpenNewTabAndCloseSecondTab()
        {
            driver.SwitchTo().NewWindow(WindowType.Tab);
            driver.Navigate().GoToUrl("http://www.youtube.com");
            tabs = driver.WindowHandles;
            Assert.That(tabs, Has.Count.EqualTo(2));
        }
        [Test]
        public void OpenFireFox()
        {
            driver = new FirefoxDriver();
            TestContext.Progress.WriteLine("Current window: " + driver.WindowHandles.Count);
            driver.Navigate().GoToUrl("https://www.google.com");
        }
        [Test]
        public void OpenEdge()
        {
            driver = new EdgeDriver();
            TestContext.Progress.WriteLine("Current window: " + driver.WindowHandles.Count);
            driver.Navigate().GoToUrl("https://www.google.com");
        }
        [Test]
        public void SendInvalidUsernameAndWait()
        {
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");
            applicationPOM.sendInvalidUsername(driver);
            Thread.Sleep(3000);
        }
        [Test]
        public void SendInvalidPasswordAndWait()
        {
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");
            applicationPOM.sendInvalidPassword(driver);
            Thread.Sleep(3000);
        }
        [Test]
        public void PrintUserTypeSelector()
        {
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");
            applicationPOM.printUserSelectionRadioElement(driver);
            Thread.Sleep(3000);
        }
        [Test]
        public void ClickUserTypeSelector()
        {
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");
            applicationPOM.clickUserSelectionRadioElement(driver);
            Thread.Sleep(3000);
        }
        [Test]
        public void ValidateEmptyLoginErrorMessage()
        {
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");
            applicationPOM.waitUntilLoginFormFound(wait);
            applicationPOM.clickSignInButton(driver);
            var errorMessage = applicationPOM.waitUntilErrorMessageIsDisplayedAndGetIt(wait);
            String message = errorMessage.GetAttribute("innerHTML");
            //TestContext.Progress.WriteLine("Error message: " + message);
            Assert.That(message, Is.EqualTo("<strong>Empty</strong> username/password."));
        }
        [Test]
        public void ValidateIncorrectLoginErrorMessage()
        {
            applicationPOM.waitUntilLoginFormFound(wait);
            applicationPOM.sendInvalidUsername(driver);
            applicationPOM.sendInvalidPassword(driver);
            applicationPOM.clickSignInButton(driver);
            var errorMessage = applicationPOM.waitUntilErrorMessageIsDisplayedAndGetIt(wait);
            String message = errorMessage.GetAttribute("innerHTML");
            //TestContext.Progress.WriteLine("Error message: " + message);
            Assert.That(message, Is.EqualTo("<strong>Incorrect</strong> username/password."));
        }
        [Test]
        public void PrintUserName()
        {
            var username = applicationPOM.getUserName(driver);
            TestContext.Progress.WriteLine("username: " + username);
            Thread.Sleep(3000);
        }
        [Test]
        public void ValidateBlinkingText()
        {

            String blinkingText = applicationPOM.getBlinkingText(driver).Text;
            TestContext.Progress.WriteLine("blinkingTextElement: " + blinkingText);
            Assert.That(blinkingText, Is.EqualTo("Free Access to InterviewQues/ResumeAssistance/Material"));
        }
        [Test]
        public void ClickOkButtonOfUserSelectionModal()
        {
            //driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");
            applicationPOM.clickUserSelectionRadioElement(driver);
            //Thread.Sleep(1000);

        }
        [Test]
        public void ClickTerms()
        {
            //driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");
            applicationPOM.clickTerms(driver);
            Assert.That(driver.Url, Is.EqualTo("https://rahulshettyacademy.com/loginpagePractise/"));
        }
        [Test]
        public void ValidateIcon()
        {
            //driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");
            IWebElement icon = applicationPOM.getIcon(driver);
            //Thread.Sleep(1500);
            Assert.IsTrue(icon.Displayed);
        }
        [Test]
        public void SelectDropdownOptionByValue()
        {
            //driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");
            IWebElement dropdown = applicationPOM.getDropdown(driver);
            SelectElement s = new SelectElement(dropdown);
            s.SelectByValue("teach");
            //Thread.Sleep(1500);
        }
        [Test]
        public void SelectDropdownOptionByText()
        {
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");
            IWebElement dropdown = applicationPOM.getDropdown(driver);
            SelectElement s = new(dropdown);
            s.SelectByText("Consultant");
            Thread.Sleep(1500);
        }
        [Test]
        public void SelectDropdownOptionByIndex()
        {
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");
            IWebElement dropdown = applicationPOM.getDropdown(driver);
            SelectElement s = new(dropdown);
            s.SelectByIndex(2);
            Thread.Sleep(1500);
        }
        [Test]
        public void ValidateModalBody()
        {
            applicationPOM.clickUserSelectionRadioElement(driver);
            IWebElement okayButton = applicationPOM.waitUntilButtonOfModalIsVisibleAndGetIt(wait, "okayBtn");
            TestContext.Progress.WriteLine("button text: " + okayButton.GetAttribute("innerHTML"));
        }
        [Test]
        public void ClickOkOfUserSelectionRadioButton()
        {
            applicationPOM.clickUserSelectionRadioElement(driver);
            IWebElement okayButton = applicationPOM.waitUntilButtonOfModalIsVisibleAndGetIt(wait, "okayBtn");
            Thread.Sleep(3000);
            //why am i waiting here? why isnt the wait working?
            okayButton.Click();
            //Thread.Sleep(3000);
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("okayBtn")));
            TestContext.Progress.WriteLine("is modal visible: " + okayButton.Displayed);
            Assert.That(okayButton.Displayed, Is.False);
        }
        [Test]
        public void ClickCancelOfUserSelectionRadioButton()
        {
            applicationPOM.clickUserSelectionRadioElement(driver);
            IWebElement cancelButton = applicationPOM.waitUntilButtonOfModalIsVisibleAndGetIt(wait, "cancelBtn");
            Thread.Sleep(3000);
            //why am i waiting here? why isnt the wait working?
            cancelButton.Click();
            //Thread.Sleep(3000);
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("cancelBtn")));
            TestContext.Progress.WriteLine("is modal visible: " + cancelButton.Displayed);
            Assert.That(cancelButton.Displayed, Is.False);
        }
        [Test]
        public void ValidateProductsLength()
        {
            if (applicationPOM.login(driver, wait))
            {
                ReadOnlyCollection<IWebElement> products = applicationPOM.getProducts(driver);
                Assert.That(products, Has.Count.EqualTo(4));
            }
        }
        [Test]
        public void ValidateProductsNames()
        {
            if (applicationPOM.login(driver, wait))
            {
                ReadOnlyCollection<IWebElement> products = applicationPOM.getProducts(driver);
                List<string> productNames = applicationPOM.getProductNames();
                for(int index = 0; index < products.Count; index++)
                {
                    string productName = products[index].FindElement(By.CssSelector($"body > app-root > app-shop > div > div > div.col-lg-9 > app-card-list > app-card:nth-child({index+1}) > div > div.card-body > h4 > a")).GetAttribute("innerHTML");
                    productNames.Remove(productName);
                    
                }
                Assert.That(productNames, Is.Empty);
                // assert diye array compare korte hobe
            }
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
            //tabs.count == 1 ? driver.close() : driver.quit();
        }
    }
}
