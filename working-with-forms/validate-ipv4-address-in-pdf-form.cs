using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for Border class

class Program
{
    static void Main()
    {
        const string inputPdf = "form_with_ip.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using the mandatory lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // Access the form object
            Form form = doc.Form;

            // Retrieve the IP address field by its name (replace "IpAddress" with the actual field name)
            // The indexer returns a WidgetAnnotation; cast it to Field for form‑field operations.
            Field ipField = form["IpAddress"] as Field;
            if (ipField == null)
            {
                Console.Error.WriteLine("IP address field not found in the PDF form.");
                return;
            }

            // The field value is stored as a string
            string ipValue = ipField.Value?.ToString() ?? string.Empty;

            // Regular expression that matches a valid IPv4 address (0.0.0.0 to 255.255.255.255)
            const string ipv4Pattern = @"^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$";

            bool isValid = Regex.IsMatch(ipValue, ipv4Pattern);

            if (isValid)
            {
                Console.WriteLine($"The IP address '{ipValue}' is valid.");
            }
            else
            {
                Console.WriteLine($"The IP address '{ipValue}' is INVALID.");

                // Highlight the field with a red border to indicate validation failure
                ipField.Border = new Border(ipField) { Width = 2 };
                ipField.Color = Aspose.Pdf.Color.Red;

                // Save the modified PDF (optional)
                const string outputPdf = "form_with_ip_validated.pdf";
                doc.Save(outputPdf);
                Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
            }
        }
    }
}
