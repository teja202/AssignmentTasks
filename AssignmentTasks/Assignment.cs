using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager.DriverConfigs.Impl;

namespace AssignmentTasks
{
    [TestClass]
    public class Assignment
    {
        IWebDriver driver;
        WebDriverWait wait;

        [TestInitialize]
        public void Initialize()
        {
            // Manages brwser driver  like chrome, firefox based on selection
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://au-shopv5.uat.bdirectcloud.net/"); //Update the Url
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30)); // Explicit Wait
        }

        public void Login()
        {
            //Login Step
            IWebElement LoginId = wait.Until(ExpectedConditions.ElementExists(By.Id("UserName")));
            IWebElement Password = wait.Until(ExpectedConditions.ElementExists(By.Id("Password")));
            // Enter Login Id
            LoginId.SendKeys("BID188512");
            // Enter Password
            Password.SendKeys("TejaswiTest3");
            // Click  on Login button
            IWebElement LoginButton = wait.Until(ExpectedConditions.ElementExists(By.Id("submitBtn")));
            LoginButton.Click();
            Thread.Sleep(5000);
        }

        [TestMethod]
        public void SearchProduct()
        {
            Login();
            // Search The Product Locator
            IWebElement searchProductText = wait.Until(ExpectedConditions.ElementExists(By.Id("search-box")));
            searchProductText.SendKeys("CHEESE EDAM (APP 3KG)");
            //Click On Product Search Button
            IWebElement searchProductButton = wait.Until(ExpectedConditions.ElementExists(By.Id("search-button")));
            searchProductButton.Click();

            //Product Seacrch Results Locator
            IWebElement searchProductResult = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@class='productName']//a")));
            // Validting Product Text
            Assert.IsTrue(searchProductResult.Text.Contains("CHEESE EDAM (APP 3KG)"), "CHEESE EDAM (APP 3KG) Product not displayed");
        }

        [TestMethod]
        public void AddProductToBasket()
        {
            Login();
            // Search The Product Locator
            IWebElement searchProductText = wait.Until(ExpectedConditions.ElementExists(By.Id("search-box")));
            searchProductText.SendKeys("CHEESE EDAM (APP 3KG)");
            //Click On Product Search Button
            IWebElement searchProductButton = wait.Until(ExpectedConditions.ElementExists(By.Id("search-button")));
            searchProductButton.Click();

            //Product Seacrch Results Locator
            IWebElement searchProductResult = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@class='productName']//a")));
            // Validting Product Text
            Assert.IsTrue(searchProductResult.Text.Contains("CHEESE EDAM (APP 3KG)"), "CHEESE EDAM (APP 3KG) Product not displayed");


            // AddTo basket Locator
            IWebElement addItemToBasket = wait.Until(ExpectedConditions.ElementExists(By.XPath("(//*[@class='btn btn-primary add-btn'])[last()]")));
            addItemToBasket.Click();

            IWebElement CartButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@class='cart']")));

            //Clicking on Cart Button To View basket Items
            CartButton.Click();

            IWebElement basketItem = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@class='productName ng-binding ng-scope']")));

            //Validate Product Added to basket
            Assert.IsTrue(basketItem.Text.Contains("CHEESE EDAM (APP 3KG)"), "CHEESE EDAM (APP 3KG) Product not displayed");

            IWebElement empltyBasketItems = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[text()='Empty My Basket']")));

            empltyBasketItems.Click();

            IWebElement OkButton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[text()='OK']")));

            OkButton.Click();
        }

    

        [TestCleanup]
        public void CloseBrowser()
        {

            // Close the browser
            driver.Quit();
        }
    }
}