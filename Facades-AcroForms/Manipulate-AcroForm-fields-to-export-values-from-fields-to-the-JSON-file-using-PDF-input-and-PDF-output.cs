using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "filled_output.pdf";
        const string jsonPath  = "fields.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade with the source PDF
        Form form = new Form(inputPdf);

        // Example manipulation: fill fields based on simple heuristics
        foreach (string fieldName in form.FieldNames)
        {
            // If the field name suggests a text field for a name, fill with a placeholder
            if (fieldName.IndexOf("Name", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                form.FillField(fieldName, "John Doe");
            }
            // If the field name suggests a checkbox, set it to true
            else if (fieldName.IndexOf("Check", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                form.FillField(fieldName, true);
            }
        }

        // Export all form field values to a JSON file
        using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
        {
            // indented = true for readable output
            form.ExportJson(jsonStream, true);
        }

        // Save the modified PDF to a new file
        form.Save(outputPdf);

        // Release resources held by the Form facade
        form.Dispose();

        Console.WriteLine($"Form fields exported to '{jsonPath}'. Modified PDF saved as '{outputPdf}'.");
    }
}