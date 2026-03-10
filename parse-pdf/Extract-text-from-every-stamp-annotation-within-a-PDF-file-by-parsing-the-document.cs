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
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all annotations on the page (1‑based indexing)
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // Check if the annotation is a StampAnnotation
                    if (ann is StampAnnotation stamp)
                    {
                        // Extract the textual content of the stamp
                        string stampText = stamp.Contents ?? string.Empty;

                        Console.WriteLine($"Page {pageIndex}, Stamp {annIndex}: \"{stampText}\"");
                    }
                }
            }
        }
    }
}