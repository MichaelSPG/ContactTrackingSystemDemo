using ContactTrackingSystem.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;

namespace ContactTrackingSystem.Client.Utils.Validator
{
    /// <summary>
    /// Checks that a given email is used once
    /// </summary>
    public class UniqueEmailValidator : ValidatorBase
    {
        [Parameter]
        public Guid? EntityId { get; set; }
        [Inject] ICandidateService? CandidateService { get; set; }
        [Parameter]
        public override string Text { get; set; } = "E-mail is already taken";
        /// <inheritdoc />
        protected override bool Validate(IRadzenFormComponent component)
        {
            string email = (string)component.GetValue();
            if (string.IsNullOrWhiteSpace(email))
                return true;
            email = email.Trim().ToLower();
            var list = CandidateService?.GetAllCandidates().Result;
            var result = list?.Find(x => x?.Email?.ToLower() == email.ToLower());

            return result == null ? true : result.Id.Equals(EntityId);
        }
    }
}
