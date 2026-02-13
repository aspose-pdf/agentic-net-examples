using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // -----------------------------------------------------------------
            // The original example used HeadingRecognitionStrategy and Headings,
            // which are not available in the current Aspose.Pdf version.
            // As a cross‑platform alternative we enumerate the document outlines
            // (bookmarks) which often represent the logical structure of the PDF.
            // -----------------------------------------------------------------
            if (pdfDocument.Outlines != null && pdfDocument.Outlines.Count > 0)
            {
                Console.WriteLine("Document outlines (bookmarks):");
                foreach (OutlineItemCollection outline in pdfDocument.Outlines)
                {
                    PrintOutline(outline, 0);
                }
            }
            else
            {
                Console.WriteLine("No outlines (bookmarks) found in the document.");
            }

            // Save the (potentially modified) PDF document
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Document saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Recursive helper to display outline hierarchy
    static void PrintOutline(OutlineItemCollection outline, int level)
    {
        Console.WriteLine($"{new string(' ', level * 2)}- {outline.Title}");
        foreach (OutlineItemCollection child in outline)
        {
            PrintOutline(child, level + 1);
        }
    }
}