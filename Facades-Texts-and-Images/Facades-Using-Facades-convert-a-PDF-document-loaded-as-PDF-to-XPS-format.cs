using System;
using System.IO;
using Aspose.Pdf;               // Core API for Document and XpsSaveOptions
using Aspose.Pdf.Facades;      // Included as per task requirement (not directly used)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXpsPath = "output.xps";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize XPS save options (required for non‑PDF output)
                XpsSaveOptions xpsOptions = new XpsSaveOptions();

                // Save the document as XPS (lifecycle rule: use Save with SaveOptions)
                pdfDocument.Save(outputXpsPath, xpsOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputXpsPath}'");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during conversion
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}