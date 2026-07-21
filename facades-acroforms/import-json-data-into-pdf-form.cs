using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string jsonFilePath  = "data.json";
        const string outputPdfPath = "output.pdf";

        // Verify that the required files exist before proceeding.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{inputPdfPath}'.");
            return;
        }

        if (!File.Exists(jsonFilePath))
        {
            Console.Error.WriteLine($"Error: JSON file not found – '{jsonFilePath}'.");
            return;
        }

        try
        {
            // Load the PDF form using the Facades Form class.
            using (Form form = new Form(inputPdfPath))
            {
                // Open the JSON file as a read‑only stream.
                using (FileStream jsonStream = new FileStream(jsonFilePath, FileMode.Open, FileAccess.Read))
                {
                    // Import all field values from the JSON stream.
                    form.ImportJson(jsonStream);
                }

                // Save the updated PDF to the desired output location.
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data successfully imported and saved to '{outputPdfPath}'.");
        }
        catch (PdfException ex)
        {
            // Handles errors specific to Aspose.Pdf processing.
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handles any other unexpected errors.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}