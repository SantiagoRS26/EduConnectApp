using Xunit.Abstractions;
using Xunit;
using EduConnect.BLL.Services;
using EduConnect.BLL.Interfaces;

namespace EduConnect.Tests
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper output;
        private readonly ISecurityService securityService;

        public UnitTest1(ITestOutputHelper output, ISecurityService securityService)
        {
            this.output = output;
            this.securityService = securityService;
        }

        [Fact]
        public void Test1()
        {
            string contra = securityService.EncryptPassword("contrasena");
            output.WriteLine(contra);
        }
    }
}