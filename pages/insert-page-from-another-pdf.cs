using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";      // PDF to receive the new page
        const string sourcePath = "source.pdf";      // PDF containing the page to insert
        const string outputPath = "merged.pdf";      // Resulting PDF

        // Verify files exist
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the target document (where the page will be inserted)
            using (Document targetDoc = new Document(targetPath))
            // Load the source document (containing the page to insert)
            using (Document sourceDoc = new Document(sourcePath))
            {
                // Choose the page to insert from the source document.
                // Here we take the first page; change the index if needed.
                Page pageToInsert = sourceDoc.Pages[1];

                // Insert the page at position 2 in the target document.
                // This preserves the page's original size, rotation, and other properties.
                targetDoc.Pages.Insert(2, pageToInsert);

                // Save the modified target document.
                targetDoc.Save(outputPath);
            }

            Console.WriteLine($"Page inserted successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}