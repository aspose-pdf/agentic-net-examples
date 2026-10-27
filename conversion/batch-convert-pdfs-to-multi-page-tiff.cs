using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // Provides TiffDevice for PDF‑to‑TIFF conversion

class BatchPdfToTiff
{
    static void Main()
    {
        // Base directory of the running application
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Input and output folders (relative to the base directory)
        string inputFolder = Path.Combine(baseDir, "PdfInputs");
        string outputFolder = Path.Combine(baseDir, "TiffOutputs");

        // If the expected input folder does not exist, fall back to the base directory
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' not found. Using base directory as input.");
            inputFolder = baseDir;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Retrieve all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to convert.");
            return;
        }

        // TiffDevice does NOT implement IDisposable – instantiate once and reuse
        TiffDevice tiffDevice = new TiffDevice();

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Build the output TIFF file path (same name, .tiff extension)
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string tiffPath = Path.Combine(outputFolder, fileNameWithoutExt + ".tiff");

                // Load the PDF document
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Create the output stream and let TiffDevice write to it
                    using (FileStream outStream = new FileStream(tiffPath, FileMode.Create, FileAccess.Write))
                    {
                        // Convert the whole document (pages 1 .. pdfDoc.Pages.Count)
                        tiffDevice.Process(pdfDoc, 1, pdfDoc.Pages.Count, outStream);
                    }
                }

                Console.WriteLine($"Converted '{pdfPath}' → '{tiffPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to convert '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
