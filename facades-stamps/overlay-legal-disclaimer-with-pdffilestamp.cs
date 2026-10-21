using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the target PDF, the disclaimer PDF, and the output PDF
        const string targetPdfPath = "target.pdf";
        const string disclaimerPdfPath = "disclaimer.pdf";
        const string outputPdfPath = "target_with_disclaimer.pdf";

        // Verify that the input files exist
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }
        if (!File.Exists(disclaimerPdfPath))
        {
            Console.Error.WriteLine($"Disclaimer PDF not found: {disclaimerPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Build a Stamp that represents the first page of the disclaimer
        // ------------------------------------------------------------
        Aspose.Pdf.Facades.Stamp disclaimerStamp = new Aspose.Pdf.Facades.Stamp();
        // Bind the source PDF page that will be used as the stamp
        disclaimerStamp.BindPdf(disclaimerPdfPath, 1);
        // Apply the stamp to the first page of the target document
        disclaimerStamp.Pages = new[] { 1 };
        // Overlay (false = on top of existing content, true = behind)
        disclaimerStamp.IsBackground = false;
        // Optional: position, opacity, rotation, etc.
        // disclaimerStamp.SetOrigin(0, 0);
        // disclaimerStamp.Opacity = 1.0F;

        // ------------------------------------------------------------
        // Apply the stamp to the target PDF using PdfFileStamp
        // ------------------------------------------------------------
        PdfFileStamp fileStamp = new PdfFileStamp(); // does NOT implement IDisposable
        fileStamp.BindPdf(targetPdfPath);
        fileStamp.AddStamp(disclaimerStamp); // the only method that applies a stamp
        fileStamp.Save(outputPdfPath);
        fileStamp.Close();

        Console.WriteLine($"Disclaimer stamped onto first page. Output saved to '{outputPdfPath}'.");
    }
}
