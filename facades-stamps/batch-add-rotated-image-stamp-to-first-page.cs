using System;
using System.IO;
using Aspose.Pdf.Facades;

class BatchImageStamp
{
    static void Main()
    {
        // Directory containing source PDFs
        const string inputDirectory = @"C:\Pdfs";
        // Image to be used as a stamp
        const string stampImagePath = @"C:\stamp.png";
        // Directory where stamped PDFs will be saved
        const string outputDirectory = @"C:\StampedPdfs";

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

        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the input directory
        foreach (string inputFilePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string outputFilePath = Path.Combine(
                outputDirectory,
                Path.GetFileNameWithoutExtension(inputFilePath) + "_stamped.pdf");

            try
            {
                // Initialize the facade and bind the source PDF
                PdfFileStamp fileStamp = new PdfFileStamp();
                fileStamp.BindPdf(inputFilePath);

                // Create a stamp, bind the image, rotate it, and limit it to the first page
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindImage(stampImagePath);
                stamp.Rotation = 45f;               // Rotate 45 degrees (arbitrary angle)
                stamp.Pages = new int[] { 1 };      // Apply only to the first page

                // Add the stamp to the document
                fileStamp.AddStamp(stamp);

                // Save the stamped PDF
                fileStamp.Save(outputFilePath);
                fileStamp.Close();

                Console.WriteLine($"Stamped PDF saved: {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputFilePath}': {ex.Message}");
            }
        }
    }
}