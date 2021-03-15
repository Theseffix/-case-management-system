using ITHSManagement.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ITHSManagement.Services;
using System;

namespace ITHSManagement.Pages.CoursePages
{
    public partial class CourseView : ComponentBase
    {
        [Inject]
        CourseService CourseContext { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        TaskRepository TaskDb { get; set; }
        [Inject]
        TaskService TaskServ { get; set; }
        [Inject]
        CourseRepository CourseDb { get; set; }
        [Parameter]
        public bool CourseEditVisible { get; set; }
        [Parameter]
        public bool DeleteCourseModal { get; set; }
        [Parameter]
        public int CourseId { get; set; }

        private Course Course = new Course();
        private List<Student> students = new List<Student>();

        private List<Programme> programmes = new List<Programme>();

        private List<CompRep> companyreps = new List<CompRep>();

        private List<TaskItem> TaskList = new List<TaskItem>();
        private TaskItem LastClickedRow = new TaskItem();

        string[] priorities = { "Ej Prio", "Låg", "Medel", "Hög", "Omedelbar" };
        string selectedPriority = "Medel";
        string[] consequences = { "Ingen", "Låg", "Medel", "Hög", "Dödlig" };
        string selectedConsequence = "Medel";

        private bool EditCourseBool = true;
        private bool HiddenEditButton = false;
        private bool HiddenSaveButton = true;

        public bool ModalMd { get; set; }
        private DemoBasicModel model = new DemoBasicModel();
        public class DemoBasicModel
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int Priority { get; set; } //How imprtant (1-5)
            public int Consequence { get; set; } //How bad if not finished (1-5)
            public int UserID { get; set; }

        }

        private void DeleteModal()
        {
            DeleteCourseModal = false;
        }
        public void NavigateToStudent(Student s)
        {
            NavigationManager.NavigateTo($"studentprofile/{s.Id}");
        }
        public void NavigateToCourses()
        {
            NavigationManager.NavigateTo("courses");
        }
/*        private async Task GetGroups()
        {
            groups = await CourseContext.GetGroupsByCourseId(CourseId);
        }*/

        private async Task GetProgrammes()
        {
            programmes = await CourseContext.GetProgrammesByCourseId(CourseId);
        }

        private async Task GetStudents()
        {
            students = await CourseContext.GetStudentsByCourseId(CourseId);
            StateHasChanged();
        }
        private async Task GetCompanyReps()
        {
            companyreps = await CourseContext.GetCompanyRepsByCourseId(CourseId);
        }
        private async Task GetGroups()
        {
            groups = await CourseContext.GetGroupsByCourseId(CourseId);
        }
        private async Task DeleteCourse(int courseId)
        {
            await CourseContext.Delete(courseId);
            NavigateToCourses();
        }
        protected override async Task OnInitializedAsync()
        {
            Course = (await CourseContext.Get(CourseId));
            TaskList = TaskDb.GetAllTasksByCourseId(CourseId);
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now.AddDays(7);
        }

        public void RowClickedTask(Blazority.Shared.MouseEventData<TaskItem> e)
        {
            LastClickedRow = e.Data;
            NavigationManager.NavigateTo($"taskprofile/{e.Data.Id}");
        }

        public string DynamicRowClass(TaskItem row, int index)
        {
            // NOTE: row-selected class is a built-in datagrid class
            return row == LastClickedRow ? "row-selected" : "";
        }

        private async Task AddTask()
        {
            TaskItem t = new TaskItem();

            t.Title = model.Title;
            t.CreationDate = DateTime.Now;
            t.Description = model.Description;
            t.StartDate = model.StartDate;
            t.EndDate = model.EndDate;
            t.Status = "Aktiv";
            t.UpdateCount = 0;

            if (selectedPriority == "Ej Prio")
            {
                t.Priority = 1;
            }
            else if (selectedPriority == "Låg")
            {
                t.Priority = 2;
            }
            else if (selectedPriority == "Medel")
            {
                t.Priority = 3;
            }
            else if (selectedPriority == "Hög")
            {
                t.Priority = 4;
            }
            else if (selectedPriority == "Omedelbar")
            {
                t.Priority = 5;
            }

            if (selectedConsequence == "Ingen")
            {
                t.Consequence = 1;
            }
            else if (selectedConsequence == "Låg")
            {
                t.Consequence = 2;
            }
            else if (selectedConsequence == "Medel")
            {
                t.Consequence = 3;
            }
            else if (selectedConsequence == "Hög")
            {
                t.Consequence = 4;
            }
            else if (selectedConsequence == "Dödlig")
            {
                t.Consequence = 5;
            }

            await TaskServ.CreateTaskCourse(t, CourseId);

            TaskList = TaskDb.GetAllTasksByCourseId(CourseId);
            StateHasChanged();
            ModalMd = false;
        }

        public void EditCourseButtons()
        {
            HiddenEditButton = true;
            HiddenSaveButton = false;
            EditCourseBool = false;
        }

        public async Task EditCourse()
        {
            HiddenEditButton = false;
            HiddenSaveButton = true;
            EditCourseBool = true;

            Course c = new Course();

            c.Id = Course.Id;
            c.CourseName = Course.CourseName;
            c.Points = Course.Points;
            c.Description = Course.Description;
            c.StartDate = Course.StartDate;
            c.EndDate = Course.EndDate;
            c.CourseAdmin = Course.CourseAdmin;


            await CourseDb.Update(c);
            StateHasChanged();
        }
    }
}
