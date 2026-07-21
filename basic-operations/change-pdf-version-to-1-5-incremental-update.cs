using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_incremental.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF.
        using (Document sourceDoc = new Document(inputPath))
        {
            // Create a new empty PDF with the desired version (1.5).
            using (Document targetDoc = new Document(PdfVersion.v_1_5))
            {
                // Copy all pages from the source document into the new document.
                targetDoc.Pages.Add(sourceDoc.Pages);

                // Save using the parameterless Save() method.
                // This writes the file using incremental update technique.
                targetDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with version 1.5 and incremental update: {outputPath}");
    }
}