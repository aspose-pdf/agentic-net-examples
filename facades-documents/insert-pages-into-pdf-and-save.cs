using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePdfPath      = "source.pdf";      // PDF whose page will be inserted
        const string destinationPdfPath = "destination.pdf"; // Original PDF to receive the page
        const string outputPdfPath      = "output.pdf";      // Resulting PDF after insertion

        // Verify that input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(destinationPdfPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdfPath}");
            return;
        }

        try
        {
            // Load both PDFs using the Document class (recommended over PdfFileEditor for page manipulation)
            Document srcDoc = new Document(sourcePdfPath);
            Document destDoc = new Document(destinationPdfPath);

            // Ensure the source PDF actually has the requested page
            if (srcDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Source PDF does not contain page 1.");
                return;
            }

            // Insert page 1 from the source PDF after page 2 of the destination PDF.
            // Pages.Insert takes the position where the new page will appear (1‑based index).
            // After page 2 means the new page should be placed at position 3.
            Page pageToInsert = srcDoc.Pages[1];
            destDoc.Pages.Insert(3, pageToInsert);

            // Save the modified document to the desired output path.
            destDoc.Save(outputPdfPath);

            Console.WriteLine($"Page inserted successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}
