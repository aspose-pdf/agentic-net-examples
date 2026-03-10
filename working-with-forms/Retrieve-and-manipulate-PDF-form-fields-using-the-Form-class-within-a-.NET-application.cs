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
        const string jsonPath = "fields.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (wrapped in using for proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Access the AcroForm of the document
                Form form = doc.Form;

                // List all form fields with their current values
                Console.WriteLine($"Form contains {form.Count} fields.");
                foreach (Field field in form.Fields)
                {
                    Console.WriteLine($"Field Name: {field.FullName}, Value: {field.Value}");
                }

                // Example: set the value of a specific field if it exists
                const string targetFieldName = "Name";
                if (form.HasField(targetFieldName))
                {
                    // The indexer returns a WidgetAnnotation; cast it to Field safely.
                    Field? targetField = form[targetFieldName] as Field;
                    if (targetField != null)
                    {
                        targetField.Value = "John Doe";
                        Console.WriteLine($"Set field '{targetFieldName}' to 'John Doe'.");
                    }
                }

                // Export all form fields to a JSON file
                using (FileStream jsonExport = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportToJson(jsonExport, new ExportFieldsToJsonOptions());
                }
                Console.WriteLine($"Form fields exported to JSON: {jsonPath}");

                // Import form field values from the same JSON file (demonstration)
                using (FileStream jsonImport = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportFromJson(jsonImport);
                }
                Console.WriteLine("Form fields imported from JSON.");

                // Flatten the form – remove interactive fields and place their values directly on the page
                form.Flatten();
                Console.WriteLine("Form flattened.");

                // Save the modified PDF document
                doc.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
