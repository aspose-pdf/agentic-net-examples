using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "pages_dimensions.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create (or overwrite) the CSV file
                using (StreamWriter writer = new StreamWriter(csvPath, false))
                {
                    // CSV header
                    writer.WriteLine("PageNumber,WidthPoints,HeightPoints,WidthInches,HeightInches");

                    // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        Page page = doc.Pages[i];

                        // Page dimensions are stored in points (1 point = 1/72 inch)
                        double widthPts  = page.PageInfo.Width;
                        double heightPts = page.PageInfo.Height;

                        // Convert points to inches for easier analysis
                        double widthIn  = widthPts / 72.0;
                        double heightIn = heightPts / 72.0;

                        // Write a CSV line for the current page
                        writer.WriteLine($"{i},{widthPts},{heightPts},{widthIn:F2},{heightIn:F2}");
                    }
                }
            }

            Console.WriteLine($"Page dimensions exported to '{csvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}