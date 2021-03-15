using ITHSManagement.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ITHSManagement.Services;


namespace ITHSManagement.Pages.ProgrammePages
{
    public partial class ProgramView : ComponentBase
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
                searchedCompReps = await ProgramContext.SearchCompRepByName(search);
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
            await ProgramContext.AddCompRepsFromSearch(selectedCompReps, ProgrammeId);
            this.StateHasChanged();
            UpdateCrModal();
            this.StateHasChanged();
            await GetCompanyReps();

        }
        private void ToggleSelectedCompRep(CompRep s)
        {
            if (selectedCompReps.Exists(x => x.Equals(s)))
            {
                selectedCompReps.Remove(s);
            }
            else
            {
                selectedCompReps.Add(s);
            }
        }
    }


}