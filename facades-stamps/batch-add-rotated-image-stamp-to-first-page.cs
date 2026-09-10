using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the PDFs to process
        string inputDirectory = @"C:\PdfFolder";
        // Directory where stamped PDFs will be saved
        string outputDirectory = Path.Combine(inputDirectory, "Stamped");
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Build output file name (original name with "_stamped" suffix)
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputDirectory, fileNameWithoutExt + "_stamped.pdf");

            // Initialize the PdfFileStamp facade (does NOT implement IDisposable)
            PdfFileStamp fileStamp = new PdfFileStamp();

            // Bind the source PDF file
            fileStamp.BindPdf(pdfPath);

            // Create a stamp object
            Stamp stamp = new Stamp();

            // Bind an image to be used as the stamp (provide a valid image path)
            stamp.BindImage("stampImage.png");

            // Rotate the stamp by the desired angle (e.g., 45 degrees)
            stamp.Rotation = 45f;

            // Apply the stamp only to the first page
            stamp.Pages = new int[] { 1 };

            // Optionally place the stamp behind page content
            stamp.IsBackground = true;

            // Add the configured stamp to the PDF
            fileStamp.AddStamp(stamp);

            // Save the stamped PDF to the output path
            fileStamp.Save(outputPath);

            // Close the facade to release resources
            fileStamp.Close();
        }

        Console.WriteLine("Batch stamping completed.");
    }
}