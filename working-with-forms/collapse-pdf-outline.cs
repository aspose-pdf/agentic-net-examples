using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one outline (bookmark) item
            if (doc.Outlines != null && doc.Outlines.Count > 0)
            {
                // Set the first outline item's Open property to false (collapsed)
                // OutlineItemCollection.Open controls the initial open/closed state
                doc.Outlines[1].Open = false;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with collapsed outline to '{outputPath}'.");
    }
}