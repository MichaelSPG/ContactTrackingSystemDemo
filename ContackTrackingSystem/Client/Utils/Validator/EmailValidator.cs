using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;
using System.Text.RegularExpressions;

namespace ContactTrackingSystem.Client.Utils.Validator
{
    /// <summary>
    /// Checks if a given email is valid with regex matching
    /// </summary>
    public class EmailValidator : ValidatorBase
    {
        /// <summary>
        /// Gets or sets the message displayed when the component is invalid. Set to <c>"E-mail has not a valid format"</c> by default.
        /// </summary>
        [Parameter]
        public override string Text { get; set; } = "E-mail has not a valid format";
        /// <inheritdoc />
        protected override bool Validate(IRadzenFormComponent component)
        {
            string email = (string)component.GetValue();
            if (string.IsNullOrWhiteSpace(email))
                return true;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match m = regex.Match(email);
            return m.Success;
        }
    }
}
