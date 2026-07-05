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

        // Page numbers (1‑based indexing)
        const int sourcePageNumber = 2;   // Page to take from source PDF
        const int insertPosition   = 3;   // Position in target PDF where the page will be inserted

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

        try
        {
            // Load both documents inside using blocks for deterministic disposal
            using (Document sourceDoc = new Document(sourcePdfPath))
            using (Document targetDoc = new Document(targetPdfPath))
            {
                // Ensure the requested page numbers exist
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

                // Retrieve the page from the source document
                Page pageToInsert = sourceDoc.Pages[sourcePageNumber];

                // Insert the page into the target document at the specified position
                // This overload inserts an existing Page instance into the collection
                targetDoc.Pages.Insert(insertPosition, pageToInsert);

                // Save the modified target document
                targetDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Page {sourcePageNumber} from '{sourcePdfPath}' inserted into '{targetPdfPath}' at position {insertPosition}.");
            Console.WriteLine($"Result saved as '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}