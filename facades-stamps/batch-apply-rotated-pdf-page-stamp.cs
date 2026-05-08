using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // UNC path to the folder that contains the PDFs to be processed
        const string networkFolder = @"\\Server\Share\PDFs";

        // Path to the PDF that provides the page used as a stamp
        const string stampSourcePdf = @"\\Server\Share\stamp.pdf";

        // Page number (1‑based) from the stamp source PDF to be used as the stamp
        const int stampPageNumber = 1;

        // Rotation angle for the stamp (degrees). Use the 'F' suffix for a float literal.
        const float rotationDegrees = 45F;

        // Verify that the source folder exists
        if (!Directory.Exists(networkFolder))
        {
            Console.Error.WriteLine($"Folder not found: {networkFolder}");
            return;
        }

        // Get all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(networkFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            // Build the output file name (original name + "_stamped.pdf")
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_stamped.pdf");

            // ------------------------------------------------------------
            // 1. Initialise the PdfFileStamp facade and bind the input PDF
            // ------------------------------------------------------------
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.InputFile = inputPath;   // load the target PDF
            fileStamp.OutputFile = outputPath; // destination file

            // ------------------------------------------------------------
            // 2. Create a Stamp that uses a page from another PDF
            // ------------------------------------------------------------
            Stamp stamp = new Stamp();
            stamp.BindPdf(stampSourcePdf, stampPageNumber); // use page as stamp content
            stamp.Rotation = rotationDegrees;               // rotate the stamp
            stamp.IsBackground = true;                      // place behind page content (optional)

            // ------------------------------------------------------------
            // 3. Apply the stamp to all pages of the document
            // ------------------------------------------------------------
            fileStamp.AddStamp(stamp);

            // ------------------------------------------------------------
            // 4. Persist the changes and release resources
            // ------------------------------------------------------------
            fileStamp.Save(outputPath);
            fileStamp.Close();

            Console.WriteLine($"Stamped PDF saved: {outputPath}");
        }

        Console.WriteLine("Batch stamping completed.");
    }
}