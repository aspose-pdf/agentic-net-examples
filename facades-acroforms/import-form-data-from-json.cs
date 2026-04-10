using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF with form fields
        const string jsonFilePath  = "data.json";   // JSON file containing field values
        const string outputPdfPath = "output.pdf";  // destination PDF after import

        // Verify that required files exist before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonFilePath))
        {
            Console.Error.WriteLine($"Error: JSON file not found – {jsonFilePath}");
            return;
        }

        try
        {
            // Create the Form facade, specifying input and output PDF files.
            // The constructor binds the source PDF and prepares the target file.
            using (Form form = new Form(inputPdfPath, outputPdfPath))
            {
                // Open the JSON file as a read‑only stream.
                using (FileStream jsonStream = new FileStream(jsonFilePath, FileMode.Open, FileAccess.Read))
                {
                    // Import all form field data from the JSON stream.
                    form.ImportJson(jsonStream);
                }

                // Persist the changes to the output PDF.
                form.Save();
            }

            Console.WriteLine($"Form data successfully imported. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during import or saving.
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}