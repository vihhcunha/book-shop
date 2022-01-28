using Book_Shop.AutomatedTests.Config;
using System;
using Xunit;

namespace Book_Shop.AutomatedTests.Login
{
    [Collection(nameof(AutomationWebFixtureCollection))]
    public class LoginTests
    {
        private readonly AutomationWebTestsFixture _testsFixture;
        private readonly LoginPageHelper _pageHelper;

        public LoginTests(AutomationWebTestsFixture fixture)
        {
            _testsFixture = fixture;
            _pageHelper = new LoginPageHelper(fixture.BrowserHelper);
        }

        [Fact(DisplayName = "User can access")]
        [Trait("Category", "LoginPage")]
        public void LoginPage_AccessLoginPage_UserCanAccess()
        {
            _pageHelper.AccessLoginPage();

            Assert.Contains("Log in", _pageHelper.GetPageTitle(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "Submit empty values - show errors")]
        [Trait("Category", "LoginPage")]
        public void LoginPage_SubmitEmptyValues_ShowErrorMessages()
        {
            _pageHelper.AccessLoginPage();
            _pageHelper.FillEmailField("");
            _pageHelper.FillPasswordField("");
            _pageHelper.PressSubmitButton();

            Assert.Contains("required", _pageHelper.GetSpanEmailValue(), StringComparison.InvariantCultureIgnoreCase);
            Assert.Contains("required", _pageHelper.GetSpanPasswordValue(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "Login into shop")]
        [Trait("Category", "LoginPage")]
        public void LoginPage_LoginIntoShop_MustWorks()
        {
            _pageHelper.Login();

            Assert.Contains("Welcome", _testsFixture.BrowserHelper.GetElementByXPathWaitingForTitle("/html/body/div/main/div/h1", "Home Page").Text, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
