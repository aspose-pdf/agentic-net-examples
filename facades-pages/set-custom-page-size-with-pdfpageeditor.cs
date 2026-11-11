using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "custom_page.pdf";

        // Custom page dimensions in points (1 inch = 72 points)
        double customWidth = 500;   // e.g., 500 points (~6.94 inches)
        double customHeight = 700;  // e.g., 700 points (~9.72 inches)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (Aspose.Pdf.Document, not PdfPageEditor, for page size manipulation)
        Document pdfDoc = new Document(inputPath);

        // Apply the custom size to every page (pages are 1‑based in Aspose.Pdf)
        for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
        {
            Page page = pdfDoc.Pages[pageNumber];
            page.PageInfo.Width = customWidth;
            page.PageInfo.Height = customHeight;
        }

        // Save the modified PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"PDF saved with custom page size to '{outputPath}'.");
    }
}
