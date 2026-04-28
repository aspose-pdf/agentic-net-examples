using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = "InputPdfs";
        // Folder where JPEG images will be saved
        const string outputFolder = "OutputImages";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            // Base name without extension for naming the images
            string pdfBaseName = Path.GetFileNameWithoutExtension(pdfPath);

            // Load the PDF document (using block ensures proper disposal)
            using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(pdfPath))
            {
                // Create a JPEG device with desired resolution and quality
                // Note: JpegDevice does NOT implement IDisposable, so we instantiate it directly
                Aspose.Pdf.Devices.JpegDevice jpegDevice = new Aspose.Pdf.Devices.JpegDevice(
                    new Aspose.Pdf.Devices.Resolution(150), // DPI
                    90);                                      // JPEG quality (0‑100)

                // Iterate through pages (1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Build the output file name using a custom pattern
                    string outputPath = Path.Combine(
                        outputFolder,
                        $"{pdfBaseName}_page{pageNumber}.jpg");

                    // Write the page image to a file stream
                    using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        jpegDevice.Process(pdfDocument.Pages[pageNumber], outStream);
                    }
                }
            }

            Console.WriteLine($"Converted PDF: {pdfPath}");
        }

        Console.WriteLine("Batch conversion to JPEG completed.");
    }
}