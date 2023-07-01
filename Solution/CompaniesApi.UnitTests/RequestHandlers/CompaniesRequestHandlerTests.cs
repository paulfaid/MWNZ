using AutoFixture;
using AutoFixture.AutoNSubstitute;
using CompaniesApi.Exceptions;
using CompaniesApi.RequestHandlers;
using CompaniesApi.Services;
using NSubstitute;

namespace CompaniesApi.UnitTests.RequestHandlers
{
    [TestClass]
    public class CompaniesRequestHandlerTests
    {
        [TestMethod]
        public async Task Handler_returnsResponseOnValidXml()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoNSubstituteCustomization());

            var backendService = fixture.Freeze<IBackendService>();
            backendService.GetCompanyXml(default).ReturnsForAnyArgs("<?xml version='1.0' encoding='UTF-8'?><Data><id>1</id><name>MWNZ</name><description>..is awesome</description></Data>");

            var uut = fixture.Create<CompaniesRequestHandler>();
            var response = await uut.GetCompany(1);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Id == 1);
            Assert.IsTrue(response.Name == "MWNZ");
            Assert.IsTrue(response.Description == "..is awesome");
        }

        [TestMethod]
        public async Task Handler_returnsApiExcpetionOnInvalidXml()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoNSubstituteCustomization());

            var backendService = fixture.Freeze<IBackendService>();
            backendService.GetCompanyXml(default).ReturnsForAnyArgs("< not xml");

            var uut = fixture.Create<CompaniesRequestHandler>();

            await Assert.ThrowsExceptionAsync<ApiException>(async () => { await uut.GetCompany(1); });
        }

        [TestMethod]
        public async Task Handler_returnsApiExcpetionOnInvalidResponseId()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoNSubstituteCustomization());

            var backendService = fixture.Freeze<IBackendService>();
            backendService.GetCompanyXml(default).ReturnsForAnyArgs("<Data><id>bat</id></Data>");

            var uut = fixture.Create<CompaniesRequestHandler>();

            await Assert.ThrowsExceptionAsync<ApiException>(async () => { await uut.GetCompany(1); });
        }

        [TestMethod]
        public async Task Handler_returnsApiExcpetionOnMissingName()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoNSubstituteCustomization());

            var backendService = fixture.Freeze<IBackendService>();
            backendService.GetCompanyXml(default).ReturnsForAnyArgs("<Data><id>3</id><description>description</description></Data>");

            var uut = fixture.Create<CompaniesRequestHandler>();

            await Assert.ThrowsExceptionAsync<ApiException>(async () => { await uut.GetCompany(1); });
        }
    }
}
