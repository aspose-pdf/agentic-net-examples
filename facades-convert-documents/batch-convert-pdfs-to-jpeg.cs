using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Ensure the output root folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            // Create a sub‑folder for each PDF to keep its pages separate
            string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
            string pdfOutputDir = Path.Combine(outputFolder, pdfName);
            Directory.CreateDirectory(pdfOutputDir);

            // Convert each page of the current PDF to a JPEG image
            using (PdfConverter converter = new PdfConverter())
            {
                // Load the PDF
                converter.BindPdf(pdfPath);
                // Prepare the converter
                converter.DoConvert();

                int pageIndex = 1;
                while (converter.HasNextImage())
                {
                    // Default image format is JPEG, so no ImageFormat enum is needed
                    string imagePath = Path.Combine(pdfOutputDir, $"page_{pageIndex}.jpg");
                    converter.GetNextImage(imagePath);
                    pageIndex++;
                }
            }

            Console.WriteLine($"Converted '{pdfPath}' to {pdfFiles.Length} JPEG images.");
        }
    }
}