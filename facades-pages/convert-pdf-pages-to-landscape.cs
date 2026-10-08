using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (Aspose.Pdf.Document provides full page manipulation capabilities)
        Document pdfDocument = new Document(inputPath);

        // A4 size in points (portrait). For landscape we swap width/height.
        float a4Width  = PageSize.A4.Width;   // 595.2756 pt
        float a4Height = PageSize.A4.Height;  // 841.8898 pt

        // Apply landscape A4 dimensions to every page
        foreach (Page page in pdfDocument.Pages)
        {
            // Set width to the original portrait height and height to the original portrait width
            page.PageInfo.Width  = a4Height; // landscape width
            page.PageInfo.Height = a4Width;  // landscape height
            // Optional flag – makes the orientation explicit
            page.PageInfo.IsLandscape = true;
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);
        Console.WriteLine($"Landscape PDF saved to '{outputPath}'.");
    }
}
