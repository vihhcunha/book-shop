using Book_Shop.Business.Extensions;

namespace Book_Shop.Business.Validations.Documents
{
    internal class CpfValidations
    {
        public const int CpfSize = 11;

        public static bool Validate(string cpf)
        {
            var cpfNumbers = cpf.JustNumbers();

            if (!ValidSize(cpfNumbers)) return false;
            return !HaveRepeatedDigits(cpfNumbers) && HaveValidDigits(cpfNumbers);
        }

        private static bool ValidSize(string value)
        {
            return value.Length == CpfSize;
        }

        private static bool HaveRepeatedDigits(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HaveValidDigits(string value)
        {
            var number = value.Substring(0, CpfSize - 2);
            var verifyingDigit = new VerifyingDigit(number)
                .WithMultipliersUntil(2, 11)
                .Replacing("0", 10, 11);
            var firstDigit = verifyingDigit.CalcDigit();
            verifyingDigit.AddDigit(firstDigit);
            var secondDigit = verifyingDigit.CalcDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(CpfSize - 2, 2);
        }
    }
}
