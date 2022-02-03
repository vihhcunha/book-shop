using Book_Shop.AutomatedTests.Config;

namespace Book_Shop.AutomatedTests.Login
{
    internal class LoginPageHelper
    {
        public string PageTitleXPath => "/html/body/div/main/div/h1";
        public string SpanEmailXPath => "/html/body/div/main/section/form/div[1]/div[1]/div/span/span";
        public string SpanPasswordXPath => "/html/body/div/main/section/form/div[1]/div[2]/div/span/span";
        public string EmailTest => "test@test.com";
        public string PasswordTest => "Test12#";

        public SeleniumHelper _helper { get; set; }
        public LoginPageHelper(SeleniumHelper helper)
        {
            _helper = helper;
        }

        public void AccessLoginPage()
        {
            _helper.GoToUrl(_helper.Configuration.LoginUrl);
        }

        public string GetPageTitle()
        {
            return _helper.GetElementByXPath(PageTitleXPath).Text;
        }

        public void FillEmailField(string value)
        {
            _helper.FillTextBoxById("Input_Email", value);
        }

        public void FillPasswordField(string value)
        {
            _helper.FillTextBoxById("Input_Password", value);
        }

        public void PressSubmitButton()
        {
            _helper.ClickOnButtonById("login-submit");
        }

        public string GetSpanEmailValue()
        {
            return _helper.GetElementByXPath(SpanEmailXPath).Text;
        }

        public string GetSpanPasswordValue()
        {
            return _helper.GetElementByXPath(SpanPasswordXPath).Text;
        }

        public void Login()
        {
            AccessLoginPage();
            FillEmailField(EmailTest);
            FillPasswordField(PasswordTest);
            PressSubmitButton();
        }
    }
}
