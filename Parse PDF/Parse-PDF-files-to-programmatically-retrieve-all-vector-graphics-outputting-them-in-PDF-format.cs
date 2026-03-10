using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF containing vector graphics
        const string outputPdfPath = "vector_pages.pdf"; // PDF that will contain only pages with vector graphics

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the source document inside a using block (ensures proper disposal)
        using (Document sourceDoc = new Document(inputPdfPath))
        {
            // Create an empty target document
            using (Document targetDoc = new Document())
            {
                // Iterate through all pages (1‑based indexing)
                for (int i = 1; i <= sourceDoc.Pages.Count; i++)
                {
                    Page page = sourceDoc.Pages[i];

                    // Detect if the page contains any vector graphics
                    if (page.HasVectorGraphics())
                    {
                        // Add the whole page (including its vector graphics) to the target document
                        targetDoc.Pages.Add(page);
                    }
                }

                // Save the resulting PDF that contains only the extracted vector‑graphic pages
                targetDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Vector‑graphic pages saved to '{outputPdfPath}'.");
    }
}