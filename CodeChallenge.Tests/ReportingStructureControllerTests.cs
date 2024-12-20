using System.Net;
using System.Net.Http;
using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }
        

        [TestMethod]
        public void GetReportingStructureById_Returns_Ok()
        {
            // Arrange
            var employee = new Employee()
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Department = "Engineering",
                FirstName = "John",
                LastName = "Lennon",
                Position = "Development Manager",
            };
            var expectedReportingStructure = new ReportingStructure
            {
                Employee = employee,
                NumberOfReports = 4
            };

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reporting-structure/{employee.EmployeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(employee.EmployeeId, reportingStructure.Employee.EmployeeId);
            Assert.AreEqual(expectedReportingStructure.NumberOfReports, reportingStructure.NumberOfReports);
        }
        
        [TestMethod]
        public void GetReportingStructureById_Returns_NotFound()
        {
            // Arrange
            const string nonexistentEmployeeId = "not-a-real-guid";

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reporting-structure/{nonexistentEmployeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
