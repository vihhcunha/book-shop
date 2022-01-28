using Bogus;
using Book_Shop.AutomatedTests.Config;
using Book_Shop.AutomatedTests.Extensions;

namespace Book_Shop.AutomatedTests.Register
{
    public class RegisterPageHelper
    {
        public string EmailSpanXPath => "/html/body/div/main/form/div/div[1]/div/span/span";
        public string PasswordSpanXPath => "/html/body/div/main/form/div/div[2]/div/span";
        public string ConfirmPasswordSpanXPath => "/html/body/div/main/form/div/div[3]/div/span/span";
        public string PageTitleH3 => "/html/body/div/main/form/h3";
        public string ValidEmail { get; }
        public string ValidPassword { get; }

        private readonly SeleniumHelper _helper;
        public RegisterPageHelper(SeleniumHelper seleniumHelper)
        {
            _helper = seleniumHelper;
            ValidPassword = new Faker().Internet.PasswordCustom();
            ValidEmail = new Faker().Internet.Email();
        }

        public void AccessRegisterPage()
        {
            _helper.GoToUrl(_helper.Configuration.RegisterUrl);
        }

        public void FillEmailField(string value)
        {
            _helper.FillTextBoxById("Input_Email", value);
        }

        public void FillPasswordField(string value)
        {
            _helper.FillTextBoxById("Input_Password", value);
        }

        public void FillConfirmPasswordField(string value)
        {
            _helper.FillTextBoxById("Input_ConfirmPassword", value);
        }

        public void SubmitRegisterForm()
        {
            _helper.ClickOnButtonById("registerSubmit");
        }

        public string GetEmailSpanText()
        {
            return _helper.GetElementByXPath(EmailSpanXPath).Text;
        }

        public string GetPasswordSpanText()
        {
            return _helper.GetElementByXPath(PasswordSpanXPath).Text;
        }

        public string GetConfirmPasswordSpanText()
        {
            return _helper.GetElementByXPath(ConfirmPasswordSpanXPath).Text;
        }
    }
}
