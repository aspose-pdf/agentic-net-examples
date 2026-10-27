using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document to obtain the total page count
        Document pdfDoc = new Document(inputPath);
        int pageCount = pdfDoc.Pages.Count;
        int[] allPages = Enumerable.Range(1, pageCount).ToArray(); // 1‑based page numbers

        // Use PdfPageEditor to apply a uniform zoom (80%) which leaves a 10 % margin on each side
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = allPages;   // target every page
            editor.Zoom = 0.8f;               // 80 % of original size → 10 % margin left/right & top/bottom
            editor.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
