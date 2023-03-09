using ContactTrackingSystem.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;

namespace ContactTrackingSystem.Client.Utils.Validator
{   /// <summary>
    /// Checks that a given phone is used once
    /// </summary>
    public class UniquePhoneValidator : ValidatorBase
    {
        [Parameter]
        public Guid? EntityId { get; set; }
        [Inject] ICandidateService CandidateService { get; set; }
        [Parameter]
        public override string Text { get; set; } = "Phone number is already taken";
        /// <inheritdoc />
        protected override bool Validate(IRadzenFormComponent component)
        {
            string phoneNumber = (string)component.GetValue();
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return true;
            phoneNumber = phoneNumber.Trim().Replace("(", "").Replace(")", "").Replace(" ", "");
            var result = CandidateService.GetAllCandidates().Result.ToList().Find(x => x?.PhoneNumber == phoneNumber);
            return result == null || result.Id == EntityId;
        }
    }
}
