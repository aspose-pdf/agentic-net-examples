using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Uniform margins (20 points on each side)
        const double leftMargin   = 20.0; // points
        const double rightMargin  = 20.0; // points
        const double topMargin    = 20.0; // points
        const double bottomMargin = 20.0; // points

        // Load the PDF using the high‑level Document API to obtain page dimensions.
        Document doc = new Document(inputPath);
        if (doc.Pages.Count == 0)
        {
            Console.Error.WriteLine("The PDF contains no pages.");
            return;
        }

        // Assume all pages have the same size; use the first page as reference.
        double pageWidth  = doc.Pages[1].PageInfo.Width;
        double pageHeight = doc.Pages[1].PageInfo.Height;

        // Compute scaling factors so that after scaling the content fits inside the desired margins.
        double scaleX = (pageWidth  - leftMargin - rightMargin) / pageWidth;
        double scaleY = (pageHeight - topMargin - bottomMargin) / pageHeight;
        // Use the smaller factor to keep the aspect ratio uniform.
        double uniformScale = Math.Min(scaleX, scaleY);

        // Use PdfPageEditor (the Facades API) to apply the scaling.
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(doc);
        // Apply the scaling to every page.
        editor.ProcessPages = Enumerable.Range(1, doc.Pages.Count).ToArray();
        editor.Zoom = (float)uniformScale;
        editor.Save(outputPath);

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
