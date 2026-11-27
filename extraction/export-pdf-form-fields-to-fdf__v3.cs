using System;
using System.IO;
using Aspose.Pdf;

class ExportFormFieldsToFdf
{
    static void Main()
    {
        const string inputPdf = "FormC.pdf";
        const string outputFdf = "FormC_fields.fdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Enumerate all form fields and write their names and values to the console
            Console.WriteLine("Form fields in the document:");
            foreach (var field in doc.Form.Fields)
            {
                // field.FullName gives the hierarchical name of the field
                // field.Value contains the current value (may be null)
                string value = field.Value?.ToString() ?? "(null)";
                Console.WriteLine($"{field.FullName}: {value}");
            }

            // Export the form data to an FDF file.
            // The overload that takes only the file name infers the format from the extension.
            // This works even when the SaveFormat.Fdf enum value is not present in older Aspose.Pdf versions.
            doc.Save(outputFdf);
        }

        Console.WriteLine($"Form data exported to '{outputFdf}'.");
    }
}
