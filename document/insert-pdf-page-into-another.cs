using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string targetPdfPath = "target.pdf";   // PDF that will receive the new page
        const string sourcePdfPath = "source.pdf";   // PDF that provides the page to insert
        const string outputPdfPath = "merged.pdf";   // Resulting PDF

        // Page numbers are 1‑based in Aspose.Pdf
        const int sourcePageNumber = 2;   // Page from source PDF to insert
        const int insertPosition   = 3;   // Position in target PDF where the page will be inserted

        // Ensure input files exist
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document targetDoc = new Document(targetPdfPath))
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Validate requested page numbers
            if (sourcePageNumber < 1 || sourcePageNumber > sourceDoc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid source page number.");
                return;
            }
            if (insertPosition < 1 || insertPosition > targetDoc.Pages.Count + 1)
            {
                Console.Error.WriteLine("Invalid insert position.");
                return;
            }

            // Retrieve the page to be inserted from the source document
            Page pageToInsert = sourceDoc.Pages[sourcePageNumber];

            // Insert the page into the target document at the specified position
            // This uses the Insert(int, Page) overload of PageCollection
            targetDoc.Pages.Insert(insertPosition, pageToInsert);

            // Save the modified target document
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Page {sourcePageNumber} from '{sourcePdfPath}' inserted into '{targetPdfPath}' at position {insertPosition}.");
        Console.WriteLine($"Result saved as '{outputPdfPath}'.");
    }
}