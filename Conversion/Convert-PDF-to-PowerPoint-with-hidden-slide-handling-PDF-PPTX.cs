using System;
using System.IO;
using Aspose.Pdf;

class PdfToPptxConverter
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output PPTX file path
        const string pptxPath = "output.pptx";

        // Verify input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create + load)
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Initialize PPTX save options
                PptxSaveOptions saveOptions = new PptxSaveOptions();

                // NOTE: Aspose.Pdf does not expose a direct API for "hidden slide" handling.
                // The conversion creates one slide per PDF page. If you need to hide
                // specific slides after conversion, you would have to manipulate the
                // resulting PPTX with a presentation library (e.g., Aspose.Slides).

                // Save the PDF as PPTX using the specified options (lifecycle: save)
                pdfDocument.Save(pptxPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: '{pptxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}