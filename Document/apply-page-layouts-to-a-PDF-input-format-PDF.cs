using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf contains Document, PageLayout, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, apply a page layout, and save.
        // The Document is wrapped in a using block for deterministic disposal (rule: document-disposal-with-using).
        using (Document doc = new Document(inputPath))
        {
            // Choose the desired layout. Options include:
            // SinglePage, OneColumn, TwoColumnLeft, TwoColumnRight,
            // TwoPageLeft, TwoPageRight, Default.
            doc.PageLayout = PageLayout.TwoColumnLeft; // example layout

            // Save the modified document. No SaveOptions are needed because we are saving as PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with layout to '{outputPath}'.");
    }
}