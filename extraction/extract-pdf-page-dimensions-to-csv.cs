using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "pages_dimensions.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Open the CSV file for writing (also inside a using block)
            using (StreamWriter csvWriter = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header
                csvWriter.WriteLine("Page,Width,Height");

                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    Page page = pdfDoc.Pages[i];
                    double width = page.PageInfo.Width;
                    double height = page.PageInfo.Height;

                    // Write page number and dimensions
                    csvWriter.WriteLine($"{i},{width},{height}");
                }
            }
        }

        Console.WriteLine($"Page dimensions written to '{outputCsvPath}'.");
    }
}