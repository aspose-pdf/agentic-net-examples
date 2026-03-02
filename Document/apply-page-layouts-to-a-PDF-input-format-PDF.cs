using System;
using System.IO;
using Aspose.Pdf; // Document, PageLayout, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, set the desired page layout, and save.
        // Document implements IDisposable – use a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Choose a layout. Options: SinglePage, OneColumn, TwoColumnLeft,
            // TwoColumnRight, TwoPageLeft, TwoPageRight, Default.
            doc.PageLayout = PageLayout.TwoColumnLeft; // example layout

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with layout to '{outputPath}'.");
    }
}