using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API (Document, PageCollection, etc.)
using Aspose.Pdf.Tagged;               // For tagged‑content handling (if needed)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // Existing PDF
        const string outputPath = "output_with_blank.pdf"; // Resulting PDF
        const int  insertAt     = 3;                    // Position (1‑based) where blank page will be inserted

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Insert an empty page at the desired position.
            // PageCollection.Insert respects 1‑based indexing.
            doc.Pages.Insert(insertAt);

            // If the document contains pagination artifacts (header/footer page numbers),
            // update them so that the new page is reflected in the numbering.
            // This is an extension method defined in Aspose.Pdf.PageCollectionExtensions.
            doc.Pages.UpdatePagination();

            // Save the modified document. No SaveOptions are required because the
            // output format is PDF (the default).
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page inserted. Output saved to '{outputPath}'.");
    }
}