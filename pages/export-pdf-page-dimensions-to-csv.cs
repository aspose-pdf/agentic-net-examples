using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // source PDF
        const string outputCsvPath = "pages_dimensions.csv"; // CSV output

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (using the standard Document constructor)
        using (Document doc = new Document(inputPdfPath))
        {
            // Open a StreamWriter for the CSV file
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header
                writer.WriteLine("PageNumber,Width,Height");

                // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // PageInfo provides the page dimensions in points (1 point = 1/72 inch)
                    double width = page.PageInfo.Width;
                    double height = page.PageInfo.Height;

                    // Write a CSV line for the current page
                    writer.WriteLine($"{i},{width},{height}");
                }
            }

            // No additional saving of the PDF is required; we only exported data.
        }

        Console.WriteLine($"Page dimensions exported to '{outputCsvPath}'.");
    }
}