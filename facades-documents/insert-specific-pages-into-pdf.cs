using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // Facades namespace (required by task)

// Insert selected pages from a source PDF stream into a destination PDF at a specific position.
class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files (replace with actual paths or streams as needed)
        const string destinationPath = "destination.pdf";
        const string sourcePath      = "source.pdf";
        const string outputPath      = "merged_output.pdf";

        // Verify that both input files exist
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"File not found: {destinationPath}");
            return;
        }
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        // Define which pages (1‑based) from the source PDF should be inserted
        // Example: insert pages 2, 4 and 5
        int[] pagesToInsert = new int[] { 2, 4, 5 };

        // Define the position in the destination PDF where the first page will be inserted.
        // The new pages will appear before the page with this number (1‑based).
        // For example, insertPosition = 3 will place the first inserted page before page 3.
        int insertPosition = 3;

        try
        {
            // Open the destination PDF as a stream
            using (FileStream destStream = new FileStream(destinationPath, FileMode.Open, FileAccess.ReadWrite))
            // Open the source PDF as a stream
            using (FileStream srcStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            // Load both PDFs into Aspose.Pdf.Document objects (wrapped in using for deterministic disposal)
            using (Aspose.Pdf.Document destDoc = new Aspose.Pdf.Document(destStream))
            using (Aspose.Pdf.Document srcDoc = new Aspose.Pdf.Document(srcStream))
            {
                // Current insertion index (adjusted as pages are added)
                int currentInsertIndex = insertPosition;

                // Insert each selected page in the order defined in pagesToInsert
                foreach (int srcPageNumber in pagesToInsert)
                {
                    // Validate source page number
                    if (srcPageNumber < 1 || srcPageNumber > srcDoc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Source page {srcPageNumber} is out of range. Skipping.");
                        continue;
                    }

                    // Retrieve the page from the source document
                    Aspose.Pdf.Page pageToInsert = srcDoc.Pages[srcPageNumber];

                    // Insert a copy of the page into the destination document at the current index
                    // Pages.Insert shifts existing pages to the right, so we increment the index after each insertion
                    destDoc.Pages.Insert(currentInsertIndex, pageToInsert);
                    currentInsertIndex++;
                }

                // Save the modified destination PDF to a new file
                destDoc.Save(outputPath);
            }

            Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}