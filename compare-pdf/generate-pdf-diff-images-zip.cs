using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Devices;

class PdfDiffToZip
{
    static void Main()
    {
        // Paths to the PDFs to compare and the output ZIP file
        const string originalPdfPath = "original.pdf";
        const string revisedPdfPath  = "revised.pdf";
        const string outputZipPath   = "diff_images.zip";
        const string tempComparisonPdfPath = "_temp_comparison.pdf"; // temporary file

        // Verify input files exist
        if (!File.Exists(originalPdfPath))
        {
            Console.Error.WriteLine($"File not found: {originalPdfPath}");
            return;
        }
        if (!File.Exists(revisedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {revisedPdfPath}");
            return;
        }

        try
        {
            // Load the source documents
            using (Document originalDoc = new Document(originalPdfPath))
            using (Document revisedDoc  = new Document(revisedPdfPath))
            {
                // Perform side‑by‑side visual comparison – this static method creates a PDF file
                SideBySideComparisonOptions compareOptions = new SideBySideComparisonOptions();
                SideBySidePdfComparer.Compare(originalDoc, revisedDoc, tempComparisonPdfPath, compareOptions);

                // Load the generated comparison PDF
                using (Document comparisonDoc = new Document(tempComparisonPdfPath))
                {
                    // Create the ZIP archive for diff images
                    using (var zip = ZipFile.Open(outputZipPath, ZipArchiveMode.Create))
                    {
                        // Render each page of the comparison PDF to PNG and add to the ZIP
                        for (int pageNumber = 1; pageNumber <= comparisonDoc.Pages.Count; pageNumber++)
                        {
                            // Prepare a PNG device with a reasonable DPI (e.g., 150)
                            var resolution = new Resolution(150);
                            var pngDevice = new PngDevice(resolution);

                            using (var ms = new MemoryStream())
                            {
                                // Render the page into the memory stream
                                pngDevice.Process(comparisonDoc.Pages[pageNumber], ms);
                                ms.Position = 0;

                                // Add the PNG as an entry in the ZIP archive
                                string entryName = $"Page_{pageNumber}.png";
                                var entry = zip.CreateEntry(entryName, CompressionLevel.Optimal);
                                using (var entryStream = entry.Open())
                                {
                                    ms.CopyTo(entryStream);
                                }
                            }
                        }
                    }
                }
            }

            // Clean up the temporary comparison PDF
            if (File.Exists(tempComparisonPdfPath))
            {
                File.Delete(tempComparisonPdfPath);
            }

            Console.WriteLine($"Diff images saved to ZIP archive: {outputZipPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF comparison or ZIP creation: {ex.Message}");
        }
    }
}
