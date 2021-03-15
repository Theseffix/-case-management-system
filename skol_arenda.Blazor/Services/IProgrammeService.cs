using ITHSManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITHSManagement.Services
{
    public interface IProgrammeService : IService<Programme>
    {
        Task<int> AddCompRepsFromSearch(List<CompRep> compReps, int programmeId);
        Task<int> AddCoursesFromSearch(List<Course> courses, int programmeId);
        Task<bool> AddCourseToProgram(int courseId, int programmeId);
        Task<bool> AddedStudentToProgram(Student student, int programmeId);
        Task<int> AddGroupsFromSearch(List<Group> groups, int programmeId);
        Task<bool> AddGroupToProgram(int groupId, int programmeId);
        Task<int> AddStudentsFromSearch(List<Student> students, int programmeId);
        Task<bool> DeletedCourseFromProgram(int courseId, int programmeId);
        Task<bool> DeletedGroupFromProgram(int groupId, int programmeId);
        Task<bool> DeleteGroupFromProgram(int groupId, int programmeId);
        Task<List<CompRep>> GetCompanyRepsByProgramId(int programmeId);
        Task<List<Course>> GetCoursesByProgramId(int programmeId);
        Task<List<Group>> GetGroupsByProgramId(int programmeId);
        Task<List<Student>> GetStudentsByProgramId(int programmeId);
        Task<int> InsertCompRep(CompRep compRep);
        Task<int> RemoveCompRepsFromProgram(List<CompRep> compRep, int programmeId);
        Task<int> RemoveStudentsFromProgram(List<Student> students, int programmeId);
        Task<List<CompRep>> SearchCompRepByName(string name);
        Task<List<Course>> SearchCoursesByName(string name, int programmeId);
        Task<List<Group>> SearchGroupsByName(string name);
        Task<List<Student>> SearchStudentByName(string name, int programmeId);
    }
}