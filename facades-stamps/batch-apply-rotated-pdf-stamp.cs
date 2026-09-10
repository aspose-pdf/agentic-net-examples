using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Network locations – adjust as needed
        const string inputFolder  = @"\\Server\Share\PDFs";
        const string outputFolder = @"\\Server\Share\StampedPDFs";
        const string stampPdfPath = @"\\Server\Share\Stamp\stamp.pdf";

        // Stamp configuration
        const int    stampPageNumber = 1;      // page in the stamp PDF to use
        const float  rotationDegrees = 45f;   // desired rotation angle

        // Validate folders
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }
        Directory.CreateDirectory(outputFolder);

        // Process each PDF in the network share
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string sourcePath in pdfFiles)
        {
            string fileName   = Path.GetFileName(sourcePath);
            string targetPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Initialise the facade for stamping
                Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp();

                // Bind the source PDF document
                fileStamp.BindPdf(sourcePath);

                // Create a stamp from a page of another PDF
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindPdf(stampPdfPath, stampPageNumber);
                stamp.Rotation = rotationDegrees;   // rotate the stamp (degrees)

                // Apply the stamp to all pages (null means every page)
                stamp.Pages = null;

                // Add the configured stamp to the document
                fileStamp.AddStamp(stamp);

                // Save the stamped PDF to the output location
                fileStamp.Save(targetPath);

                // Release resources held by the facade
                fileStamp.Close();

                Console.WriteLine($"Stamped PDF saved: {targetPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{sourcePath}': {ex.Message}");
            }
        }
    }
}