using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // TiffDevice, Resolution

class Program
{
    static void Main()
    {
        // List of PDF files to be combined
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Output multi‑page TIFF file
        const string outputTiff = "merged.tif";

        // Verify that all source files exist
        foreach (var file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Source file not found: {file}");
                return;
            }
        }

        // Create a new Document that will hold all pages
        using (Document mergedDoc = new Document())
        {
            // Append pages from each PDF into the merged document
            foreach (var file in pdfFiles)
            {
                using (Document srcDoc = new Document(file))
                {
                    mergedDoc.Pages.Add(srcDoc.Pages);
                }
            }

            // Initialize TiffDevice with a default resolution (e.g., 300 DPI)
            // No explicit TiffSettings are provided, so default compression is used
            Resolution resolution = new Resolution(300);
            TiffDevice tiffDevice = new TiffDevice(resolution);

            // Convert the entire merged document to a multi‑page TIFF archive
            tiffDevice.Process(mergedDoc, outputTiff);
        }

        Console.WriteLine($"Multi‑page TIFF created at: {outputTiff}");
    }
}