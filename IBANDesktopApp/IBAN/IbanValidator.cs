using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace IBANDesktopApp.IBAN
{
    public class IbanValidator : IIbanValidator
    {
        private static readonly IDictionary<string, int> CountryCodeLenghts =
            new Dictionary<string, int>
            {
                {"AE", 23},
                {"AL", 28},
                {"AD", 24},
                {"AT", 20},
                {"AZ", 28},
                {"BE", 16},
                {"BH", 22},
                {"BA", 20},
                {"BR", 29},
                {"BG", 22},
                {"BY", 28},
                {"CR", 21},
                {"HR", 21},
                {"CY", 28},
                {"CZ", 24},
                {"DK", 18},
                {"DO", 28},
                {"EE", 20},
                {"FO", 18},
                {"FI", 18},
                {"FR", 27},
                {"GE", 22},
                {"DE", 22},
                {"GI", 23},
                {"GR", 27},
                {"GL", 18},
                {"GT", 28},
                {"HU", 28},
                {"IS", 26},
                {"IE", 22},
                {"IL", 23},
                {"IT", 27},
                {"KZ", 20},
                {"KW", 30},
                {"LV", 21},
                {"LB", 28},
                {"LI", 21},
                {"LT", 20},
                {"LU", 20},
                {"MK", 19},
                {"MT", 31},
                {"MR", 27},
                {"MU", 30},
                {"MC", 27},
                {"MD", 24},
                {"ME", 22},
                {"NL", 18},
                {"NO", 15},
                {"PK", 24},
                {"PS", 29},
                {"PL", 28},
                {"PT", 25},
                {"RO", 24},
                {"SM", 27},
                {"SA", 24},
                {"RS", 22},
                {"SK", 24},
                {"SI", 19},
                {"ES", 24},
                {"SE", 24},
                {"CH", 21},
                {"TN", 24},
                {"TR", 26},
                {"GB", 22},
                {"VA", 22},
                {"VG", 24}
            };

        public bool ValidateIban(string ibanCode)
        {
            // checks if correct string
            if (String.IsNullOrWhiteSpace(ibanCode))
                return false;

            if (ibanCode.Length < 2)
                return false;

            ibanCode = ibanCode.ToUpper();

            var countryCode = ibanCode.Substring(0, 2);

            // checks if country code exists
            if (!CountryCodeLenghts.ContainsKey(countryCode))
                return false;

            // checks if iban is correct length for a country
            if (ibanCode.Length != CountryCodeLenghts[countryCode])
                return false;

            // puts first 4 chars to a string ending
            ibanCode = ibanCode.Substring(4) + ibanCode.Substring(0, 4);

            var generatedIbanCode = "";
            foreach (var letter in ibanCode)
            {
                if (Char.IsLetter(letter))
                {
                    var tempLetterInt = (int)(((int)letter) - 55);
                    generatedIbanCode += tempLetterInt;
                }
                else if (Char.IsNumber(letter))
                {
                    generatedIbanCode += int.Parse(letter.ToString());
                }
                else
                {
                    return false;
                }
            }

            if (BigInteger.Parse(generatedIbanCode) % 97 != 1)
                return false;

            return true;
        }
    }
}
