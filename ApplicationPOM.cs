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

namespace Selenium.Learning
{
    public class ApplicationPOM
    {
        //readonly IWebDriver? driver;
        //readonly ReadOnlyCollection<string>? tabs;
        //readonly WebDriverWait? wait;
        //[SetUp]
        //public void SetUp()
        //{
        //    // selenium cant talk directly to chrome
        //    // so we download chromedriver to talk to chrome
        //    // we pass all codes to chromedriver which interats with chrome to direct what we want to do
        //    // need correct version, so always need to update version manually
        //    // that is why drivermanager has been invented to maintain version automatically
        //    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
        //    driver = new ChromeDriver();
        //    // we shouldnt use Thread.sleep when slow page loading
        //    // this makes the test slow cause even if element is found, still waiting
        //    // implicit wait-wait if element not found. if found, continue test
        //    // this way we save time
        //    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        //    // explicit wait
        //    //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        //    //wait.PollingInterval = TimeSpan.FromMilliseconds(1000);
        //    //wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        //    //wait.Until(e => e.FindElement(By.Id("login-form")));
        //    driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");
        //    wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        //    wait.PollingInterval = TimeSpan.FromMilliseconds(200);
        //    //wait.Until(e => e.FindElement(By.Id("login-form")));
        //    wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("signInBtn")));
        //}

        public void sendInvalidUsername(IWebDriver driver)
        {
            driver.FindElement(By.Id("username")).SendKeys("username");
        }
        public void sendInvalidPassword(IWebDriver driver)
        {
            driver.FindElement(By.Name("password")).SendKeys("password");
        }
        public void printUserSelectionRadioElement(IWebDriver driver)
        {
            TestContext.Progress.WriteLine("User type selector is: "+driver.FindElement(By.CssSelector("#login-form > div:nth-child(4) > div > label:nth-child(1)")));
        }
        public void clickUserSelectionRadioElement(IWebDriver driver)
        {
            driver.FindElement(By.CssSelector("#login-form > div:nth-child(4) > div > label:nth-child(2)")).Click();
        }
        public void getIncorrectLoginMessage(IWebDriver driver)
        {
                driver.FindElement(By.CssSelector("#login-form > div.alert.alert-danger.col-md-12"));
        }
        public void clickSignInButton(IWebDriver driver)
        {
                driver.FindElement(By.Id("signInBtn")).Click();
        }
        public object getUserName(IWebDriver driver)
        {
            return driver.FindElement(By.Id("username"));
        }
        public IWebElement getErrorElement(IWebDriver driver)
        {
            //Thread.Sleep(3000);
            IWebElement errorElement = driver.FindElement(By.CssSelector("div.alert.alert-danger.col-md-12"));
            return errorElement;
        }
        public IWebElement getBlinkingText(IWebDriver driver)
        {
            return driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
        }
        public void clickTerms(IWebDriver driver)
        {
            driver.FindElement(By.Id("terms")).Click();
        }
        public IWebElement getIcon(IWebDriver driver)
        {
            return driver.FindElement(By.ClassName("icon-circled"));
        }
        public IWebElement getDropdown(IWebDriver driver)
        {
            return driver.FindElement(By.CssSelector("select.form-control"));
        }
        public IWebElement getRadioButtons(IWebDriver driver)
        {
            return driver.FindElement(By.CssSelector("input[type='radio']"));
        }
        public void waitUntilLoginFormFound(WebDriverWait wait)
        {
            TestContext.Progress.WriteLine("wait value: " + wait);
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login-form")));
        }
        public IWebElement waitUntilErrorMessageIsDisplayedAndGetIt(WebDriverWait wait)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#login-form > div.alert.alert-danger.col-md-12")));
        }
        public IWebElement waitUntilButtonOfModalIsVisibleAndGetIt(WebDriverWait wait, string buttonText)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(By.Id(buttonText)));
        }
        public void sendValidUsername(IWebDriver driver, WebDriverWait wait)
        {
            // wait until rakhbo jeno input ta paay erpor send keys
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            wait.Until((driver) =>
            {
                IWebElement usernameInput = driver.FindElement(By.Id("username"));
                //if(usernameInput.GetAttribute("value") == "rahulshettyacademy") {
                //    return true;
                //}
                //return false;
                return usernameInput.GetAttribute("value") == "rahulshettyacademy" ? true : false;
            });
        }
        public void sendValidPassword(IWebDriver driver)
        {
            driver.FindElement(By.Id("password")).SendKeys("learning");
        }
        public IWebElement waitUntilHeaderIsVisible(WebDriverWait wait)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));
        }
        public bool login(IWebDriver driver, WebDriverWait wait)
        {
            sendValidUsername(driver, wait);
            sendValidPassword(driver);
            clickSignInButton(driver);
            IWebElement header = waitUntilHeaderIsVisible(wait);
            return header.GetAttribute("innerHTML") == "Shop Name" ? true : false;
        }
        public ReadOnlyCollection<IWebElement> getProducts(IWebDriver driver)
        {
            return driver.FindElements(By.TagName("app-card"));
        }
        public List<string> getProductNames() {
            return new List<string>() { "iphone X", "Samsung Note 8","Nokia Edge","Blackberry"};
        }
        public List<string> getProductCategories()
        {
            return new List<string>() { "Category 1", "Category 2", "Category 3" };
        }
        public ReadOnlyCollection<IWebElement> getProductCategoryLinktexts(IWebDriver driver)
        {
            return driver.FindElements(By.CssSelector("body > app-root > app-shop > div > div > div.col-lg-3 > div"));
        }
    }
}
