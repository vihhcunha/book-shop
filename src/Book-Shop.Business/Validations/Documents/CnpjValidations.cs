using Book_Shop.Business.Extensions;

namespace Book_Shop.Business.Validations.Documents
{
    internal class CnpjValidations
    {
        public const int CnpjSize = 14;

        public static bool Validate(string cpnj)
        {
            var cnpjNumbers = cpnj.JustNumbers();

            if (!HasValidSize(cnpjNumbers)) return false;
            return !HaveRepeatedNumbers(cnpjNumbers) && HaveValidDigits(cnpjNumbers);
        }

        private static bool HasValidSize(string value)
        {
            return value.Length == CnpjSize;
        }

        private static bool HaveRepeatedNumbers(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HaveValidDigits(string value)
        {
            var number = value.Substring(0, CnpjSize - 2);

            var verifyierDigit = new VerifyingDigit(number)
                .WithMultipliersUntil(2, 9)
                .Replacing("0", 10, 11);
            var firstDigit = verifyierDigit.CalcDigit();
            verifyierDigit.AddDigit(firstDigit);
            var secondDigit = verifyierDigit.CalcDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(CnpjSize - 2, 2);
        }
    }
}
