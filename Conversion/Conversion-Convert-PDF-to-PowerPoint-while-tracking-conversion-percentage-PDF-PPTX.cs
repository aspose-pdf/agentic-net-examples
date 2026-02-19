using System;
using System.IO;
using Aspose.Pdf;

class PdfToPptxConverter
{
    static void Main(string[] args)
    {
        // Input PDF path and output PPTX path can be passed as arguments or hard‑coded.
        string pdfPath = args.Length > 0 ? args[0] : "input.pdf";
        string pptxPath = args.Length > 1 ? args[1] : "output.pptx";

        // Verify that the source PDF file exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(pdfPath);

            // Total number of pages – used for a simple progress display.
            int totalPages = pdfDocument.Pages.Count;
            Console.WriteLine($"Converting PDF to PPTX: {totalPages} page(s) to process.");

            // Simple progress simulation: report start (0%) and end (100%).
            // Real conversion is performed by the Save method in one step.
            Console.WriteLine("Progress: 0%");

            // Save the document as PPTX. The file extension determines the format.
            // The lifecycle rule 'document-save' is used here.
            pdfDocument.Save(pptxPath);

            Console.WriteLine("Progress: 100%");
            Console.WriteLine($"Conversion completed successfully. PPTX saved to '{pptxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}