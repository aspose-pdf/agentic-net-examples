using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF containing the IP address form field
        const string inputPath = "input.pdf";
        // Name of the form field that should hold the IP address
        const string ipFieldName = "IPAddress";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form collection
            Form form = doc.Form;

            // Retrieve the field by name; cast to TextBoxField (generic text field)
            TextBoxField ipField = form[ipFieldName] as TextBoxField;
            if (ipField == null)
            {
                Console.Error.WriteLine($"Form field '{ipFieldName}' not found or is not a text box.");
                return;
            }

            // Get the current value of the field
            string ipValue = ipField.Value?.ToString() ?? string.Empty;

            // Regular expression for a valid IPv4 address
            string ipv4Pattern = @"^(25[0-5]|2[0-4]\d|[01]?\d\d?)\."
                               + @"(25[0-5]|2[0-4]\d|[01]?\d\d?)\."
                               + @"(25[0-5]|2[0-4]\d|[01]?\d\d?)\."
                               + @"(25[0-5]|2[0-4]\d|[01]?\d\d?)$";

            bool isValid = Regex.IsMatch(ipValue, ipv4Pattern);

            if (isValid)
            {
                Console.WriteLine($"IP address '{ipValue}' is valid.");
            }
            else
            {
                // Optionally, set the field border color to red to indicate error
                ipField.Color = Aspose.Pdf.Color.Red;

                // Throw an Aspose.Pdf exception to signal invalid format
                throw new InvalidValueFormatException($"The value '{ipValue}' is not a valid IPv4 address.");
            }

            // Save the document (if any visual changes were made)
            doc.Save("output.pdf");
        }
    }
}