using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string jsonFilePath  = "data.json";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(jsonFilePath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonFilePath}");
            return;
        }

        try
        {
            // Initialize the Form facade with the source PDF
            using (Form form = new Form(inputPdfPath))
            {
                // Open the JSON file as a stream and import the data
                using (FileStream jsonStream = new FileStream(jsonFilePath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportJson(jsonStream);
                }

                // Optional: inspect import results for each field
                var importResults = form.ImportResult;
                if (importResults != null)
                {
                    foreach (var result in importResults)
                    {
                        // result is of type FormImportResult; display field name and status
                        Console.WriteLine($"Field: {result.FieldName}, Status: {result.Status}");
                    }
                }

                // Save the updated PDF to the output path
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during import: {ex.Message}");
        }
    }
}