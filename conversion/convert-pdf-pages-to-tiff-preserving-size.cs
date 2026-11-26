using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // Resolution, TiffDevice, TiffSettings, CompressionType

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "TiffPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the source PDF
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Iterate through each page of the source document
            for (int i = 1; i <= srcDoc.Pages.Count; i++)
            {
                Page srcPage = srcDoc.Pages[i];

                // Create a new single‑page document and copy the source page into it.
                using (Document singlePageDoc = new Document())
                {
                    // Pages.Add copies the page and retains its original size.
                    singlePageDoc.Pages.Add(srcPage);

                    // Ensure the page size is exactly the same as the source (optional, defensive).
                    // singlePageDoc.Pages[1].SetRectangle(srcPage.PageInfo.MediaBox);

                    // Prepare the TIFF device – this is the core API for rasterizing pages.
                    var resolution = new Resolution(300); // 300 DPI preserves original size proportionally
                    var tiffSettings = new TiffSettings
                    {
                        Compression = CompressionType.CCITT4 // equivalent to TiffCompression.CcittGroup4
                    };
                    var tiffDevice = new TiffDevice(resolution, tiffSettings);

                    string outputTiffPath = Path.Combine(outputFolder, $"Page_{i}.tiff");

                    // Save the only page of the temporary document as a TIFF image.
                    // Process renders the document (or a single‑page document) to the specified file.
                    tiffDevice.Process(singlePageDoc, outputTiffPath);
                    Console.WriteLine($"Saved page {i} as TIFF → {outputTiffPath}");
                }
            }
        }

        Console.WriteLine("PDF to TIFF conversion completed.");
    }
}
