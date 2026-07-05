using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_grayscale_even_pages.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document using the core Document API
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based. Process only even pages.
                for (int pageNumber = 2; pageNumber <= doc.Pages.Count; pageNumber += 2)
                {
                    // Convert the current even page to grayscale
                    doc.Pages[pageNumber].MakeGrayscale();
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}