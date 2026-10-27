using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for VerticalAlignment enum

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned_page3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor with ProcessPages to target specific pages
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(inputPath);
            // Specify the page(s) to edit – 1‑based indexing
            pageEditor.ProcessPages = new int[] { 3 };
            // Align the page content to the top
            pageEditor.VerticalAlignmentType = Aspose.Pdf.VerticalAlignment.Top;
            // Save the modified PDF
            pageEditor.Save(outputPath);
        }

        Console.WriteLine($"Page 3 aligned to top and saved as '{outputPath}'.");
    }
}
