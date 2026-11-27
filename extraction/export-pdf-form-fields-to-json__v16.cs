using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "FormH.pdf";
        const string outputPath = "FormH_fields.json"; // JSON export of form data

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Enumerate all form fields and display their names and values
                Console.WriteLine("Form fields in the document:");
                foreach (var field in doc.Form.Fields)
                {
                    string name = field.PartialName;
                    string value = field.Value?.ToString() ?? string.Empty;
                    Console.WriteLine($"- {name}: {value}");
                }

                // Export the complete form data to a JSON file (Aspose.Pdf does not provide ExportToXfdf on Form)
                using (FileStream jsonStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    doc.Form.ExportToJson(jsonStream);
                }
                Console.WriteLine($"Form data exported to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
