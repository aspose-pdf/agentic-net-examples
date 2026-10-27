using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for TiffDevice and Resolution

class PdfToTiffConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputFolder = "TiffPages";         // folder for TIFF images
        const int dpi = 300;                              // desired resolution

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // If the input PDF does not exist, create a minimal placeholder PDF
        if (!File.Exists(inputPdfPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPdfPath);
        }

        // Load the PDF inside a using block for deterministic disposal (document-disposal-with-using rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (page-indexing-one-based rule)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string tiffPath = Path.Combine(outputFolder, $"Page_{pageNumber}.tiff");

                // Create a TiffDevice with the required DPI. The constructor that accepts only Resolution
                // uses DPI as the default unit, so we avoid the missing ResolutionUnit enum.
                var resolution = new Resolution(dpi);
                var tiffDevice = new TiffDevice(resolution);

                // Save the individual page as a high‑resolution TIFF image
                using (FileStream imageStream = new FileStream(tiffPath, FileMode.Create))
                {
                    tiffDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine("Conversion completed. TIFF files are located in: " + Path.GetFullPath(outputFolder));
    }
}
