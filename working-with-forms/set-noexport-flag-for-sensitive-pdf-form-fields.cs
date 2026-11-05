using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Identify fields that are considered sensitive
                if (IsSensitive(field))
                {
                    // Prevent the field from being exported with form data
                    field.Exportable = false;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    // Simple heuristic to decide if a field is sensitive
    static bool IsSensitive(Field field)
    {
        string name = field?.PartialName?.ToLowerInvariant() ?? string.Empty;
        return name.Contains("ssn") ||
               name.Contains("password") ||
               name.Contains("secret") ||
               name.Contains("pin");
    }
}
