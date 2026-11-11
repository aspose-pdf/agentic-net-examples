using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_a3.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // A3 size in points (1 point = 1/72 inch)
        const double a3Width = 842;   // points
        const double a3Height = 1191; // points

        // Resize every page to A3
        foreach (Page page in pdfDocument.Pages)
        {
            page.PageInfo.Width = a3Width;
            page.PageInfo.Height = a3Height;
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"All pages resized to A3 and saved as '{outputPath}'.");
    }
}
