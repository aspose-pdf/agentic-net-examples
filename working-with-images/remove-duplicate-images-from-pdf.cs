using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_duplicate_images.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the required load rule)
        using (Document doc = new Document(inputPath))
        {
            // OptimizeResources merges identical resources (including images) across all pages.
            // This effectively removes duplicate images by comparing their raw data.
            doc.OptimizeResources();

            // Save the cleaned document (using the required save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved without duplicate images: {outputPath}");
    }
}