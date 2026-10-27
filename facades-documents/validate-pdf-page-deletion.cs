using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_deleted.pdf";
        const int pageToDelete = 1; // 1‑based page number to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load original document and get the initial page count
            int originalPageCount;
            using (Document srcDoc = new Document(inputPath))
            {
                originalPageCount = srcDoc.Pages.Count;
                Console.WriteLine($"Original page count: {originalPageCount}");

                // Delete the specified page directly via the Document API
                srcDoc.Pages.Delete(pageToDelete);

                // Save the modified document
                srcDoc.Save(outputPath);
            }

            // Load the resulting document to obtain the new page count
            int newPageCount;
            using (Document dstDoc = new Document(outputPath))
            {
                newPageCount = dstDoc.Pages.Count;
                Console.WriteLine($"New page count after deletion: {newPageCount}");
            }

            // Validate that the page count decreased by exactly one
            if (newPageCount == originalPageCount - 1)
            {
                Console.WriteLine("Delete operation successful: page count reduced by one.");
            }
            else
            {
                Console.WriteLine("Delete operation failed: unexpected page count.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
