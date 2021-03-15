using ITHSManagement.Models;
using ITHSManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Linq;
using Xunit;

namespace skol_arenda.Tests
{
    public class StudentSearch
    {
        /*[Fact]
        public async void SearchShouldReturnStudent()
        {
            //TODO mock Ilogger
            var context = new EFContextFactory();
            var logger = new NullLogger<ProgrammeService>();
            var service = new ProgrammeService(context, logger);
            int programId;
            using (var db = context.CreateDbContext())
            {
                programId = db.Programmes.First().Id;
            }
            var s = await service.SearchStudentByName("Stude", programId);
            Assert.Equal("Student", s.Where(s => s.FirstName == "Student").ToList().First().FirstName); 
        }*/
    }
}
