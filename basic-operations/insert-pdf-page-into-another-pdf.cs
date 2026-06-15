using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string targetPdfPath = "target.pdf";   // PDF into which the page will be inserted
        const string sourcePdfPath = "source.pdf";   // PDF containing the page to insert
        const string outputPdfPath = "merged.pdf";   // Resulting PDF

        // Validate input files
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

        // 1‑based page numbers (Aspose.Pdf uses 1‑based indexing)
        int sourcePageNumber = 1;   // page from source PDF to insert
        int insertPosition   = 2;   // position in target PDF where the page will be placed

        try
        {
            // Load both documents inside using blocks for deterministic disposal
            using (Document targetDoc = new Document(targetPdfPath))
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                // Ensure the requested pages exist
                if (sourcePageNumber < 1 || sourcePageNumber > sourceDoc.Pages.Count)
                {
                    Console.Error.WriteLine("Source page number out of range.");
                    return;
                }
                if (insertPosition < 1 || insertPosition > targetDoc.Pages.Count + 1)
                {
                    Console.Error.WriteLine("Insert position out of range.");
                    return;
                }

                // Retrieve the page from the source document
                Page pageToInsert = sourceDoc.Pages[sourcePageNumber];

                // Insert the page into the target document at the specified position
                // This overload inserts an existing Page object.
                targetDoc.Pages.Insert(insertPosition, pageToInsert);

                // Save the modified document
                targetDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Page {sourcePageNumber} from '{sourcePdfPath}' inserted at position {insertPosition} in '{targetPdfPath}'.");
            Console.WriteLine($"Result saved as '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}