using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade classes: PdfFileStamp, Stamp

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder  = @"C:\Pdf\Input";
        // Folder where watermarked PDFs will be saved
        const string outputFolder = @"C:\Pdf\Output";

        // Watermark configuration
        const string watermarkImagePath = @"C:\Pdf\watermark.png"; // image used as watermark
        const float opacity   = 0.5f;   // 0 = fully transparent, 1 = fully opaque
        const float originX   = 100f;   // X coordinate of the watermark origin
        const float originY   = 200f;   // Y coordinate of the watermark origin

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Prepare the output file path (same file name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            // Initialize the PdfFileStamp facade
            PdfFileStamp fileStamp = new PdfFileStamp();

            // Bind the source PDF file
            fileStamp.BindPdf(inputPath);

            // Create a Stamp object that will act as the watermark
            Stamp stamp = new Stamp();

            // Bind the watermark image to the stamp
            stamp.BindImage(watermarkImagePath);

            // Position the watermark on the page
            stamp.SetOrigin(originX, originY);

            // Set visual properties
            stamp.Opacity = opacity;          // transparency
            stamp.IsBackground = true;        // render behind existing content

            // Add the stamp to the PDF
            fileStamp.AddStamp(stamp);

            // Save the watermarked PDF to the output location
            fileStamp.Save(outputPath);

            // Close the facade (releases file handles)
            fileStamp.Close();
        }

        Console.WriteLine("Watermarking completed.");
    }
}