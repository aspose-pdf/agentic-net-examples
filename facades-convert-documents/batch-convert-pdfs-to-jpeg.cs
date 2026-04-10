using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Resolution class

class PdfToJpegBatchConverter
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Folder where JPEG images will be saved
        const string outputFolder = @"C:\OutputJpegs";

        // Verify that the input folder exists; if not, inform the user and exit gracefully.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: '{inputFolder}'. Please create the folder and place PDF files inside it.");
            return;
        }

        // Ensure the output root folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Create a subfolder for images of the current PDF
            string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
            string pdfOutputDir = Path.Combine(outputFolder, pdfName);
            Directory.CreateDirectory(pdfOutputDir);

            // Use PdfConverter (a Facade) to convert pages to JPEG images
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF file to the converter
                converter.BindPdf(pdfPath);

                // Optional: set a higher resolution for better image quality
                converter.Resolution = new Resolution(300);

                // Prepare the converter for image extraction
                converter.DoConvert();

                int pageIndex = 1;
                // Extract each page as a JPEG image
                while (converter.HasNextImage())
                {
                    string imagePath = Path.Combine(pdfOutputDir, $"page_{pageIndex}.jpg");
                    // GetNextImage saves the image in JPEG format by default
                    converter.GetNextImage(imagePath);
                    pageIndex++;
                }
            }

            Console.WriteLine($"Converted '{pdfPath}' to JPEG images in '{pdfOutputDir}'.");
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
