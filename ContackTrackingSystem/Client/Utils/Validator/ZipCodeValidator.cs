using ContactTrackingSystem.Shared.Data;
using ContactTrackingSystem.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using PhoneNumbers;
using Radzen.Blazor;
using Radzen;
using System.Text.RegularExpressions;

namespace ContactTrackingSystem.Client.Utils.Validator
{
    /// <summary>
    /// Checks a given Residential Zip Core 
    /// </summary>
    public class ResidentialZipCodeValidator : ValidatorBase
    {
        [Parameter] public override string Text { get; set; } = "Residential zip code is invalid";
        protected override bool Validate(IRadzenFormComponent component)
        {
            var _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
            var _caZipRegEx = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";
            string zipCode = (string)component.GetValue();
            if (string.IsNullOrWhiteSpace(zipCode))
                return true;
            zipCode = zipCode.Trim().ToLower();
            var validZipCode = true;
            if (!Regex.Match(zipCode, _usZipRegEx).Success && !Regex.Match(zipCode, _caZipRegEx).Success)
            {
                validZipCode = false;
            }
            return validZipCode;
        }
    }
}
