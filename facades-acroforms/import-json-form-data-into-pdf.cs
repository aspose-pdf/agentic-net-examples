using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string jsonFilePath = "data.json";
        const string outputPdfPath = "output.pdf";

        // Verify input files exist
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
            // Load the PDF document (deterministic disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize the Form facade with the loaded document
                using (Form form = new Form(pdfDoc))
                {
                    // Open the JSON file stream
                    using (FileStream jsonStream = new FileStream(jsonFilePath, FileMode.Open, FileAccess.Read))
                    {
                        // Import form field values from JSON
                        form.ImportJson(jsonStream);
                    }

                    // Save the modified PDF to the output path
                    form.Save(outputPdfPath);
                }
            }

            Console.WriteLine($"Form data imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during the import process
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}