using System;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Facades;            // For additional utilities if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "collapsed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // The document outline (bookmarks) is an OutlineItemCollection.
            // Setting Open = false collapses the section.
            foreach (OutlineItemCollection outline in doc.Outlines)
            {
                outline.Open = false;   // collapse this outline item
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with collapsed outlines to '{outputPath}'.");
    }
}