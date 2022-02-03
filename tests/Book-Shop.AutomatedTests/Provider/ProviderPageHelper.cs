using Book_Shop.AutomatedTests.Config;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace Book_Shop.AutomatedTests.Provider
{
    public class ProviderPageHelper
    {
        public string PageTitleXPath => "/html/body/div/main/div[1]/h1";
        public string NewProviderLinkButtonXPath => "/html/body/div/main/a";
        public string NewPageTitleXPath => "/html/body/div/main/div[1]/h1";
        public string SpanNameXPath => "/html/body/div/main/div[2]/div/form/div[1]/span/span";
        public string SpanDocumentXPath => "/html/body/div/main/div[2]/div/form/div[2]/span/span";
        public string SpanZipCodeXPath => "/html/body/div/main/div[2]/div/form/div[6]/span/span";
        public string SpanStreetXPath => "/html/body/div/main/div[2]/div/form/div[7]/span/span";
        public string SpanNumberXPath => "/html/body/div/main/div[2]/div/form/div[8]/span/span";
        public string SpanDistrictXPath => "/html/body/div/main/div[2]/div/form/div[10]/span/span";
        public string SpanCityXPath => "/html/body/div/main/div[2]/div/form/div[11]/span/span";
        public string SpanStateXPath => "/html/body/div/main/div[2]/div/form/div[12]/span/span";
        public string ErrorMessageXPath => "/html/body/div/main/div[2]/div/form/div[2]/div/ul/li";

        public SeleniumHelper _helper { get; set; }
        public ProviderPageHelper(SeleniumHelper helper)
        {
            _helper = helper;
        }

        public void AccessProviderList()
        {
            _helper.GoToUrl(_helper.Configuration.ProviderListUrl);
        }

        public string GetPageTitle()
        {
            return _helper.GetElementByXPath(PageTitleXPath).Text;
        }

        public void AccessAddProviderPage()
        {
            AccessProviderList();
            _helper.ClickByXPath(NewProviderLinkButtonXPath);
        }

        public string GetNewProviderPageTitle()
        {
            return _helper.GetElementByXPath(NewPageTitleXPath).Text;
        }

        public string GetNewProviderNameSpanValue()
        {
            return _helper.GetElementByXPath(SpanNameXPath).Text;
        }

        public string GetNewProviderDocumentSpanValue()
        {
            return _helper.GetElementByXPath(SpanDocumentXPath).Text;
        }

        public string GetNewProviderZipCodeSpanValue()
        {
            return _helper.GetElementByXPath(SpanZipCodeXPath).Text;
        }

        public string GetNewProviderStreetSpanValue()
        {
            return _helper.GetElementByXPath(SpanStreetXPath).Text;
        }

        public string GetNewProviderNumberSpanValue()
        {
            return _helper.GetElementByXPath(SpanNumberXPath).Text;
        }

        public string GetNewProviderDistrictSpanValue()
        {
            return _helper.GetElementByXPath(SpanDistrictXPath).Text;
        }

        public string GetNewProviderCitySpanValue()
        {
            return _helper.GetElementByXPath(SpanCityXPath).Text;
        }

        public string GetNewProviderStateSpanValue()
        {
            return _helper.GetElementByXPath(SpanStateXPath).Text;
        }

        public string GetErrorMessageValue()
        {
            return _helper.GetElementByXPath(ErrorMessageXPath).Text;
        }

        public void SetNameFieldValue(string value)
        {
            _helper.FillTextBoxById("Name", value);
        }

        public void SetDocumentFieldValue(string value)
        {
            _helper.FillTextBoxById("Document", value);
        }

        public void ChooseIndividualEntityKind(bool isIndividualEntity = true)
        {
            if (isIndividualEntity) _helper.ClickByXPath("/html/body/div/main/div[2]/div/form/div[3]/input[1]");
            if (!isIndividualEntity) _helper.ClickByXPath("/html/body/div/main/div[2]/div/form/div[3]/input[2]");
        }

        public void ChooseIfActive(bool isActive = true)
        {
            if (isActive) _helper.ClickOnButtonById("Active");
        }

        public void SetZipCodeFieldValue(string value)
        {
            _helper.FillTextBoxById("Address_ZipCode", value);
        }

        public void SetStreetFieldValue(string value)
        {
            _helper.FillTextBoxById("Address_Street", value);
        }

        public void SetNumberFieldValue(string value)
        {
            _helper.FillTextBoxById("Address_Number", value);
        }

        public void SetDistrictFieldValue(string value)
        {
            _helper.FillTextBoxById("Address_District", value);
        }

        public void SetStateFieldValue(string value)
        {
            _helper.FillTextBoxById("Address_State", value);
        }

        public void SetCityFieldValue(string value)
        {
            _helper.FillTextBoxById("Address_City", value);
        }

        public void SubmitForm()
        {
            _helper.ClickByXPath("/html/body/div/main/div[2]/div/form/div[13]/input");
        }
    }
}
