using ContactTrackingSystem.Shared.Data;
using Microsoft.AspNetCore.Components;
using PhoneNumbers;
using Radzen.Blazor;
using Radzen;

namespace ContactTrackingSystem.Client.Utils.Validator
{
    /// <summary>
    /// Checks if a given phone number is valid with PhoneNumberUtil for a region
    /// </summary>
    public class PhoneValidator : ValidatorBase
    {
        private readonly PhoneNumberUtil _phoneUtil;

        public PhoneValidator()
        {
            _phoneUtil = PhoneNumberUtil.GetInstance();
        }

        [Parameter]
        public override string Text { get; set; } = "Phone number is already taken";
        /// <inheritdoc />
        protected override bool Validate(IRadzenFormComponent component)
        {
            string phone = (string)component.GetValue();

            if (string.IsNullOrWhiteSpace(phone))
                return true;
            phone = phone.Trim().Replace("(", "").Replace(")", "").Replace(" ", "");
            var phoneNumber = _phoneUtil.Parse(phone, _phoneUtil.GetRegionCodeForCountryCode(ApiUtils.PhoneCountryCode));
            return _phoneUtil.IsValidNumberForRegion(phoneNumber, _phoneUtil.GetRegionCodeForCountryCode(ApiUtils.PhoneCountryCode));
        }
    }
}
