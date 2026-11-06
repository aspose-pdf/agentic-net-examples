using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_letter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF using the file‑path overload of Document
            Document pdfDocument = new Document(inputPath);

            // Letter size in points (1 inch = 72 points)
            const double letterWidth = 8.5 * 72;   // 612 points
            const double letterHeight = 11 * 72;   // 792 points

            // Resize each page to Letter dimensions via PageInfo
            foreach (Page page in pdfDocument.Pages)
            {
                page.PageInfo.Width = letterWidth;
                page.PageInfo.Height = letterHeight;
                // Optional: ensure orientation matches the new size
                page.PageInfo.IsLandscape = letterWidth > letterHeight;
            }

            // Save the resized PDF to a new file
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
