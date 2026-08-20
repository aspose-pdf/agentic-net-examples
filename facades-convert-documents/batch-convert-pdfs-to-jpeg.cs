using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

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

        // Ensure the output root folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            // Create a subfolder for each PDF to keep its pages separate
            string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
            string pdfOutputDir = Path.Combine(outputFolder, pdfName);
            Directory.CreateDirectory(pdfOutputDir);

            // Use PdfConverter from Aspose.Pdf.Facades to convert pages to JPEG
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF file to the converter
                converter.BindPdf(pdfPath);
                // Prepare the converter for image extraction
                converter.DoConvert();

                int pageIndex = 1;
                // Extract each page as a JPEG image
                while (converter.HasNextImage())
                {
                    string imagePath = Path.Combine(pdfOutputDir, $"page_{pageIndex}.jpg");
                    // Save the next image using the JPEG format (System.Drawing.Imaging.ImageFormat)
                    converter.GetNextImage(imagePath, ImageFormat.Jpeg);
                    pageIndex++;
                }
            }

            Console.WriteLine($"Converted '{pdfPath}' to images in '{pdfOutputDir}'.");
        }
    }
}
