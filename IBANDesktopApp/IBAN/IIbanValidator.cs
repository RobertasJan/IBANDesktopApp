using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBANDesktopApp.IBAN
{
    public interface IIbanValidator
    {
        /// <summary>
        /// Method checks correctness of provided IBAN code.
        /// </summary>
        /// <param name="ibanCode">IBAN code to parse</param>
        /// <returns>Checks correctness of provided IBAN code. Returns true for correct code, false for incorrect.</returns>
        bool ValidateIban(string ibanCode);
    }
}
