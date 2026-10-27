using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";   // PDF to take the page from
        const string targetPath = "target.pdf";   // PDF to receive the page
        const string outputSource = "source_updated.pdf"; // source after removal
        const string outputTarget = "target_updated.pdf"; // target after addition
        const int pageNumber = 2; // 1‑based index of the page to move

        if (!File.Exists(sourcePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Load both documents; using ensures deterministic disposal
        using (Document sourceDoc = new Document(sourcePath))
        using (Document targetDoc = new Document(targetPath))
        {
            // Validate page number (Aspose.Pdf uses 1‑based indexing)
            if (pageNumber < 1 || pageNumber > sourceDoc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Retrieve the page reference; its size and rotation are preserved
            Page pageToMove = sourceDoc.Pages[pageNumber];

            // Add the page to the target document
            targetDoc.Pages.Add(pageToMove);

            // Remove the page from the source document
            sourceDoc.Pages.Delete(pageNumber);

            // Save the modified documents
            targetDoc.Save(outputTarget);   // PDF output, no SaveOptions needed
            sourceDoc.Save(outputSource);
        }

        Console.WriteLine("Page moved successfully.");
    }
}