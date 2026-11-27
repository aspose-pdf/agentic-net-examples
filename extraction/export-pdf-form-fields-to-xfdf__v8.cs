using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "FormF.pdf";
        const string outputPath = "FormF_fields.xfdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document containing the form
            using (Document doc = new Document(inputPath))
            {
                // Enumerate all form fields and display their names and values
                foreach (Field field in doc.Form)
                {
                    string fieldName = field.FullName;
                    string fieldValue = field.Value?.ToString() ?? string.Empty;
                    Console.WriteLine($"Field: {fieldName}, Value: {fieldValue}");
                }

                // Export all form field data to an XFDF file using the core API
                using (FileStream xfdfStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    doc.ExportAnnotationsToXfdf(xfdfStream);
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
