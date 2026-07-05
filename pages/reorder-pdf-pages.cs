using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";
        // Output PDF path
        const string outputPath = "reordered.pdf";

        // Define the new page order (1‑based indexes)
        // Example: move page 3 to first, then page 1, then page 2, etc.
        int[] newOrder = new int[] { 3, 1, 2 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load source document
            using (Document srcDoc = new Document(inputPath))
            {
                // Create a new empty document to hold reordered pages
                using (Document targetDoc = new Document())
                {
                    // Ensure the source document has enough pages
                    int pageCount = srcDoc.Pages.Count;
                    foreach (int idx in newOrder)
                    {
                        if (idx < 1 || idx > pageCount)
                        {
                            Console.Error.WriteLine($"Invalid page index {idx} in newOrder array.");
                            return;
                        }

                        // Retrieve the page from the source (1‑based)
                        Page srcPage = srcDoc.Pages[idx];

                        // Add a copy of the source page to the target document
                        // The Add(Page) overload clones the page into the target collection
                        targetDoc.Pages.Add(srcPage);
                    }

                    // Save the reordered PDF
                    targetDoc.Save(outputPath);
                }
            }

            Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}