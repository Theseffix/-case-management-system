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
        [Parameter]
        public bool ProgramModalVisible { get; set; }

        public void NavigateToProgram(Programme p)
        {
            NavigationManager.NavigateTo($"programme/{p.Id}");
        }
        private void UpdateProgramModal()
        {
            ProgramModalVisible = false;
        }
        private List<Programme> searchedProgramme = new List<Programme>();
        private List<Programme> selectedProgrammes = new List<Programme>();
        public string SearchProgrammeString { get; set; }
        private bool searchingProgramme;
        private string lastProgrammeSearch = "";


        private async Task OnProgramSearchChange(ChangeEventArgs e)
        {
            var search = e.Value?.ToString().ToLower();

            if (search.Length > 2 && !searchingProgramme)
            {
                searchingProgramme = true;
                searchedProgramme = await CourseContext.SearchProgramByName(search, CourseId);
                lastProgrammeSearch = search;
            }
            else if (search.Contains(lastProgrammeSearch) && searchingProgramme)
            {

                searchedProgramme = searchedProgramme.Where(x => x.ProgramName.ToLower().Contains(search)).ToList();
                lastProgrammeSearch = search;
            }
            else
            {
                searchedProgramme.Clear();
                searchingProgramme = false;
                lastProgrammeSearch = "";
            }
            this.StateHasChanged();
        }
        private async Task AddProgramsFromSearch()
        {
            UpdateProgramModal();
            await CourseContext.AddProgramFromSearch(selectedProgrammes, CourseId);
            this.StateHasChanged();
            await GetProgrammes();
        }
        private async Task RemoveProgrammeFromCourse(int programmeId)
        {
            await CourseContext.DeletedCourseFromProgram(CourseId, programmeId);
            StateHasChanged();
            await GetProgrammes();
        }
        private void ToggleSelectedProgram(Programme p)
        {
            if (selectedProgrammes.Exists(x => x.Equals(p)))
            {
                selectedProgrammes.Remove(p);
            }
            else
            {
                selectedProgrammes.Add(p);
            }
        }

    }
}

