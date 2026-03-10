using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate through all annotations on the current page (1‑based indexing)
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Check if the annotation is a StampAnnotation
                    if (ann is StampAnnotation stamp)
                    {
                        // Retrieve the textual content of the stamp
                        string stampText = stamp.Contents ?? string.Empty;

                        Console.WriteLine($"Stamp found on page {pageNum}, annotation #{annIdx}: \"{stampText}\"");
                    }
                }
            }
        }
    }
}