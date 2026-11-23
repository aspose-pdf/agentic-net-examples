using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "intro.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a top‑level bookmark titled "Introduction" that links to page 1
            OutlineItemCollection intro = new OutlineItemCollection(doc.Outlines);
            intro.Title = "Introduction";
            // Use a FitExplicitDestination to jump to the first page
            intro.Destination = new FitExplicitDestination(doc.Pages[1]);
            // Add the bookmark to the document's outline collection
            doc.Outlines.Add(intro);

            // Save the updated PDF as intro.pdf
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}