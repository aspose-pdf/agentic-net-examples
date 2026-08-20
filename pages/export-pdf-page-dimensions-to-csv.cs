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

        try
        {
            // Load the PDF document (using the recommended lifecycle pattern)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Create a CSV file and write header
                using (StreamWriter csvWriter = new StreamWriter(outputCsvPath, false))
                {
                    csvWriter.WriteLine("PageNumber,Width,Height");

                    // Pages are 1‑based in Aspose.Pdf
                    for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                    {
                        Page page = pdfDoc.Pages[pageIndex];
                        double width = page.PageInfo.Width;   // page width in points
                        double height = page.PageInfo.Height; // page height in points

                        csvWriter.WriteLine($"{pageIndex},{width},{height}");
                    }
                }
            }

            Console.WriteLine($"Page dimensions exported to '{outputCsvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}