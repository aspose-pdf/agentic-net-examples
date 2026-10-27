using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the edited output PDF
        const string inputPath  = "source.pdf";
        const string outputPath = "edited.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document using the Document class (the core API)
            Document pdfDocument = new Document(inputPath);

            // 1. Delete the first page (if the document has at least one page)
            if (pdfDocument.Pages.Count >= 1)
                pdfDocument.Pages.Delete(1);

            // 2. Rotate the (new) second page 90 degrees clockwise (if it exists)
            //    After a possible deletion, the page index is still 1‑based.
            if (pdfDocument.Pages.Count >= 2)
                pdfDocument.Pages[2].Rotate = Rotation.on90; // clockwise 90°

            // Save the modified PDF to a new file
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
