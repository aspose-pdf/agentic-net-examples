using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class PdfToPptxConverter
{
    static void Main()
    {
        // Paths to the source PDF and the destination PPTX file.
        const string pdfPath  = "input.pdf";
        const string pptxPath = "output.pptx";

        // Verify that the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize save options for PPTX conversion.
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Set SlidesAsImages to true so each slide contains only an image
            // (one image per PDF page).
            pptxOptions.SlidesAsImages = true;

            // Save the PDF as a PPTX file using the configured options.
            pdfDocument.Save(pptxPath, pptxOptions);
        }

        Console.WriteLine($"Conversion completed: '{pptxPath}'");
    }
}