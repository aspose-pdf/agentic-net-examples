using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";   // original PDF
        const string insertPath = "insert.pdf";   // PDF containing the page(s) to insert
        const string outputPath = "output.pdf";   // result PDF
        const int insertPosition = 5;              // 1‑based position where the new page(s) will be placed

        // Verify that the required files exist
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(insertPath))
        {
            Console.Error.WriteLine($"Insert file not found: {insertPath}");
            return;
        }

        try
        {
            // Load the target document (the one we will modify)
            Document targetDoc = new Document(sourcePath);
            // Load the document that provides the page(s) to insert
            Document insertDoc = new Document(insertPath);

            // Insert the pages from insertDoc into targetDoc at the specified 1‑based position.
            // The overload takes the position and a PageCollection (sourceDoc.Pages).
            targetDoc.Pages.Insert(insertPosition, insertDoc.Pages);

            // Save the resulting PDF
            targetDoc.Save(outputPath);

            Console.WriteLine($"Page(s) inserted successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}
