using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string sourcePdfPath = "source.pdf";   // PDF containing the page to insert
        const string targetPdfPath = "target.pdf";   // PDF into which the page will be inserted
        const string outputPdfPath = "merged.pdf";   // Resulting PDF

        // Page numbers are 1‑based in Aspose.Pdf
        const int sourcePageNumber = 1;   // Page to take from source PDF
        const int insertPosition   = 2;   // Position in target PDF where the page will be inserted

        // Validate input files
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdfPath}");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document targetDoc = new Document(targetPdfPath))
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Retrieve the page from the source document
            Page pageToInsert = sourceDoc.Pages[sourcePageNumber];

            // Insert the page into the target document at the specified position
            // Insert overload copies the page into the target collection
            targetDoc.Pages.Insert(insertPosition, pageToInsert);

            // Save the modified target document to the output path
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Page {sourcePageNumber} from '{sourcePdfPath}' inserted at position {insertPosition} in '{targetPdfPath}'.");
        Console.WriteLine($"Result saved to '{outputPdfPath}'.");
    }
}