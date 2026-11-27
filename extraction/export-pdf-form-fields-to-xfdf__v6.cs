using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains Document, Form)

class ExportFormFields
{
    static void Main()
    {
        const string inputPath  = "FormE.pdf";
        // Use .xfdf extension; Aspose.Pdf infers the format from the file name.
        const string outputPath = "FormE_fields.xfdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Enumerate all form fields and output their names and values
            foreach (var field in doc.Form.Fields)
            {
                // field.FullName gives the hierarchical name of the field
                // field.Value contains the current value (string, bool, etc.)
                Console.WriteLine($"{field.FullName}: {field.Value}");
            }

            // Export the form data. The overload that takes only the file name
            // infers the required format from the file extension, avoiding the
            // missing SaveFormat.Fdf enum value in older Aspose.Pdf versions.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form data exported to '{outputPath}'.");
    }
}
