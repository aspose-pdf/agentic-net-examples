using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing source PDF files
        const string inputDir = @"C:\InputPdfs";
        // Directory where JPEG images will be saved
        const string outputDir = @"C:\OutputJpegs";

        // Verify input directory exists
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input directory.");
            return;
        }

        // Desired image resolution (e.g., 300 DPI)
        Resolution resolution = new Resolution(300);
        // Reuse a single JpegDevice instance for all conversions
        JpegDevice jpegDevice = new JpegDevice(resolution);

        foreach (string pdfPath in pdfFiles)
        {
            string pdfBaseName = Path.GetFileNameWithoutExtension(pdfPath);

            try
            {
                // Load the PDF document (using statement ensures proper disposal)
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Iterate pages using 1‑based indexing
                    for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                    {
                        // Build custom output file name: <pdfname>_page<page>_out.jpeg
                        string jpegFile = Path.Combine(
                            outputDir,
                            $"{pdfBaseName}_page{pageNum}_out.jpeg");

                        // Convert the page to JPEG and write to file
                        using (FileStream jpegStream = new FileStream(jpegFile, FileMode.Create))
                        {
                            jpegDevice.Process(pdfDoc.Pages[pageNum], jpegStream);
                        }
                    }
                }

                Console.WriteLine($"Successfully converted: {pdfBaseName}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}