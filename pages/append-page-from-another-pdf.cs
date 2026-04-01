using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the PDF files
        string targetPath = "target.pdf";   // existing document (or will be created)
        string sourcePath = "source.pdf";   // page to append
        string outputPath = "output.pdf";   // result file

        // Verify that the source file exists before attempting to load it
        if (!File.Exists(sourcePath))
        {
            Console.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Load the target document if it exists; otherwise create a new one with a blank page
        Document targetDoc;
        if (File.Exists(targetPath))
        {
            targetDoc = new Document(targetPath);
        }
        else
        {
            targetDoc = new Document();
            targetDoc.Pages.Add(); // ensure at least one page
        }

        using (targetDoc)
        {
            // Load the source document that contains the page to be appended
            using (Document sourceDoc = new Document(sourcePath))
            {
                // Get the first page from the source document (1‑based indexing)
                Page sourcePage = sourceDoc.Pages[1];

                // Append the source page to the end of the target document
                targetDoc.Pages.Add(sourcePage);
            }

            // Save the combined document
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Page appended successfully to {outputPath}");
    }
}