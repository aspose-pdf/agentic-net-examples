using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 4 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count >= 4)
            {
                // Get page 4
                Page page = doc.Pages[4];

                // Collect all LinkAnnotation objects on the page
                List<LinkAnnotation> linksToRemove = new List<LinkAnnotation>();
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is LinkAnnotation link)
                        linksToRemove.Add(link);
                }

                // Remove the collected link annotations
                foreach (LinkAnnotation link in linksToRemove)
                {
                    page.Annotations.Delete(link);
                }
            }
            else
            {
                Console.WriteLine("The document has fewer than 4 pages; no annotations were removed.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}