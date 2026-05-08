using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing source PDF files
        const string inputDir = @"C:\PdfInput";
        // Path for the resulting multi‑page TIFF archive
        const string outputTiff = @"C:\PdfOutput\Combined.tif";

        // Validate input directory
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Collect all PDF files (1‑based indexing is not relevant here)
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found to process.");
            return;
        }

        // Merge all PDFs into a single Document
        using (Document mergedDoc = new Document())
        {
            // Document.Merge(params string[]) merges the specified PDF files
            mergedDoc.Merge(pdfFiles);

            // Create a TiffDevice with default settings (default compression)
            TiffDevice tiffDevice = new TiffDevice();

            // Convert the merged PDF to a multi‑page TIFF archive
            tiffDevice.Process(mergedDoc, outputTiff);
        }

        Console.WriteLine($"Batch conversion completed. TIFF saved to: {outputTiff}");
    }
}