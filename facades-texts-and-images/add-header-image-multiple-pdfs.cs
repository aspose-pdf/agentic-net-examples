using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing input PDFs
        string inputDirectory = "input";
        // Path to the header image to be added
        string headerImagePath = "header.jpg";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        if (!File.Exists(headerImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImagePath}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = $"{fileNameWithoutExt}_header.pdf";

            // Use PdfFileStamp to add the header image
            using (PdfFileStamp stamp = new PdfFileStamp())
            {
                stamp.BindPdf(pdfPath);
                // Add header image with a top margin of 20 points
                stamp.AddHeader(headerImagePath, 20f);
                stamp.Save(outputPath);
                stamp.Close();
            }

            Console.WriteLine($"Processed: {outputPath}");
        }

        Console.WriteLine("All PDF files have been processed.");
    }
}