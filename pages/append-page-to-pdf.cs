using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPdfPath = "target.pdf";   // PDF to which the page will be appended
        const string sourcePdfPath = "source.pdf";   // PDF containing the page to copy
        const string outputPdfPath = "output.pdf";   // Resulting PDF

        if (!File.Exists(targetPdfPath) || !File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document targetDoc = new Document(targetPdfPath))
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page pageToAppend = sourceDoc.Pages[1];

            // Append the page to the end of the target document
            targetDoc.Pages.Add(pageToAppend);

            // Save the combined document
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Page appended successfully. Output saved to '{outputPdfPath}'.");
    }
}