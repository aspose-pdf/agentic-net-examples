using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class PdfToJpegBatchConverter
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Folder where JPEG images will be saved
        const string outputFolder = @"C:\OutputJpegs";

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Enumerate all PDF files in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Create a subfolder for each PDF to keep its pages separate
            string pdfNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string pdfOutputDir = Path.Combine(outputFolder, pdfNameWithoutExt);
            Directory.CreateDirectory(pdfOutputDir);

            // Use PdfConverter from Aspose.Pdf.Facades to convert pages to JPEG
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF file to the converter
                converter.BindPdf(pdfPath);
                // Prepare the converter for image extraction
                converter.DoConvert();

                int pageIndex = 1;
                // Loop through all pages and save each as a JPEG image
                while (converter.HasNextImage())
                {
                    string jpegPath = Path.Combine(pdfOutputDir, $"page_{pageIndex}.jpg");
                    // GetNextImage saves the image in JPEG format by default
                    converter.GetNextImage(jpegPath);
                    pageIndex++;
                }

                // Release resources held by the converter
                converter.Close();
            }

            Console.WriteLine($"Converted '{pdfPath}' to JPEG images in '{pdfOutputDir}'.");
        }

        Console.WriteLine("Batch conversion completed.");
    }
}