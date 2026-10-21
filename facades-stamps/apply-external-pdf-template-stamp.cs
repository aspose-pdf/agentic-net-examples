using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the external stamp template PDF, and the output PDF
        const string sourcePdfPath = "input.pdf";
        const string stampPdfPath  = "stamp_template.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(stampPdfPath))
        {
            Console.Error.WriteLine($"Stamp template PDF not found: {stampPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Create a Stamp object that represents the external template
        // ------------------------------------------------------------
        Stamp stamp = new Stamp();
        // Bind the first page of the template PDF as the stamp content
        stamp.BindPdf(stampPdfPath, 1);
        // Position of the stamp on the target page (lower‑left corner, points)
        stamp.SetOrigin(100, 100); // adjust X/Y as needed
        // Apply the stamp only to page 3 (Aspose.Pdf uses 1‑based indexing)
        stamp.Pages = new[] { 3 };
        // Set to overlay (false) or background (true) – here we overlay
        stamp.IsBackground = false;

        // ------------------------------------------------------------
        // 2. Apply the stamp to the source PDF using PdfFileStamp
        // ------------------------------------------------------------
        PdfFileStamp pdfStamp = new PdfFileStamp();
        pdfStamp.BindPdf(sourcePdfPath);
        pdfStamp.AddStamp(stamp); // the only method that adds a stamp
        pdfStamp.Save(outputPdfPath);
        pdfStamp.Close();

        Console.WriteLine($"Stamp applied to page 3 and saved as '{outputPdfPath}'.");
    }
}
