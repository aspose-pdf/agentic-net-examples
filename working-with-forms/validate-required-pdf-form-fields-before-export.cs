using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputJson = "formData.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Validate that every required field has a non‑empty value
            bool allValid = true;
            foreach (Field field in doc.Form.Fields)
            {
                // Aspose.Pdf.Forms.Field exposes a 'Required' property (not IsRequired)
                if (field.Required)
                {
                    var value = field.Value;
                    if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        Console.Error.WriteLine($"Required field '{field.PartialName}' is empty.");
                        allValid = false;
                    }
                }
            }

            if (!allValid)
            {
                Console.Error.WriteLine("Form contains empty required fields. Export aborted.");
                return;
            }

            // Export the form data to JSON
            using (FileStream fs = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
            {
                doc.Form.ExportToJson(fs);
            }

            Console.WriteLine($"Form data exported to '{outputJson}'.");
        }
    }
}
