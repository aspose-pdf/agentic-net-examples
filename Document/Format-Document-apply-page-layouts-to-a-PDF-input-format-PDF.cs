using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, PageLayout, etc.)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_formatted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Apply the desired page layout. Options are defined in Aspose.Pdf.PageLayout enum.
            // Example: display pages in two columns with odd-numbered pages on the left.
            doc.PageLayout = PageLayout.TwoColumnLeft;

            // Save the modified document. No SaveOptions needed because we are saving as PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with new layout to '{outputPath}'.");
    }
}