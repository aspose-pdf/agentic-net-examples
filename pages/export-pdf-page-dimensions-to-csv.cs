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
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Prepare CSV writer
                using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
                {
                    // Write CSV header
                    writer.WriteLine("PageNumber,Width,Height");

                    // Iterate over all pages (1‑based indexing)
                    for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                    {
                        Page page = pdfDoc.Pages[i];
                        double width = page.PageInfo.Width;
                        double height = page.PageInfo.Height;

                        // Write dimensions for the current page
                        writer.WriteLine($"{i},{width},{height}");
                    }
                }

                // No need to save the PDF; just export dimensions
                Console.WriteLine($"Page dimensions exported to '{outputCsvPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}