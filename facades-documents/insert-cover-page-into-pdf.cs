using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string coverPath  = "cover.pdf";          // PDF to be used as cover
        const string mainPath   = "main.pdf";           // Main document
        const string outputPath = "merged_with_cover.pdf"; // Resulting PDF

        // Ensure both source files exist
        if (!File.Exists(coverPath))
        {
            Console.Error.WriteLine($"Cover PDF not found: {coverPath}");
            return;
        }
        if (!File.Exists(mainPath))
        {
            Console.Error.WriteLine($"Main PDF not found: {mainPath}");
            return;
        }

        try
        {
            // Load the main document (target) and the cover document (source)
            Document target = new Document(mainPath);
            Document source = new Document(coverPath);

            // Insert all pages of the cover at the very beginning (page number 1) of the target document
            // Pages.Insert takes the position (1‑based) and a PageCollection to insert.
            target.Pages.Insert(1, source.Pages);

            // Save the merged document
            target.Save(outputPath);
            Console.WriteLine($"Cover page inserted successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}
