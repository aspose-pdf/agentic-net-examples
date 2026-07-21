using System;
using System.IO;
using Aspose.Pdf;               // Core API for PDF manipulation
using Aspose.Pdf.Facades;      // Not needed here but kept for completeness

class Program
{
    static void Main()
    {
        // Input files
        const string sourcePdfPath = "source.pdf";   // PDF containing the page to insert
        const string targetPdfPath = "target.pdf";   // PDF into which the page will be inserted
        const string outputPdfPath = "merged.pdf";   // Resulting PDF

        // Page numbers (1‑based indexing as required by Aspose.Pdf)
        const int sourcePageNumber = 2;   // Page from sourcePdfPath to copy
        const int insertPosition   = 3;   // Position in targetPdfPath where the page will be inserted

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
            // Load the source document (the page we want to copy)
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                // Ensure the requested source page exists
                if (sourcePageNumber < 1 || sourcePageNumber > sourceDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Source page number {sourcePageNumber} is out of range.");
                    return;
                }

                // Load the target document (the document to receive the new page)
                using (Document targetDoc = new Document(targetPdfPath))
                {
                    // Ensure the insert position is valid (it can be Count+1 to append at the end)
                    if (insertPosition < 1 || insertPosition > targetDoc.Pages.Count + 1)
                    {
                        Console.Error.WriteLine($"Insert position {insertPosition} is out of range.");
                        return;
                    }

                    // Retrieve the page from the source document
                    Page pageToInsert = sourceDoc.Pages[sourcePageNumber];

                    // Insert the page into the target document at the specified position
                    // This overload copies the page into the target collection.
                    targetDoc.Pages.Insert(insertPosition, pageToInsert);

                    // Save the modified target document
                    targetDoc.Save(outputPdfPath);
                }
            }

            Console.WriteLine($"Page {sourcePageNumber} from '{sourcePdfPath}' inserted at position {insertPosition} in '{targetPdfPath}'.");
            Console.WriteLine($"Result saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}