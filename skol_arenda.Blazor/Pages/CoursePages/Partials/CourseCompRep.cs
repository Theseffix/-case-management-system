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
        public bool CrModalVisible { get; set; }

        public string crSearchString { get; set; }

        [Parameter]
        public List<CompRep> CompReps { get; set; }

        private List<CompRep> selectedCompReps = new List<CompRep>();
        private List<CompRep> searchedCompReps = new List<CompRep>();
        private bool searchingCr;
        private string lastCrSearch = "";

        private void UpdateCrModal()
        {
            CrModalVisible = false;
        }
        private async Task OnCrSearchChange(ChangeEventArgs e)
        {
            var search = e.Value?.ToString().ToLower();

            if (search.Length > 2 && !searchingCr)
            {
                searchingCr = true;
                searchedCompReps = await CourseContext.SearchCompRepByName(search,CourseId);
                lastCrSearch = search;
            }
            else if (search.Contains(lastCrSearch) && searchingCr)
            {

                searchedCompReps = searchedCompReps.Where(x => x.FirstName.ToLower().Contains(search) || x.LastName.ToLower().Contains(search)).ToList();
                lastCrSearch = search;
            }
            else
            {
                searchedCompReps.Clear();
                searchingCr = false;
                lastCrSearch = "";
            }
            this.StateHasChanged();
        }

        private async Task AddCompRepsFromSearch()
        {
            await CourseContext.AddCompRepsFromSearch(selectedCompReps, CourseId);
            this.StateHasChanged();
            UpdateCrModal();
            this.StateHasChanged();
            await GetCompanyReps();

        }
        private async Task RemoveCompRepFromCourse(CompRep compRep)
        {
            List<CompRep> repToRemove = new List<CompRep>();
            repToRemove.Add(compRep);
            await CourseContext.RemoveCompRepsFromCourse(repToRemove, CourseId);
            StateHasChanged();
            await GetCompanyReps();
        }
        private void ToggleSelectedCompRep(CompRep c)
        {
            if (selectedCompReps.Exists(x => x.Equals(c)))
            {
                selectedCompReps.Remove(c);
            }
            else
            {
                selectedCompReps.Add(c);
            }
        }
    }
}

