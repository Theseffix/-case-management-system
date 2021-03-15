using ITHSManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITHSManagement.Pages.CoursePages
{
    public partial class CourseView
    {
        private Student student = new Student();
        private List<Student> selectedStudents = new List<Student>();
        private List<Student> searchedStudents = new List<Student>();
        private bool searchingStudent;
        private string lastStudentSearch = "";



        [Parameter]
        public bool StudentModalVisible { get; set; }
        public string SearchStringStudentStudentStudent { get; set; }

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
        private async Task RemoveStudentFromProgram(Student student)
        {
            List<Student> toRemove = new List<Student>();
            toRemove.Add(student);
            await CourseContext.RemoveStudentsFromCourse(toRemove, CourseId);
            await GetStudents();
        }
        private async Task OnStudentSearchChange(ChangeEventArgs e)
        {
            var search = e.Value?.ToString().ToLower();

            if (search.Length > 2 && !searchingStudent)
            {
                searchingStudent = true;
                searchedStudents = await CourseContext.SearchStudentByName(search, CourseId);
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
        private async Task AddStudentsFromSearch()
        {
            UpdateStudentModal();
            await CourseContext.AddStudentsFromSearch(selectedStudents, CourseId);
            await GetStudents();
            StateHasChanged();
            searchedStudents.Clear();
            selectedStudents.Clear();
        }
    }
}
