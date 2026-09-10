using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Name of the form field that should contain the IP address
            const string fieldName = "IPAddress";

            // Ensure the form field exists
            Form form = doc.Form;
            if (!form.HasField(fieldName))
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found.");
                return;
            }

            // Retrieve the field as a TextBoxField (the typical type for free‑form text)
            TextBoxField ipField = form[fieldName] as TextBoxField;
            if (ipField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a text box.");
                return;
            }

            // Get the current value of the field (may be null)
            string ipValue = ipField.Value?.ToString() ?? string.Empty;

            // Regular expression that matches a valid IPv4 address
            const string ipv4Pattern = @"^(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)){3}$";

            // Validate the field value against the pattern
            bool isValid = Regex.IsMatch(ipValue, ipv4Pattern);

            if (isValid)
            {
                Console.WriteLine($"Valid IPv4 address: '{ipValue}'.");
            }
            else
            {
                Console.WriteLine($"Invalid IPv4 address: '{ipValue}'.");
                // Optional: mark the field visually (e.g., change background color)
                // ipField.Color = Aspose.Pdf.Color.Red;
            }

            // Save the (potentially modified) document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}