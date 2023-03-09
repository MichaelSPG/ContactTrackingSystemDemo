using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;
using System.Reflection;
using System.Text.RegularExpressions;
using ContactTrackingSystem.Shared.Interfaces;
using FluentValidation;
using PhoneNumbers;
using ContactTrackingSystem.Shared.Data;
using System.Reflection.Emit;
using ContactTrackingSystem.Shared.Model;

namespace ContactTrackingSystem.Client.Shared
{    
    public partial class NewOrEditCandidate : ComponentBase
    {        
        [Parameter]
        public string? EntityId { get; set; }
        [Parameter]
        public EventCallback<Candidate>? OnFinishForm { get; set; }
        [Inject] ICandidateService? CandidateService { get; set; }
        [Inject] NotificationService? NotificationService { get; set; }
        [Inject] DialogService? DialogService { get; set; }
        protected Candidate? Model { get; set; }
        protected bool IsNew{ get; set; }
        protected bool IsLoaded { get; set; }
        public string? NumberFormat { get; private set; }
        public string? NumberPattern { get; private set; }

        private List<string> Errors= new List<string>();
        public NewOrEditCandidate()
        {
            this.Model = new Candidate();
            this.IsNew = true;
        }

        protected override async Task OnInitializedAsync()
        {
            this.IsLoaded = false;
            await Task.Delay(1);
            if (!string.IsNullOrWhiteSpace(EntityId))
            {
                this.IsNew = false;
                this.Model = new Candidate(await CandidateService.GetCandidate(EntityId));
            }
            var region = PhoneNumberUtil.GetInstance().GetRegionCodeForCountryCode(ApiUtils.PhoneCountryCode);
            NumberFormat = PhoneNumberUtil.GetInstance().Format(PhoneNumberUtil.GetInstance().GetExampleNumber(region), PhoneNumberFormat.INTERNATIONAL);
            NumberPattern = Regex.Replace(NumberFormat.Replace($"+{ApiUtils.PhoneCountryCode} ", ""), "[0-9]", "*");
            this.IsLoaded = true;
        }

        public async Task OnValidSubmit()
        {
            Errors.Clear();
            try
            {
                var result = IsNew ? (await CandidateService.AddCandidate(Model)) != null: await CandidateService.EditCandidate(Model);
                if(!result && IsNew)
                {
                    Errors.Add("Couldn't add a new customer");
                    return;
                }
                else if (!result && !IsNew)
                {
                    Errors.Add("Couldn't edit the customer");
                    return;
                }

                NotificationMessage notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Success", Detail = IsNew ? "New Candidate has been added" : "Candidate has been edited", Duration = 4000 };
                NotificationService.Notify(notificationMessage);
                OnFinishForm?.InvokeAsync(Model);
                DialogService.Close();
                
            }
            catch (Exception ex)
            {
                Errors.Add(ex.Message);
            }            
        }
    }
}
