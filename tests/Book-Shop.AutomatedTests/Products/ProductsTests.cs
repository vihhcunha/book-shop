﻿using Bogus;
using Book_Shop.AutomatedTests.Config;
using Book_Shop.AutomatedTests.Login;
using Book_Shop.AutomatedTests.Provider;
using System;
using Xunit;

namespace Book_Shop.AutomatedTests.Products;

[Collection(nameof(AutomationWebFixtureCollection))]
public class ProductsTests
{
    private readonly AutomationWebTestsFixture _testsFixture;
    private readonly ProductsPageHelper _productsPageHelper;
    private readonly ProviderPageHelper _providerPageHelper;
    private readonly LoginPageHelper _loginPageHelper;

    public ProductsTests(AutomationWebTestsFixture fixture)
    {
        _testsFixture = fixture;
        _productsPageHelper = new ProductsPageHelper(fixture.BrowserHelper);
        _providerPageHelper = new ProviderPageHelper(fixture.BrowserHelper);
        _loginPageHelper = new LoginPageHelper(fixture.BrowserHelper);

        _loginPageHelper.Login();
    }

    [Fact(DisplayName = "User can access products list")]
    [Trait("Category", "ProductsPage")]
    public void ProductsPage_AccessProductList_UserCanAccess()
    {
        _productsPageHelper.AccessProductsList();

        Assert.Contains("Product List", _productsPageHelper.GetPageTitle(), StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact(DisplayName = "User can access new product page")]
    [Trait("Category", "ProductsPage")]
    public void ProductsPage_AccessNewProductPage_UserCanAccess()
    {
        _productsPageHelper.AccessAddProductPage();

        Assert.Contains("New product", _productsPageHelper.GetNewProductPageTitle(), StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact(DisplayName = "New product - add new product")]
    [Trait("Category", "ProductsPage")]
    public void ProductPage_AddNewProduct_MustAdd()
    {
        var faker = new Faker();
        var name = faker.Commerce.ProductName();

        _productsPageHelper.AccessAddProductPage();
        _productsPageHelper.SetNameFieldValue(name);
        _productsPageHelper.SetProviderFieldValue();
        _productsPageHelper.SetDescriptionFieldValue(faker.Commerce.ProductDescription());
        _productsPageHelper.SetValueFieldValue(faker.Commerce.Price(1, 10000, 2));
        _productsPageHelper.ChooseIfActive(true);
        _productsPageHelper.SetImageFieldValue();
        _productsPageHelper.SubmitForm();

        Assert.Contains("Product List", _testsFixture.BrowserHelper.GetElementByXPathWaitingForTitle(_productsPageHelper.PageTitleXPath, "Product List").Text, StringComparison.InvariantCultureIgnoreCase);
        Assert.True(_testsFixture.BrowserHelper.SearchIfTextExistsInTable(name, _productsPageHelper.PageTableXPath));
    }

    [Fact(DisplayName = "New product - validate name")]
    [Trait("Category", "ProductsPage")]
    public void ProductPage_AddNewProductWithEmptyName_MustShowErrorMessage()
    {
        var faker = new Faker();

        _productsPageHelper.AccessAddProductPage();
        _productsPageHelper.SetNameFieldValue("");
        _productsPageHelper.SetProviderFieldValue();
        _productsPageHelper.SetDescriptionFieldValue(faker.Commerce.ProductDescription());
        _productsPageHelper.SetValueFieldValue(faker.Commerce.Price(1, 10000, 2));
        _productsPageHelper.ChooseIfActive(true);
        _productsPageHelper.SetImageFieldValue();
        _productsPageHelper.SubmitForm();

        Assert.Contains("mandatory", _productsPageHelper.GetNameSpanValue(), StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact(DisplayName = "New product - validate provider")]
    [Trait("Category", "ProductsPage")]
    public void ProductPage_AddNewProductWithEmptyProvider_MustShowErrorMessage()
    {
        var faker = new Faker();

        _productsPageHelper.AccessAddProductPage();
        _productsPageHelper.SetNameFieldValue(faker.Commerce.ProductName());
        _productsPageHelper.SetDescriptionFieldValue("");
        _productsPageHelper.SetValueFieldValue(faker.Commerce.Price(1, 10000, 2));
        _productsPageHelper.ChooseIfActive(true);
        _productsPageHelper.SetImageFieldValue();
        _productsPageHelper.SubmitForm();

        Assert.Contains("mandatory", _productsPageHelper.GetProviderSpanValue(), StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact(DisplayName = "New product - validate description")]
    [Trait("Category", "ProductsPage")]
    public void ProductPage_AddNewProductWithEmptyDescription_MustShowErrorMessage()
    {
        var faker = new Faker();

        _productsPageHelper.AccessAddProductPage();
        _productsPageHelper.SetProviderFieldValue();
        _productsPageHelper.SetNameFieldValue(faker.Commerce.ProductName());
        _productsPageHelper.SetDescriptionFieldValue("");
        _productsPageHelper.SetValueFieldValue(faker.Commerce.Price(1, 10000, 2));
        _productsPageHelper.ChooseIfActive(true);
        _productsPageHelper.SetImageFieldValue();
        _productsPageHelper.SubmitForm();

        Assert.Contains("mandatory", _productsPageHelper.GetDescriptionSpanValue(), StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact(DisplayName = "New product - validate image")]
    [Trait("Category", "ProductsPage")]
    public void ProductPage_AddNewProductWithEmptyImage_MustShowErrorMessage()
    {
        var faker = new Faker();

        _productsPageHelper.AccessAddProductPage();
        _productsPageHelper.SetNameFieldValue(faker.Commerce.ProductName());
        _productsPageHelper.SetProviderFieldValue();
        _productsPageHelper.SetDescriptionFieldValue(faker.Commerce.ProductDescription());
        _productsPageHelper.SetValueFieldValue(faker.Commerce.Price(1, 10000, 2));
        _productsPageHelper.ChooseIfActive(true);
        _productsPageHelper.SubmitForm();

        Assert.Contains("choose", _productsPageHelper.GetImageSpanValue(), StringComparison.InvariantCultureIgnoreCase);
    }
}

