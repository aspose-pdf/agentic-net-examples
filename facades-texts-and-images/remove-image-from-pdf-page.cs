using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the resulting PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Page number (1‑based) and the image object ID to be removed
        const int pageNumber    = 4;
        const int imageObjectId = 12345; // TODO: replace with the actual object ID

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core Document class (PdfFileEditor does not expose RemoveImage).
        Document pdfDoc;
        try
        {
            pdfDoc = new Document(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load PDF: {ex.Message}");
            return;
        }

        // Validate the requested page number.
        if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
        {
            Console.Error.WriteLine($"Invalid page number {pageNumber}. Document has {pdfDoc.Pages.Count} pages.");
            return;
        }

        // Attempt to delete the image resource from the specified page.
        Page page = pdfDoc.Pages[pageNumber];
        try
        {
            // ImageCollection provides a Delete method that accepts the object ID.
            page.Resources.Images.Delete(imageObjectId);
            Console.WriteLine($"Removed image (Object ID: {imageObjectId}) from page {pageNumber}.");
        }
        catch (Exception ex)
        {
            // If the object ID does not exist, Delete throws an exception.
            Console.Error.WriteLine($"Could not remove image with Object ID {imageObjectId} on page {pageNumber}: {ex.Message}");
            // Continue – we still want to save the document (unchanged) if desired.
        }

        // Save the modified PDF.
        try
        {
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Result saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save PDF: {ex.Message}");
        }
    }
}
