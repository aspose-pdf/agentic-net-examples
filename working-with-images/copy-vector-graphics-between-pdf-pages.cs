using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string outputPdfPath = "merged.pdf";

        if (!File.Exists(sourcePdfPath) || !File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine("Source or target PDF not found.");
            return;
        }

        // Load source and target documents
        using (Document sourceDoc = new Document(sourcePdfPath))
        using (Document targetDoc = new Document(targetPdfPath))
        {
            // Choose pages (1‑based indexing)
            Page sourcePage = sourceDoc.Pages[1];
            Page targetPage = targetDoc.Pages[1];

            // Absorb vector graphics from the source page using the correct absorber class
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(sourcePage);

            // Add each extracted graphic element individually to the target page
            foreach (var element in absorber.Elements)
            {
                element.AddOnPage(targetPage);
            }

            // Save the modified target document
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Vector graphics copied to '{outputPdfPath}'.");
    }
}