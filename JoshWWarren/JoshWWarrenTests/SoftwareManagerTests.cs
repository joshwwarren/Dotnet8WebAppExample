using JoshWWarren;
using NUnit.Framework;
using Should;

namespace JoshWWarrenTests
{
    [TestFixture]
    public class SoftwareManagerTests
    {
        [Test]
        public void GetAllSoftware_ReturnsSoftware()
        {
            var software = SoftwareManager.GetAllSoftware();
            Assert.That(software, Is.Not.Null);
            Assert.That(software, Is.Not.Empty);
        }

        [TestCase("2")]
        [TestCase("2.0.0.0.0")]
        public void It_should_return_versions_greater_than(string input)
        {
            var software = SoftwareManager.Search(input);

            software.ShouldEqual("MS Word 13.2.1<br/>Angular 13<br/>Vue.js 2.6<br/>Visual Studio 17.0.31919.166.0<br/>Visual Studio 16.11.9.3.55<br/>Blazor 3.2.0");
        }

        [Test]
        public void It_should_return_versions_greater_than_accounting_for_0s()
        {
            var software = SoftwareManager.Search("17");

            software.ShouldEqual("Visual Studio 17.0.31919.166.0");
        }

        [TestCase("1.1.1.1.1.1")]
        [TestCase("ssome other stuff")]
        [TestCase(null)]
        [TestCase(" ")]
        public void It_should_return_an_error_if_input_has_more_than_5_parts(string? input)
        {
            SoftwareManager.Search(input)
                .ShouldEqual("<span style='color:red'>version is not valid</span>");
        }
    }
}