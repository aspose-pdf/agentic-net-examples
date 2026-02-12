using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path (adjust as needed)
        const string inputPath = "input.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document sourceDoc = new Document(inputPath);

            // Iterate over each page (Aspose.Pdf collections are 1‑based)
            for (int i = 1; i <= sourceDoc.Pages.Count; i++)
            {
                // Create a new empty PDF document
                Document singlePageDoc = new Document();

                // Add the current page from the source document to the new document
                // This copies the page content into the new document
                singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                // Build an output file name for the split page
                string outputPath = $"output_page_{i}.pdf";

                // Save the single‑page PDF using the prescribed rule
                // document-save: {DocumentVar}.Save({OutputPath});
                singlePageDoc.Save(outputPath);

                Console.WriteLine($"Page {i} saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Generic error handling – report any unexpected issues
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}