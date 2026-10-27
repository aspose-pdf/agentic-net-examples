using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized.pdf";

        // Desired page dimensions (points). 1 point = 1/72 inch.
        const double newWidth  = 595; // A4 width 8.27in * 72
        const double newHeight = 842; // A4 height 11.69in * 72

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document. All annotations, form fields, links, etc. are preserved.
            Document pdfDocument = new Document(inputPath);

            // Iterate over each page and set the new dimensions.
            foreach (Page page in pdfDocument.Pages)
            {
                page.PageInfo.Width  = newWidth;
                page.PageInfo.Height = newHeight;
            }

            // Save the modified PDF.
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
