using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF using the high‑level Document API
            Document pdfDoc = new Document(inputPath);
            int pageCount = pdfDoc.Pages.Count; // 1‑based page numbers

            // Increase each page size by 5 % – the original content stays at the same position,
            // so the extra area becomes white margin around the existing page.
            for (int i = 1; i <= pageCount; i++)
            {
                var pageInfo = pdfDoc.Pages[i].PageInfo;
                double newWidth = pageInfo.Width * 1.05;   // 5 % wider
                double newHeight = pageInfo.Height * 1.05; // 5 % taller
                pageInfo.Width = newWidth;
                pageInfo.Height = newHeight;
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
