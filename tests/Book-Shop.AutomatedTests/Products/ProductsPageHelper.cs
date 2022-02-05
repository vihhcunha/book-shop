using Book_Shop.AutomatedTests.Config;

namespace Book_Shop.AutomatedTests.Products;
public class ProductsPageHelper
{
    public string PageTitleXPath => "/html/body/div/main/div[1]/h1";
    public string PageTableXPath => "/html/body/div/main/div[2]/table";
    public string NewProductLinkButtonXPath => "/html/body/div/main/a";
    public string NewPageTitleXPath => "/html/body/div/main/div[1]/h1";

    public SeleniumHelper _helper { get; set; }
    public ProductsPageHelper(SeleniumHelper helper)
    {
        _helper = helper;
    }

    public void AccessProductsList()
    {
        _helper.GoToUrl(_helper.Configuration.ProductListUrl);
    }

    public string GetPageTitle()
    {
        return _helper.GetElementByXPath(PageTitleXPath).Text;
    }

    public void AccessAddProductPage()
    {
        AccessProductsList();
        _helper.ClickByXPath(NewProductLinkButtonXPath);
    }

    public string GetNewProductPageTitle()
    {
        return _helper.GetElementByXPath(NewPageTitleXPath).Text;
    }

    public void SetNameFieldValue(string value)
    {
        _helper.FillTextBoxById("Name", value);
    }

    public void SetProviderFieldValue()
    {
        _helper.FillDropDownByIdWithRandomItem("ProviderId");
    }

    public void SetDescriptionFieldValue(string value)
    {
        _helper.FillTextBoxById("Description", value);
    }

    public void SetValueFieldValue(string value)
    {
        _helper.FillTextBoxById("Value", value);
    }

    public void SetImageFieldValue()
    {
        _helper.FillInputFileById("ImageUpload", @"C:\Test\test.jpg");
    }

    public void ChooseIfActive(bool isActive = true)
    {
        if (isActive) _helper.ClickOnButtonById("Active");
    }

    public void SubmitForm()
    {
        _helper.ClickByXPath("/html/body/div/main/div[2]/div/form/div[7]/input");
    }
}