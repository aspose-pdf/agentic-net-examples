using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the target PDF (to be stamped) and the final output PDF
        const string targetPdfPath = "target.pdf";
        const string outputPdfPath = "stamped_output.pdf";

        // PDF files that will be combined into a multi‑page stamp
        string[] stampPdfFiles = new string[]
        {
            "stamp1.pdf",
            "stamp2.pdf",
            "stamp3.pdf"
        };

        // Ensure all required files exist before proceeding
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        foreach (string stampFile in stampPdfFiles)
        {
            if (!File.Exists(stampFile))
            {
                Console.Error.WriteLine($"Stamp PDF not found: {stampFile}");
                return;
            }
        }

        // Initialize PdfFileStamp and bind the target PDF (input)
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(targetPdfPath); // Bind only the input PDF

        // Create a Stamp object for each source PDF and add it to the PdfFileStamp
        foreach (string stampFile in stampPdfFiles)
        {
            // The Stamp class works with a specific page of the source PDF.
            // Here we use the first page of each source PDF as a stamp page.
            Stamp stamp = new Stamp();
            stamp.BindPdf(stampFile, 1);          // Bind the first page of the source PDF
            stamp.IsBackground = true;            // Apply as background (watermark) – optional
            stamp.Opacity = 0.5f;                 // Semi‑transparent – optional
            fileStamp.AddStamp(stamp);
        }

        // Save the stamped PDF to the desired output path
        fileStamp.Save(outputPdfPath);
        // Release resources
        fileStamp.Close();

        Console.WriteLine($"Multi‑page stamp applied successfully. Output saved to '{outputPdfPath}'.");
    }
}
