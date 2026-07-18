using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Added for destination types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_heading.pdf";
        const string headingTitle = "New Section";
        const int targetPageNumber = 2; // 1‑based page index where the heading points

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a new outline (bookmark) item
            OutlineItemCollection newOutline = new OutlineItemCollection(doc.Outlines)
            {
                Title = headingTitle,
                // Set the destination to the specified page (fit to window)
                Destination = new FitExplicitDestination(doc.Pages[targetPageNumber])
            };

            // Add the outline to the document's outline collection
            doc.Outlines.Add(newOutline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new heading at '{outputPath}'.");
    }
}
