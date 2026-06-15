using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileStamp, Stamp

class BatchImageStamp
{
    static void Main()
    {
        // Directory containing PDF files
        const string inputDirectory = @"C:\PdfFolder";
        // Path to the image that will be used as a stamp
        const string stampImagePath = @"C:\Images\stamp.png";
        // Rotation angle in degrees (e.g., 45°)
        const float rotationAngle = 45f;

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Process each PDF file in the directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            try
            {
                // Prepare output file name (original name + "_stamped.pdf")
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(pdfPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(pdfPath) + "_stamped.pdf");

                // Initialize the facade and bind the source PDF
                PdfFileStamp fileStamp = new PdfFileStamp();
                fileStamp.BindPdf(pdfPath);   // Load source PDF

                // Create a stamp, bind the image, set rotation and target page
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindImage(stampImagePath);   // Use the image as stamp content
                stamp.Rotation = rotationAngle;    // Rotate the stamp
                stamp.Pages = new int[] { 1 };     // Apply only to the first page

                // Add the stamp to the document
                fileStamp.AddStamp(stamp);

                // Save the result and release resources
                fileStamp.Save(outputPath);
                fileStamp.Close();

                Console.WriteLine($"Stamped PDF saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}