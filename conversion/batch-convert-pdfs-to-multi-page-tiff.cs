using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing PDF files
        string inputDirectory = @"C:\PdfFiles";
        // Directory where TIFF archives will be saved
        string outputDirectory = @"C:\TiffArchives";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string tiffPath = Path.Combine(outputDirectory, fileNameWithoutExt + ".tif");

            try
            {
                // Load PDF document
                using (Document pdfDocument = new Document(pdfPath))
                {
                    // Create TiffDevice with default settings (default compression)
                    TiffDevice tiffDevice = new TiffDevice();

                    // Convert entire PDF to a multi‑page TIFF archive
                    tiffDevice.Process(pdfDocument, tiffPath);
                }

                Console.WriteLine($"Converted '{pdfPath}' to '{tiffPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}