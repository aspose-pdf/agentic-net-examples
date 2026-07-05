using System;
using System.IO;
using Aspose.Pdf.Facades;

class BatchStampProcessor
{
    static void Main()
    {
        // Network share folder containing source PDFs
        const string sourceFolder = @"\\Server\Share\PdfFiles";
        // Folder where stamped PDFs will be saved (can be the same or different)
        const string outputFolder = @"\\Server\Share\StampedPdfFiles";
        // Path to the PDF file that will be used as the stamp source
        const string stampPdfPath = @"\\Server\Share\StampTemplate\stamp.pdf";
        // Page number (1‑based) in the stamp PDF to use as the stamp content
        const int stampPageNumber = 1;
        // Desired rotation angle for the stamp (in degrees, can be any value)
        const float stampRotationDegrees = 45f;

        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Get all PDF files in the source folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Build output file path – same name, placed in the output folder
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

                // Initialize PdfFileStamp facade
                PdfFileStamp fileStamp = new PdfFileStamp();
                fileStamp.InputFile = inputPath;
                fileStamp.OutputFile = outputPath;

                // Create a stamp based on a page from another PDF
                Stamp stamp = new Stamp();
                stamp.BindPdf(stampPdfPath, stampPageNumber); // use specified page as stamp content
                stamp.Rotation = stampRotationDegrees;       // rotate the stamp
                stamp.IsBackground = true;                  // place stamp behind page content (optional)
                // Apply to all pages (default); can also set specific pages:
                // stamp.Pages = new int[] { 1, 3, 5 };

                // Add the stamp to the document
                fileStamp.AddStamp(stamp);

                // Finalize and release resources
                fileStamp.Close();

                Console.WriteLine($"Stamped PDF saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}