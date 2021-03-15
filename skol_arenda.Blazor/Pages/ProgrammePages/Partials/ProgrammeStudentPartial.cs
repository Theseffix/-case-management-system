using ITHSManagement.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ITHSManagement.Services;

namespace ITHSManagement.Pages.ProgrammePages
{
    partial class ProgramView
    {


        private Student student = new Student();
        private List<Student> selectedStudents = new List<Student>();
        private List<Student> searchedStudents = new List<Student>();
        private bool searchingStudent;
        private string lastStudentSearch = "";



        [Parameter]
        public bool StudentModalVisible { get; set; }
        public string SearchStringStudentStudentStudent { get; set; }

        private async Task AddStudentsFromSearch()
        {    
            UpdateStudentModal();
            await ProgramContext.AddStudentsFromSearch(selectedStudents, ProgrammeId);
            await GetStudents();
            StateHasChanged();
            searchedStudents.Clear();
            selectedStudents.Clear();
        }

        private async Task OnStudentSearchChange(ChangeEventArgs e)
        {
            var search = e.Value?.ToString().ToLower();

            if (search.Length > 2 && !searchingStudent)
            {
                searchingStudent = true;
                searchedStudents = await ProgramContext.SearchStudentByName(search, ProgrammeId);
                lastStudentSearch = search;
            }
            else if (search.Contains(lastStudentSearch) && searchingStudent)
            {

                searchedStudents = searchedStudents.Where(x => x.FirstName.ToLower().Contains(search) || x.LastName.ToLower().Contains(search)).ToList();
                lastStudentSearch = search;
            }
            else
            {
                searchedStudents.Clear();
                searchingStudent = false;
                lastStudentSearch = "";
            }
            this.StateHasChanged();
        }
        private async Task SearchStudentByName(string name)
        {
            await ProgramContext.SearchStudentByName(name, ProgrammeId);

        }
        private void ToggleSelectedStudent(Student s)
        {
            if (selectedStudents.Exists(x => x.Equals(s)))
            {
                selectedStudents.Remove(s);
            }
            else
            {
                selectedStudents.Add(s);
            }
        }


        private void UpdateStudentModal()
        {
            StudentModalVisible = false;
        }
    }
}