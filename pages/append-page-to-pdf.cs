using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class AppendPageExample
{
    static void Main()
    {
        // Paths for the target PDF, the source PDF (page to copy), and the result PDF
        const string targetPath = "target.pdf";
        const string sourcePath = "source.pdf";
        const string outputPath = "merged.pdf";

        // Verify that both input files exist
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
            // Load the target document (the one we will append to)
            using (Document targetDoc = new Document(targetPath))
            // Load the source document (the one containing the page to copy)
            using (Document sourceDoc = new Document(sourcePath))
            {
                // Ensure the source document has at least one page
                if (sourceDoc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Source PDF contains no pages.");
                    return;
                }

                // Get the first page from the source document.
                // Aspose.Pdf uses 1‑based indexing for pages.
                Page pageToAppend = sourceDoc.Pages[1];

                // Append the page to the end of the target document.
                // The Add method copies the page content without altering the original.
                targetDoc.Pages.Add(pageToAppend);

                // Save the modified target document to the output file.
                targetDoc.Save(outputPath);
            }

            Console.WriteLine($"Page appended successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}