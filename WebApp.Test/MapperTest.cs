using NUnit.Framework;
using WebApp.Core.Helpers;
using WebApp.Core.Models;
using WebApp.Core.Models.ViewModels;

namespace Tests
{
    public class MapperTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_Be_True_On_FirstName()
        {
            var vm = new AutoGiroViewModel
            {
                FirstName = "Test"
            };
            var model = Mapper.Map<AutoGiroViewModel, Autogiro>(vm);

            Assert.AreEqual(vm.FirstName,model.FirstName);
        }
    }
}