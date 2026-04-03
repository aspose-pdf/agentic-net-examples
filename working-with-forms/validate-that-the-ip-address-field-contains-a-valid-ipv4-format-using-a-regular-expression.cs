using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // PDF containing a form field named "IPAddress"
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to Aspose.Pdf.Forms.Field
            Field field = doc.Form["IPAddress"] as Field;
            if (field == null)
            {
                Console.Error.WriteLine("Form field 'IPAddress' not found or is not a form field.");
                return;
            }

            // Obtain the current value of the field
            string ipValue = field.Value?.ToString() ?? string.Empty;

            // Regular expression for basic IPv4 structure (four groups of 1-3 digits separated by dots)
            const string ipv4Pattern = @"^(?:\d{1,3}\.){3}\d{1,3}$";

            bool isValid = false;
            if (Regex.IsMatch(ipValue, ipv4Pattern))
            {
                // Further validate each octet is within 0‑255
                string[] octets = ipValue.Split('.');
                isValid = true;
                foreach (string octet in octets)
                {
                    if (!int.TryParse(octet, out int value) || value < 0 || value > 255)
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            // Change field background colour to indicate validity (TextBoxField is the typical type for an IP address)
            if (field is TextBoxField txtField)
            {
                txtField.Color = isValid ? Aspose.Pdf.Color.LightGreen : Aspose.Pdf.Color.LightCoral;
            }
            else if (field is NumberField numField) // fallback if the field is numeric
            {
                numField.Color = isValid ? Aspose.Pdf.Color.LightGreen : Aspose.Pdf.Color.LightCoral;
            }

            Console.WriteLine(isValid
                ? $"IP address '{ipValue}' is valid."
                : $"IP address '{ipValue}' is invalid.");

            // Save the modified document (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
