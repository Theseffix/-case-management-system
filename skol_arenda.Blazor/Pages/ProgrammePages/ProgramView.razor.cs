using ITHSManagement.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ITHSManagement.Services;
using System;

// In SDK-style projects such as this one, several assembly attributes that were historically
// defined in this file are now automatically added during build and populated with
// values defined in project properties. For details of which attributes are included
// and how to customise this process see: https://aka.ms/assembly-info-properties


// Setting ComVisible to false makes the types in this assembly not visible to COM
// components.  If you need to access a type in this assembly from COM, set the ComVisible
// attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM.

[assembly: Guid("55114667-dd8f-44dd-a3cb-dde8bb073695")]
namespace ITHSManagement.Pages.ProgrammePages
{
    public partial class ProgramView
    {
        [Inject]
        IProgrammeService ProgramContext { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        TaskRepository TaskDb { get; set; }
        [Inject]
        TaskService TaskServ { get; set; }
        [Inject]
        ProgrammeRepository ProgrammeDb { get; set; }


        [Parameter]
        public bool ProgramModalVisible { get; set; }
        [Parameter]
        public bool ProgramGroupModalVisible { get; set; }
        [Parameter]
        public bool DeleteProgramModal { get; set; }

        public bool test { get; set; }
        [Parameter]
        public int ProgrammeId { get; set; }

        [Parameter]
        public Programme Programme { get; set; }
        private List<Student> students = new List<Student>();

        private List<Course> courses = new List<Course>();
        private Course course = new Course();

        private List<Group> groups = new List<Group>();
        private Group group = new Group();

        private List<CompRep> companyreps = new List<CompRep>();
        private CompRep compRep = new CompRep();

        private List<TaskItem> TaskList = new List<TaskItem>();
        private TaskItem LastClickedRow = new TaskItem();

        string[] priorities = { "Ej Prio", "Låg", "Medel", "Hög", "Omedelbar" };
        string selectedPriority = "Medel";
        string[] consequences = { "Ingen", "Låg", "Medel", "Hög", "Dödlig" };
        string selectedConsequence = "Medel";

        private bool EditProgrammeBool = true;
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

        public void NavigateToStudent(Student s)
        {
            NavigationManager.NavigateTo($"studentprofile/{s.Id}");
        }
        public void NavigateToPrograms()
        {
            NavigationManager.NavigateTo("showprograms");
        }

        public void NavigateToCourse(Course c)
        {
            NavigationManager.NavigateTo($"course/{c.Id}");
        }

        private async Task GetGroups()
        {
            groups = await ProgramContext.GetGroupsByProgramId(ProgrammeId);
        }

        private async Task GetCourses()
        {
            courses = await ProgramContext.GetCoursesByProgramId(ProgrammeId);
        }

        private async Task GetStudents()
        {
            students = await ProgramContext.GetStudentsByProgramId(ProgrammeId);
            StateHasChanged();
        }
        private async Task GetCompanyReps()
        {
            companyreps = await ProgramContext.GetCompanyRepsByProgramId(ProgrammeId);
        }
        private async Task RemoveStudentFromProgram(Student student)
        {
            List<Student> toRemove = new List<Student>();
            toRemove.Add(student);
            await ProgramContext.RemoveStudentsFromProgram(toRemove, ProgrammeId);
            await GetStudents();
        }
        private async Task RemoveCompanyRepFromProgram(CompRep compRep)
        {
            List<CompRep> repToRemove = new List<CompRep>();
            repToRemove.Add(compRep);
            await ProgramContext.RemoveCompRepsFromProgram(repToRemove, ProgrammeId);
            StateHasChanged();
            await GetCompanyReps();
        }
        private async Task RemoveCourseFromProgram(int courseId)
        {
            await ProgramContext.DeletedCourseFromProgram(courseId, ProgrammeId);
            StateHasChanged();
            await GetCourses();
        }
        private async Task AddCompanyRep() //Tillfällig
        {
            test = false;
            await ProgramContext.InsertCompRep(compRep);
            StateHasChanged();
            await GetCompanyReps();
        }
        private async Task DeleteProgramme(int ProgrammeId)
        {
            await ProgramContext.Delete(ProgrammeId);
            NavigateToPrograms();
        }

        protected override async Task OnInitializedAsync()
        {
            Programme = (await ProgramContext.Get(ProgrammeId));
            TaskList = TaskDb.GetAllTasksByProgrammeId(ProgrammeId);
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now.AddDays(7);
        }
        private void EditModal()
        {
            ProgramModalVisible = false;
        }
        private void DeleteModal()
        {
            DeleteProgramModal = false;
        }
        private async Task EditProgramme(Programme programme)
        {
            await ProgramContext.Update(programme);
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

            await TaskServ.CreateTaskProgramme(t, ProgrammeId);

            TaskList = TaskDb.GetAllTasksByProgrammeId(ProgrammeId);
            StateHasChanged();
            ModalMd = false;
        }

        public void EditProgrammeButtons()
        {
            HiddenEditButton = true;
            HiddenSaveButton = false;
            EditProgrammeBool = false;
        }

        public async Task EditProgramme()
        {
            HiddenEditButton = false;
            HiddenSaveButton = true;
            EditProgrammeBool = true;

            Programme p = new Programme();

            p.Id = Programme.Id;
            p.YhNumber = Programme.YhNumber;
            p.Description = Programme.Description;
            p.StartDate = Programme.StartDate;
            p.EndDate = Programme.EndDate;
            p.CourseAdmin = Programme.CourseAdmin;


            await ProgrammeDb.Update(p);
            StateHasChanged();
        }
    }

}