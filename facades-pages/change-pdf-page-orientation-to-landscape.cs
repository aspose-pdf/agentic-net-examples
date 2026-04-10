using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_landscape.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // A4 landscape dimensions (points). A4 portrait is 595 x 842.
        // Landscape swaps width and height.
        double landscapeWidth  = PageSize.A4.Height; // 842 points
        double landscapeHeight = PageSize.A4.Width;  // 595 points

        // Apply landscape size to every page
        foreach (Page page in pdfDocument.Pages)
        {
            page.PageInfo.Width  = landscapeWidth;
            page.PageInfo.Height = landscapeHeight;
        }

        // Save the modified PDF
        pdfDocument.Save(outputPdf);
        Console.WriteLine($"Landscape PDF saved to '{outputPdf}'.");
    }
}
