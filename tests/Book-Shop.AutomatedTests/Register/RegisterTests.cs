using Book_Shop.AutomatedTests.Config;
using System;
using Xunit;

namespace Book_Shop.AutomatedTests.Register
{
    [Collection(nameof(AutomationWebFixtureCollection))]
    public class RegisterTests
    {
        private readonly AutomationWebTestsFixture _testsFixture;
        private readonly RegisterPageHelper _pageHelper;

        public RegisterTests(AutomationWebTestsFixture fixture)
        {
            _testsFixture = fixture;
            _pageHelper = new RegisterPageHelper(fixture.BrowserHelper);
        }

        [Fact(DisplayName = "User can access")]
        [Trait("Category", "RegistrationPage")]
        public void RegistrationPage_AccessRegistrationPage_UserCanAccess()
        {
            _pageHelper.AccessRegisterPage();

            Assert.Contains("Create a new account", _testsFixture.BrowserHelper.GetElementByXPath(_pageHelper.PageTitleH3).Text, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "Submit empty values - show errors")]
        [Trait("Category", "RegistrationPage")]
        public void RegistrationPage_SubmitEmptyValues_ShowErrorMessages()
        {
            _pageHelper.AccessRegisterPage();
            _pageHelper.FillEmailField("");
            _pageHelper.FillPasswordField("");
            _pageHelper.FillConfirmPasswordField("");
            _pageHelper.SubmitRegisterForm();

            Assert.Contains("required", _pageHelper.GetEmailSpanText(), StringComparison.InvariantCultureIgnoreCase);
            Assert.Contains("required", _pageHelper.GetPasswordSpanText(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "Submit invalid password - show errors")]
        [Trait("Category", "RegistrationPage")]
        public void RegistrationPage_SubmitInvalidPassword_ShowErrorMessages()
        {
            _pageHelper.AccessRegisterPage();
            _pageHelper.FillEmailField(_pageHelper.ValidEmail);
            _pageHelper.FillPasswordField("aaa");
            _pageHelper.FillConfirmPasswordField("");
            _pageHelper.SubmitRegisterForm();

            Assert.Contains("characters", _pageHelper.GetPasswordSpanText(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "Submit invalid email - show errors")]
        [Trait("Category", "RegistrationPage")]
        public void RegistrationPage_SubmitInvalidEmail_ShowErrorMessages()
        {
            _pageHelper.AccessRegisterPage();
            _pageHelper.FillEmailField("aaa");
            _pageHelper.FillPasswordField("");
            _pageHelper.FillConfirmPasswordField("");
            _pageHelper.SubmitRegisterForm();

            Assert.Contains("valid", _pageHelper.GetEmailSpanText(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "Submit different passwords - show errors")]
        [Trait("Category", "RegistrationPage")]
        public void RegistrationPage_SubmitDifferentPasswords_ShowErrorMessages()
        {
            _pageHelper.AccessRegisterPage();
            _pageHelper.FillEmailField(_pageHelper.ValidEmail);
            _pageHelper.FillPasswordField("abcdefg");
            _pageHelper.FillConfirmPasswordField("gfedcba");
            _pageHelper.SubmitRegisterForm();

            Assert.Contains("match", _pageHelper.GetConfirmPasswordSpanText(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "Submit form")]
        [Trait("Category", "RegistrationPage")]
        public void RegistrationPage_SubmitForm_ShouldBeOk()
        {
            _pageHelper.AccessRegisterPage();
            _pageHelper.FillEmailField(_pageHelper.ValidEmail);
            _pageHelper.FillPasswordField(_pageHelper.ValidPassword);
            _pageHelper.FillConfirmPasswordField(_pageHelper.ValidPassword);
            _pageHelper.SubmitRegisterForm();

            Assert.Contains("Welcome", _testsFixture.BrowserHelper.GetElementByXPathWaitingForTitle("/html/body/div/main/div/h1", "Home Page").Text, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
