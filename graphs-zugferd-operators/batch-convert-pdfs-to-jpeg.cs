using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing the source PDFs
        const string sourceDir = @"C:\PdfFolder";
        // Output directory for JPEG images
        const string outputDir = @"C:\PdfFolder\JpegOutput";

        if (!Directory.Exists(sourceDir))
        {
            Console.Error.WriteLine($"Source directory does not exist: {sourceDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the source directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(sourceDir, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);

            try
            {
                // Load the PDF inside a using block for deterministic disposal
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Iterate pages (Aspose.Pdf uses 1‑based indexing)
                    for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                    {
                        // Prepare the JPEG device – 300 DPI gives good quality
                        JpegDevice jpegDevice = new JpegDevice(new Resolution(300));

                        // Build output file name: <source>_page<pageNum>.jpg
                        string jpegPath = Path.Combine(
                            outputDir,
                            $"{fileNameWithoutExt}_page{pageNum}.jpg");

                        // Save each page as a JPEG image
                        using (FileStream imageStream = new FileStream(jpegPath, FileMode.Create))
                        {
                            jpegDevice.Process(pdfDoc.Pages[pageNum], imageStream);
                        }

                        Console.WriteLine($"Saved: {jpegPath}");
                    }
                }
            }
            catch (TypeInitializationException)
            {
                // JPEG conversion may require GDI+ (Windows only)
                Console.WriteLine($"Skipping '{pdfPath}' – JPEG conversion requires Windows (GDI+).");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}