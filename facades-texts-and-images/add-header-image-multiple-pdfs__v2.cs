using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to process (place the files in the executable directory)
        string[] pdfFiles = new string[] { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Path to the header image (JPEG, PNG, etc.)
        string headerImagePath = "header.jpg";

        if (!File.Exists(headerImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImagePath}");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPath}");
                continue;
            }

            // Output file name – original name with "_header" suffix
            string outputPath = Path.GetFileNameWithoutExtension(inputPath) + "_header.pdf";

            // Use PdfFileStamp to add the header image
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(inputPath);
            // Add the image as a header with a top margin of 20 points
            fileStamp.AddHeader(headerImagePath, 20f);
            fileStamp.Save(outputPath);
            fileStamp.Close();

            Console.WriteLine($"Added header to '{inputPath}' -> '{outputPath}'");
        }

        Console.WriteLine("Processing completed.");
    }
}