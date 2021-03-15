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
        private Group group = new Group();
        private List<Group> groups = new List<Group>();
        private List<Group> selectedGroups = new List<Group>();
        private List<Group> searchedGroups = new List<Group>();
        private bool searchingGroup;
        private string lastGroupSearch = "";

        [Parameter]
        public bool GroupModalVisible { get; set; }
        public string SearchStringGroup { get; set; }

        private async Task AddGroupsFromSearch()
        {
            UpdateGroupModal();
            await CourseContext.AddGroupFromSearch(selectedGroups, CourseId);
            await GetStudents();
            StateHasChanged();
            searchedGroups.Clear();
            selectedGroups.Clear();
        }

        private async Task OnGroupSearchChange(ChangeEventArgs e)
        {
            var search = e.Value?.ToString().ToLower();

            if (search.Length > 2 && !searchingGroup)
            {
                searchingGroup = true;
                searchedGroups = await CourseContext.SearchGroupsByName(search, CourseId);
                lastGroupSearch = search;
            }
            else if (search.Contains(lastGroupSearch) && searchingGroup)
            {

                searchedGroups = searchedGroups.Where(x => x.Title.ToLower().Contains(search)).ToList();
                lastGroupSearch = search;
            }
            else
            {
                searchedGroups.Clear();
                searchingGroup = false;
                lastGroupSearch = "";
            }
            this.StateHasChanged();
        }
        private void ToggleSelectedStudent(Group s)
        {
            if (selectedGroups.Exists(x => x.Equals(s)))
            {
                selectedGroups.Remove(s);
            }
            else
            {
                selectedGroups.Add(s);
            }
        }

        private void UpdateGroupModal()
        {
            GroupModalVisible = false;
        }
    }
}

