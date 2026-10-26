using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where JPEG images will be saved
        const string outputFolder = "OutputImages";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output root folder exists
        Directory.CreateDirectory(outputFolder);

        // Retrieve all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Create a subdirectory for each PDF to hold its page images
            string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
            string pdfOutputDir = Path.Combine(outputFolder, pdfName);
            Directory.CreateDirectory(pdfOutputDir);

            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // JpegDevice with desired resolution (DPI) and quality (0‑100)
                JpegDevice jpegDevice = new JpegDevice(150, 90);

                // Pages are 1‑based; iterate through each page and save as JPEG
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    string imagePath = Path.Combine(pdfOutputDir, $"Page_{pageNum}.jpg");
                    using (FileStream imageStream = new FileStream(imagePath, FileMode.Create))
                    {
                        jpegDevice.Process(pdfDoc.Pages[pageNum], imageStream);
                    }
                }
            }

            Console.WriteLine($"Converted '{pdfPath}' to JPEG images in '{pdfOutputDir}'.");
        }
    }
}