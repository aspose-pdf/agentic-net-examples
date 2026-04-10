using System;
using System.IO;
using Aspose.Pdf;

class ExportPageDimensions
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output CSV file path
        const string outputCsvPath = "page_dimensions.csv";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create (or overwrite) the CSV file
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header
                writer.WriteLine("PageNumber,Width,Height");

                // Iterate over pages (1‑based indexing as required by Aspose.Pdf)
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    Page page = pdfDoc.Pages[i];

                    // Page dimensions are available via PageInfo (units are points)
                    double width = page.PageInfo.Width;
                    double height = page.PageInfo.Height;

                    // Write a CSV line: page number, width, height
                    writer.WriteLine($"{i},{width},{height}");
                }
            }
        }

        Console.WriteLine($"Page dimensions exported to '{outputCsvPath}'.");
    }
}