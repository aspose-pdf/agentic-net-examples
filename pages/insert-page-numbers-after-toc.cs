using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Find the bookmark that represents the Table of Contents
            int tocPageNumber = -1;
            foreach (OutlineItemCollection outline in doc.Outlines)
            {
                // Simple title match – adjust as needed for your document
                if (outline.Title != null && outline.Title.IndexOf("Table of Contents", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // The bookmark usually points to a page via a GoToAction
                    if (outline.Action is GoToAction goTo && goTo.Destination is ExplicitDestination dest)
                    {
                        // ExplicitDestination provides the target page (1‑based)
                        tocPageNumber = dest.PageNumber;
                    }
                    else if (outline.Destination is ExplicitDestination dest2)
                    {
                        tocPageNumber = dest2.PageNumber;
                    }
                    break;
                }
            }

            if (tocPageNumber == -1)
            {
                Console.Error.WriteLine("Table of Contents bookmark not found. No page numbers added.");
                doc.Save(outputPath);
                return;
            }

            // Add page numbers to all pages after the TOC
            for (int i = tocPageNumber + 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a page number stamp; "#" will be replaced by the actual number
                PageNumberStamp stamp = new PageNumberStamp("#")
                {
                    // Position the stamp at the bottom‑right corner
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    // Optional styling
                    Background = false,
                    Opacity    = 0.8f,
                    // Use the page's own number as the starting number
                    StartingNumber = i
                };

                // Add the stamp to the page
                page.AddStamp(stamp);
            }

            // Save the modified document
            doc.Save(outputPath);
            Console.WriteLine($"Page numbers added after TOC. Saved to '{outputPath}'.");
        }
    }
}