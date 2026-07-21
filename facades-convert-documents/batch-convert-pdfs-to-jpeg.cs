using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Folder where JPEG images will be saved
        const string outputFolder = @"C:\OutputImages";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Use PdfConverter inside a using block for deterministic disposal
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the current PDF file to the converter
                converter.BindPdf(pdfPath);
                // Perform initial conversion setup
                converter.DoConvert();

                // Base name for output images (e.g., "sample" from "sample.pdf")
                string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                int pageNumber = 1;

                // Loop through all pages and save each as a JPEG image
                while (converter.HasNextImage())
                {
                    string outputFile = Path.Combine(outputFolder,
                        $"{baseName}_page{pageNumber}.jpg");

                    // GetNextImage saves the image in JPEG format by default
                    converter.GetNextImage(outputFile);
                    pageNumber++;
                }
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}