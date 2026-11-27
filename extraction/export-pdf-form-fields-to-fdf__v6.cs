using System;
using System.IO;
using Aspose.Pdf;

class ExportFormFieldsToFdf
{
    static void Main()
    {
        const string inputPdf = "FormH.pdf";
        const string outputFdf = "FormH_fields.fdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Enumerate all form fields and output their names/values
            Console.WriteLine("Form fields in the document:");
            foreach (var field in doc.Form.Fields)
            {
                // field.FullName gives the hierarchical name, field.Value holds the current data
                Console.WriteLine($"{field.FullName}: {field.Value}");
            }

            // Export the entire form data to an FDF file.
            // The SaveFormat enum does not contain an Fdf member in older Aspose.Pdf versions.
            // Using the overload that takes only the file name lets the library infer the format from the .fdf extension.
            doc.Save(outputFdf);
        }

        Console.WriteLine($"Form data exported to: {outputFdf}");
    }
}
