using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document srcDoc = new Document(inputPdf))
            {
                int pageCount = srcDoc.Pages.Count;

                // Iterate through each page, copy it into a new single‑page document and save
                for (int i = 1; i <= pageCount; i++) // Aspose.Pdf pages are 1‑based
                {
                    // Create a fresh document for the single page
                    using (Document pageDoc = new Document())
                    {
                        // Add the current page from the source document to the new document
                        pageDoc.Pages.Add(srcDoc.Pages[i]);

                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");
                        pageDoc.Save(outPath);
                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }

            Console.WriteLine("Splitting complete.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
