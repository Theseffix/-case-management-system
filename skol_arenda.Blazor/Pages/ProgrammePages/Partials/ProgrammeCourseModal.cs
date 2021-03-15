using System.Runtime.InteropServices;
using ITHSManagement.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITHSManagement.Services;


namespace ITHSManagement.Pages.ProgrammePages
{
    public partial class ProgramView
    {
        [Parameter]
        public bool CourseModalVisible { get; set; }


        private void UpdateCourseModal()
        {
            CourseModalVisible = false;
        }


        private List<Course> searchedCourses = new List<Course>();
        private List<Course> selectedCourses = new List<Course>();
        public string SearchCourseString { get; set; }
        private bool searchingCourse;
        private string lastCourseSearch = "";


        private async Task OnCourseSearchChange(ChangeEventArgs e)
        {
            var search = e.Value?.ToString().ToLower();

            if (search.Length > 2 && !searchingCourse)
            {
                searchingCourse = true;
                searchedCourses = await ProgramContext.SearchCoursesByName(search, ProgrammeId);
                lastCourseSearch = search;
            }
            else if (search.Contains(lastCourseSearch) && searchingCourse)
            {

                searchedCourses = searchedCourses.Where(x => x.CourseName.ToLower().Contains(search)).ToList();
                lastCourseSearch = search;
            }
            else
            {
                searchedCourses.Clear();
                searchingCourse = false;
                lastCourseSearch = "";
            }
            this.StateHasChanged();
        }
        private async Task AddCoursesToProgram()
        {
            await ProgramContext.AddCoursesFromSearch(selectedCourses, ProgrammeId);
            this.StateHasChanged();
            UpdateCourseModal();
            this.StateHasChanged();
            await GetCourses();
        }

        private void ToggleSelectedCourse(Course c)
        {
            if (selectedCourses.Exists(x => x.Equals(c)))
            {
                selectedCourses.Remove(c);
            }
            else
            {
                selectedCourses.Add(c);
            }
        }

    }
}