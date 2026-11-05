using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "merged.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Merge PDFs and preserve bookmarks using the core Document API
        using (Document target = new Document(firstPdf))
        using (Document source = new Document(secondPdf))
        {
            // Append all pages from the source document to the target document
            target.Pages.Add(source.Pages);

            // Import bookmarks (outlines) from the source document into the target document
            // OutlineCollection is enumerable; each element is an OutlineItemCollection (a single bookmark entry).
            foreach (OutlineItemCollection srcItem in source.Outlines)
            {
                // Adding the item to the target's root collection copies the bookmark hierarchy.
                target.Outlines.Add(srcItem);
            }

            // Save the merged PDF; the target document remains alive until Save completes
            target.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF with bookmarks saved to '{outputPdf}'.");
    }
}
