using System.ComponentModel.DataAnnotations;

namespace Api.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RussianPhoneAttribute : ValidationAttribute
    {
        private const int expectedPhoneLength = 11;
        private const char firstDigitInPhone = '7';

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            if (!(value is string valueAsString) || expectedPhoneLength != valueAsString.Length || valueAsString[0] != firstDigitInPhone)
            {
                return false;
            }

            for ( var i = 1; i < valueAsString.Length; i++)
            {
                if (!char.IsDigit(valueAsString[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
