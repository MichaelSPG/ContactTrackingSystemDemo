using ContactTrackingSystem.Client.Shared;
using ContactTrackingSystem.Shared.Interfaces;
using ContactTrackingSystem.Shared.Model;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using System.Text;

namespace ContactTrackingSystem.Client.Pages
{
    /// <summary>
    /// Candidates page class, contains all the backend code
    /// </summary>
    public partial class Candidates : ComponentBase
    {      
        [Inject] DialogService? DialogService { get; set; }
        [Inject] ICandidateService? CustomerService { get; set; }
        CustomDataGrid<Candidate>? CustomDataGrid { get; set; }
        RadzenDataGrid<Candidate>? dataGrid { get; set; }
        private List<Candidate>? CandidateList { get; set; }
        private bool UseCustomDataGrid { get; set; }

        public Candidates()
        {
            CandidateList = new List<Candidate>();
        }
       
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await OnLoad();
        }
        /// <summary>
        /// Loads the Candidates and fills a datagrid
        /// </summary>
        /// <returns>Task</returns>
        async Task OnLoad()
        {
            CandidateList?.Clear();
            CandidateList.AddRange(await CustomerService?.GetAllCandidates());
            if(dataGrid != null)
                await dataGrid.Reload();
            StateHasChanged();
        }
        /// <summary>
        /// Removes a candidate from the source
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Task</returns>
        async Task OnRemoveCandidate(Candidate item)
        {
            await CustomerService.RemoveCandidate(item);
            await OnLoad();
            CustomDataGrid?.Update();
        }
        /// <summary>
        /// Edits a candidate the source
        /// </summary>
        /// <param name="item">the candidate instance</param>
        /// <returns>Task</returns>
        async Task OnEditCandidate(Candidate item)
        {
            await OpenCreateEditCandidate(item.Id.ToString());            
        }
        /// <summary>
        /// Triggered after creating or editing a candidate
        /// </summary>
        /// <param name="item">The candiadte instance</param>
        void OnConfirmCandidateDialog(Candidate item)
        {
            OnLoad().Wait();
            CustomDataGrid?.Update();
            StateHasChanged();
        }
        /// <summary>
        /// Opens a the candidate create or edit dialog 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task OpenCreateEditCandidate(string? id)
        {
            var dialog = await DialogService.OpenAsync<NewOrEditCandidate>(!string.IsNullOrWhiteSpace(id) ? "Edit Candidate" : "New Candidate",
                   new Dictionary<string, object>() { { "EntityId", id },
                       { "OnFinishForm", new EventCallback<Candidate>(this, OnConfirmCandidateDialog) },                      
                   },
                   new DialogOptions() { CloseDialogOnEsc = false });
        }
    }
}
