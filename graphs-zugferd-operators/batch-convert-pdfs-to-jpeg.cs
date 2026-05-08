using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Base directory of the running application (cross‑platform)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Input and output directories are resolved relative to the base directory
        string inputDirectory = Path.Combine(baseDir, "PdfInput");
        string outputDirectory = Path.Combine(baseDir, "JpegOutput");

        // Ensure the directories exist (creates them if they are missing)
        Directory.CreateDirectory(inputDirectory);
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Base file name without extension (e.g., "Report")
            string baseFileName = Path.GetFileNameWithoutExtension(pdfPath);

            // Open the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(pdfPath))
            {
                // 300 DPI gives good quality JPEGs
                Resolution resolution = new Resolution(300);
                JpegDevice jpegDevice = new JpegDevice(resolution);

                // Aspose.Pdf uses 1‑based page indexing
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    string jpegFileName = $"{baseFileName}_page{pageNumber}.jpeg";
                    string jpegPath = Path.Combine(outputDirectory, jpegFileName);

                    // Convert the current page to JPEG and write it to a file stream
                    using (FileStream jpegStream = new FileStream(jpegPath, FileMode.Create))
                    {
                        jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                    }
                }
            }

            Console.WriteLine($"Converted '{pdfPath}' to JPEG images.");
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
