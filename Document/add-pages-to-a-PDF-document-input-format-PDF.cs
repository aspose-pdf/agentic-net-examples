using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_extra_pages.pdf";

        // Number of blank pages to add
        const int pagesToAdd = 3;

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPath))
            {
                // Add the requested number of blank pages
                for (int i = 0; i < pagesToAdd; i++)
                {
                    // PageCollection.Add() creates an empty page.
                    // The size of the new page follows the most common size in the document.
                    doc.Pages.Add();
                }

                // Save the modified document (lifecycle rule: use Save with path)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Successfully added {pagesToAdd} page(s) and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}