using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // Path to the source PDF
        const string outputCsvPath = "pages_dimensions.csv"; // Path for the CSV output

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare the CSV file for writing
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header
                writer.WriteLine("PageNumber,Width,Height");

                // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    Page page = pdfDoc.Pages[i];

                    // Page dimensions are available via PageInfo
                    double width = page.PageInfo.Width;
                    double height = page.PageInfo.Height;

                    // Write a CSV line for the current page
                    writer.WriteLine($"{i},{width},{height}");
                }
            }
        }

        Console.WriteLine($"Page dimensions exported to '{outputCsvPath}'.");
    }
}