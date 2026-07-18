using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class BatchPdfToTiff
{
    static void Main()
    {
        // Directory containing source PDF files
        const string inputDirectory = @"C:\PdfInput";

        // Directory where the resulting multi‑page TIFF files will be saved
        const string outputDirectory = @"C:\TiffOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Derive the output TIFF file name from the PDF file name
            string tiffFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".tif";
            string tiffPath = Path.Combine(outputDirectory, tiffFileName);

            // Load the PDF document (using the standard Document constructor)
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Create a TiffDevice with default settings (default compression is None)
                TiffDevice tiffDevice = new TiffDevice();

                // Convert the entire PDF to a multi‑page TIFF archive
                tiffDevice.Process(pdfDocument, tiffPath);
            }

            Console.WriteLine($"Converted '{pdfPath}' to '{tiffPath}'.");
        }

        Console.WriteLine("Batch conversion completed.");
    }
}