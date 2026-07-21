using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector; // Vector graphics handling lives here

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string targetPath = "target.pdf";
        const string outputPath = "merged.pdf";

        if (!File.Exists(sourcePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("Source or target PDF not found.");
            return;
        }

        // Load the source and target PDFs with deterministic disposal
        using (Document srcDoc = new Document(sourcePath))
        using (Document tgtDoc = new Document(targetPath))
        {
            // Work with the first page of each document (adjust as needed)
            Page srcPage = srcDoc.Pages[1];
            Page tgtPage = tgtDoc.Pages[1];

            // Absorb vector graphics from the source page
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(srcPage);

            // Add each extracted graphic to the target page
            foreach (var graphic in absorber.Elements)
            {
                // AddOnPage creates a copy of the graphic on the specified page
                graphic.AddOnPage(tgtPage);
            }

            // Save the modified target document
            tgtDoc.Save(outputPath);
        }

        Console.WriteLine($"Vector graphics copied to '{outputPath}'.");
    }
}
