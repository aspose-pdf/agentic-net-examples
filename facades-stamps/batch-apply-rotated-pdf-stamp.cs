using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Network share folder containing PDFs to be stamped
        const string sourceFolder = @"\\networkshare\pdfs";

        // PDF file that provides the page to be used as a stamp
        const string stampPdfPath = @"\\networkshare\stamp\stamp.pdf";
        const int stampPageNumber = 1;          // page number in the stamp PDF
        const float rotationAngle = 45f;        // rotation angle in degrees

        // Validate paths
        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }
        if (!File.Exists(stampPdfPath))
        {
            Console.Error.WriteLine($"Stamp PDF not found: {stampPdfPath}");
            return;
        }

        // Get all PDF files in the network share (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            // Build output file name (original name with "_stamped" suffix)
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(sourceFolder, $"{fileNameWithoutExt}_stamped.pdf");

            // Initialize the PdfFileStamp facade (do NOT wrap in using – it is not IDisposable)
            PdfFileStamp fileStamp = new PdfFileStamp();

            // Bind the source PDF that will receive the stamp
            fileStamp.BindPdf(inputPath);

            // Create a stamp based on a page from another PDF
            Stamp stamp = new Stamp();
            stamp.BindPdf(stampPdfPath, stampPageNumber);

            // Apply rotation (arbitrary angle)
            stamp.Rotation = rotationAngle;

            // Place the stamp behind page content (optional)
            stamp.IsBackground = true;

            // Add the stamp to the document (null Pages means all pages)
            fileStamp.AddStamp(stamp);

            // Save the stamped PDF to the output path
            fileStamp.Save(outputPath);

            // Close the facade to release resources
            fileStamp.Close();
        }

        Console.WriteLine("Batch stamping completed.");
    }
}