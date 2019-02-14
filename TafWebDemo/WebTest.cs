using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TAF.Core;
using TAF.WebSupport;
using TAF.WebSupport.SeleniumAdapter;
using By = TAF.WebSupport.By;

namespace TafWebDemo
{
    [TestClass]
    public class WebTest : TestSet
    {
        private WebInteraction _web;

        [TestInitialize]
        public void Init()
        {
            _web = new WebInteraction(CurrentTestCase.TestCaseResult.LogPosts,
                new SeleniumTafDriver(CurrentTestCase.TestCaseResult.LogPosts, new ChromeDriver()));

            CurrentTestCase.AddReport(new TestCaseExecutionPlainTextReport());
            CurrentTestCase.AddReport(new TestCaseHtmlReport());
        }

        [TestCleanup]
        public void TearDown()
        {
            _web.MakeSureDriverIsClosed();
        }

        [TestMethod]
        public void ClaremontSe()
        {
            var searchIcon = new DomElement(By
                .TagName("button")
                .AndByClass("icon"));

            var inputBox = new DomElement(By
                .TagName("input")
                .AndByAttributeValue("type", "text"));

            var inputBoxXPath = new DomElement(By
                .XPath("//input[@type='text']"));

            var testFestLink = new DomElement(By
                .TagName("a")
                .AndByAttributeValue("href", "/event/event-testfest-1218/"));

            _web.Navigate("http://www.claremont.se");
            _web.Verify().CurrentUrl("https://www.claremont.se/");
            _web.Click(searchIcon);
            _web.Write(inputBoxXPath, "Test");
            _web.Type(inputBox, Keys.Enter);
            _web.Verify(testFestLink).Text("TestFest");
        }
    }
}
