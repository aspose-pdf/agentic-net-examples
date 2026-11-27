using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string manifestPath = "manifest.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        var manifestLines = new List<string>();

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                Page page = doc.Pages[pageNumber];
                int imageCount = 0;

                // XImageCollection is iterated directly, not as a dictionary
                foreach (XImage img in page.Resources.Images)
                {
                    imageCount++;
                }

                manifestLines.Add($"Page {pageNumber}: {imageCount} image(s)");
            }
        }

        // Write the manifest text file
        File.WriteAllLines(manifestPath, manifestLines);
        Console.WriteLine($"Manifest written to {manifestPath}");
    }
}