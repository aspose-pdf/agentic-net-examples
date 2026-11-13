using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextAbsorber if needed in future

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDir    = "VectorPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Open the PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // Retrieve and display basic metadata
                Console.WriteLine($"Title : {doc.Info.Title}");
                Console.WriteLine($"Author: {doc.Info.Author}");

                // Extract each page as an SVG (vector format)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Create a temporary document containing only the current page
                    using (Document pageDoc = new Document())
                    {
                        // Add a copy of the page to the temporary document
                        pageDoc.Pages.Add(doc.Pages[i]);

                        // Define SVG save options (no extra options required for basic extraction)
                        SvgSaveOptions svgOptions = new SvgSaveOptions();

                        // Build output file name
                        string svgPath = Path.Combine(outputDir, $"Page_{i}.svg");

                        // Save the single‑page document as SVG
                        pageDoc.Save(svgPath, svgOptions);

                        Console.WriteLine($"Extracted vector page {i} → {svgPath}");
                    }
                }
            }

            Console.WriteLine("Metadata retrieval and vector extraction completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}