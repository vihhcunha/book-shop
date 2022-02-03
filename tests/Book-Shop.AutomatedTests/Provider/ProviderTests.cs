using Bogus;
using Book_Shop.AutomatedTests.Config;
using Book_Shop.AutomatedTests.Login;
using System;
using Xunit;
using Bogus.Extensions.Brazil;
using Book_Shop.AutomatedTests.Extensions;

namespace Book_Shop.AutomatedTests.Provider
{
    [Collection(nameof(AutomationWebFixtureCollection))]
    public class ProviderTests
    {
        private readonly AutomationWebTestsFixture _testsFixture;
        private readonly ProviderPageHelper _providerPageHelper;
        private readonly LoginPageHelper _loginPageHelper;

        public ProviderTests(AutomationWebTestsFixture fixture)
        {
            _testsFixture = fixture;
            _providerPageHelper = new ProviderPageHelper(fixture.BrowserHelper);
            _loginPageHelper = new LoginPageHelper(fixture.BrowserHelper);

            _loginPageHelper.Login();
        }

        [Fact(DisplayName = "User can access provider list")]
        [Trait("Category", "ProviderPage")]
        public void ProviderPage_AccessProviderList_UserCanAccess()
        {
            _providerPageHelper.AccessProviderList();

            Assert.Contains("Providers List", _providerPageHelper.GetPageTitle(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "User can access new provider page")]
        [Trait("Category", "ProviderPage")]
        public void ProviderPage_AccessNewProviderPage_UserCanAccess()
        {
            _providerPageHelper.AccessAddProviderPage();

            Assert.Contains("New provider", _providerPageHelper.GetNewProviderPageTitle(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "New provider - add new individual provider")]
        [Trait("Category", "ProviderPage")]
        public void ProviderPage_AddNewIndividualProvider_MustAdd()
        {
            var faker = new Faker();

            _providerPageHelper.AccessAddProviderPage();
            _providerPageHelper.SetNameFieldValue(faker.Name.FullName());
            _providerPageHelper.SetDocumentFieldValue(faker.Person.Cpf(false));
            _providerPageHelper.ChooseIndividualEntityKind();
            _providerPageHelper.ChooseIfActive();
            FillAddressFields(faker);
            _providerPageHelper.SubmitForm();

            Assert.Contains("Providers List", _testsFixture.BrowserHelper.GetElementByXPathWaitingForTitle(_providerPageHelper.PageTitleXPath, "Providers List").Text, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "New provider - add new legal provider")]
        [Trait("Category", "ProviderPage")]
        public void ProviderPage_AddNewLegalProvider_MustAdd()
        {
            var faker = new Faker();

            _providerPageHelper.AccessAddProviderPage();
            _providerPageHelper.SetNameFieldValue(faker.Company.CompanyName());
            _providerPageHelper.SetDocumentFieldValue(faker.Company.Cnpj(false));
            _providerPageHelper.ChooseIndividualEntityKind(false);
            _providerPageHelper.ChooseIfActive();
            FillAddressFields(faker);
            _providerPageHelper.SubmitForm();

            Assert.Contains("Providers List", _testsFixture.BrowserHelper.GetElementByXPathWaitingForTitle(_providerPageHelper.PageTitleXPath, "Providers List").Text, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "New provider - validate name")]
        [Trait("Category", "ProviderPage")]
        public void ProviderPage_AddNewProviderWithEmptyName_MustShowErrorMessage()
        {
            var faker = new Faker();

            _providerPageHelper.AccessAddProviderPage();
            _providerPageHelper.SetNameFieldValue("");
            _providerPageHelper.SetDocumentFieldValue(faker.Person.Cpf(false));
            _providerPageHelper.ChooseIndividualEntityKind();
            _providerPageHelper.ChooseIfActive();
            FillAddressFields(faker);
            _providerPageHelper.SubmitForm();

            Assert.Contains("Mandatory", _providerPageHelper.GetNewProviderNameSpanValue(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Theory(DisplayName = "New provider - validate document size")]
        [InlineData("0")]
        [InlineData("")]
        [Trait("Category", "ProviderPage")]
        public void ProviderPage_AddNewProviderWithEmptyDocument_MustShowErrorMessage(string wrongDoc)
        {
            var faker = new Faker();

            _providerPageHelper.AccessAddProviderPage();
            _providerPageHelper.SetNameFieldValue(faker.Name.FullName());
            _providerPageHelper.SetDocumentFieldValue(wrongDoc);
            _providerPageHelper.ChooseIndividualEntityKind();
            _providerPageHelper.ChooseIfActive();
            FillAddressFields(faker);
            _providerPageHelper.SubmitForm();

            Assert.Contains("Document", _providerPageHelper.GetNewProviderDocumentSpanValue(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Theory(DisplayName = "New provider - validate individal entity document")]
        [InlineData("00000000000")]
        [InlineData("08794515875")]
        [Trait("Category", "ProviderPage")]
        public void ProviderPage_AddNewIndividualProviderWithWrongDocument_MustShowErrorMessage(string wrongDoc)
        {
            var faker = new Faker();

            _providerPageHelper.AccessAddProviderPage();
            _providerPageHelper.SetNameFieldValue(faker.Name.FullName());
            _providerPageHelper.SetDocumentFieldValue(wrongDoc);
            _providerPageHelper.ChooseIndividualEntityKind();
            _providerPageHelper.ChooseIfActive();
            FillAddressFields(faker);
            _providerPageHelper.SubmitForm();

            Assert.Contains("invalid", _providerPageHelper.GetErrorMessageValue(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Theory(DisplayName = "New provider - validate legal entity document")]
        [InlineData("00000000000000")]
        [InlineData("25669214812747")]
        [InlineData("08794515875458")]
        [Trait("Category", "ProviderPage")]
        public void ProviderPage_AddNewLegalProviderWithWrongDocument_MustShowErrorMessage(string wrongDoc)
        {
            var faker = new Faker();

            _providerPageHelper.AccessAddProviderPage();
            _providerPageHelper.SetNameFieldValue(faker.Company.CompanyName());
            _providerPageHelper.SetDocumentFieldValue(wrongDoc);
            _providerPageHelper.ChooseIndividualEntityKind(false);
            _providerPageHelper.ChooseIfActive();
            FillAddressFields(faker);
            _providerPageHelper.SubmitForm();

            Assert.Contains("invalid", _providerPageHelper.GetErrorMessageValue(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "New provider - validate zip code")]
        [Trait("Category", "ProviderPage")]
        public void ProviderPage_AddNewProviderWithEmptyZipCode_MustShowErrorMessage()
        {
            var faker = new Faker();

            _providerPageHelper.AccessAddProviderPage();
            _providerPageHelper.SetNameFieldValue(faker.Name.FullName());
            _providerPageHelper.SetDocumentFieldValue(faker.Person.Cpf(false));
            _providerPageHelper.ChooseIndividualEntityKind();
            _providerPageHelper.ChooseIfActive();
            FillAddressFields(faker);
            _providerPageHelper.SetZipCodeFieldValue("");
            _providerPageHelper.SubmitForm();

            Assert.Contains("Mandatory", _providerPageHelper.GetNewProviderZipCodeSpanValue(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "New provider - validate street")]
        [Trait("Category", "ProviderPage")]
        public void ProviderPage_AddNewProviderWithEmptyStreet_MustShowErrorMessage()
        {
            var faker = new Faker();

            _providerPageHelper.AccessAddProviderPage();
            _providerPageHelper.SetNameFieldValue(faker.Name.FullName());
            _providerPageHelper.SetDocumentFieldValue(faker.Person.Cpf(false));
            _providerPageHelper.ChooseIndividualEntityKind();
            _providerPageHelper.ChooseIfActive();
            FillAddressFields(faker);
            _providerPageHelper.SetStreetFieldValue("");
            _providerPageHelper.SubmitForm();

            Assert.Contains("Mandatory", _providerPageHelper.GetNewProviderStreetSpanValue(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "New provider - validate number")]
        [Trait("Category", "ProviderPage")]
        public void ProviderPage_AddNewProviderWithEmptyNumber_MustShowErrorMessage()
        {
            var faker = new Faker();

            _providerPageHelper.AccessAddProviderPage();
            _providerPageHelper.SetNameFieldValue(faker.Name.FullName());
            _providerPageHelper.SetDocumentFieldValue(faker.Person.Cpf(false));
            _providerPageHelper.ChooseIndividualEntityKind();
            _providerPageHelper.ChooseIfActive();
            FillAddressFields(faker);
            _providerPageHelper.SetNumberFieldValue("");
            _providerPageHelper.SubmitForm();

            Assert.Contains("Mandatory", _providerPageHelper.GetNewProviderNumberSpanValue(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "New provider - validate district")]
        [Trait("Category", "ProviderPage")]
        public void ProviderPage_AddNewProviderWithEmptyDistrict_MustShowErrorMessage()
        {
            var faker = new Faker();

            _providerPageHelper.AccessAddProviderPage();
            _providerPageHelper.SetNameFieldValue(faker.Name.FullName());
            _providerPageHelper.SetDocumentFieldValue(faker.Person.Cpf(false));
            _providerPageHelper.ChooseIndividualEntityKind();
            _providerPageHelper.ChooseIfActive();
            FillAddressFields(faker);
            _providerPageHelper.SetDistrictFieldValue("");
            _providerPageHelper.SubmitForm();

            Assert.Contains("Mandatory", _providerPageHelper.GetNewProviderDistrictSpanValue(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "New provider - validate city")]
        [Trait("Category", "ProviderPage")]
        public void ProviderPage_AddNewProviderWithEmptyCity_MustShowErrorMessage()
        {
            var faker = new Faker();

            _providerPageHelper.AccessAddProviderPage();
            _providerPageHelper.SetNameFieldValue(faker.Name.FullName());
            _providerPageHelper.SetDocumentFieldValue(faker.Person.Cpf(false));
            _providerPageHelper.ChooseIndividualEntityKind();
            _providerPageHelper.ChooseIfActive();
            FillAddressFields(faker);
            _providerPageHelper.SetCityFieldValue("");
            _providerPageHelper.SubmitForm();

            Assert.Contains("Mandatory", _providerPageHelper.GetNewProviderCitySpanValue(), StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "New provider - validate state")]
        [Trait("Category", "ProviderPage")]
        public void ProviderPage_AddNewProviderWithEmptyState_MustShowErrorMessage()
        {
            var faker = new Faker();

            _providerPageHelper.AccessAddProviderPage();
            _providerPageHelper.SetNameFieldValue(faker.Name.FullName());
            _providerPageHelper.SetDocumentFieldValue(faker.Person.Cpf(false));
            _providerPageHelper.ChooseIndividualEntityKind();
            _providerPageHelper.ChooseIfActive();
            FillAddressFields(faker);
            _providerPageHelper.SetStateFieldValue("");
            _providerPageHelper.SubmitForm();

            Assert.Contains("Mandatory", _providerPageHelper.GetNewProviderStateSpanValue(), StringComparison.InvariantCultureIgnoreCase);
        }

        private void FillAddressFields(Faker faker)
        {
            _providerPageHelper.SetZipCodeFieldValue(faker.Address.ZipCode("########"));
            _providerPageHelper.SetStreetFieldValue(faker.Address.StreetName());
            _providerPageHelper.SetNumberFieldValue(faker.Random.Number(0, 1000).ToString());
            _providerPageHelper.SetDistrictFieldValue(faker.Address.City());
            _providerPageHelper.SetCityFieldValue(faker.Address.City());
            _providerPageHelper.SetStateFieldValue(faker.Address.StateAbbr());
        }
    }
}
