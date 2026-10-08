using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "page_dimensions.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare CSV content with a header row
            StringBuilder csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("PageNumber,Width,Height");

            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Width and Height are measured in points (1 point = 1/72 inch)
                double width = page.PageInfo.Width;
                double height = page.PageInfo.Height;

                csvBuilder.AppendLine($"{i},{width},{height}");
            }

            // Write the CSV data to the output file
            File.WriteAllText(outputCsvPath, csvBuilder.ToString(), Encoding.UTF8);
            Console.WriteLine($"Page dimensions exported to '{outputCsvPath}'.");
        }
    }
}