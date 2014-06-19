using Nancy;
using Nancy.Testing;
using Xunit;

namespace NancyIsFancy.Tests
{
    public class HomeModuleTests
    {
        [Fact]
        public void Should_return_status_unauthorized_when_super_secret_header_is_missing()
        {
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper);

            var result = browser.Get("/", with =>
                {
                    with.HttpRequest();
                });

            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public void Should_return_status_unauthorized_when_super_secret_header_value_is_incorrect()
        {
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper);

            var result = browser.Get("/", with =>
            {
                with.Header("X-Super-Secret", "wrong-O!");
                with.HttpRequest();
            });

            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public void Should_return_status_OK_when_super_secret_header_value_is_correct()
        {
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper);

            var result = browser.Get("/", with =>
            {
                with.Header("X-Super-Secret", "NancyIsFancy!");
                with.HttpRequest();
            });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
