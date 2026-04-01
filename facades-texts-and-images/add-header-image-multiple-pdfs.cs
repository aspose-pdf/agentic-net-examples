using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        string[] pdfFiles = new string[] { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Path to the header image file
        string headerImagePath = "header.jpg";
        // Top margin for the header image (in points)
        float topMargin = 50f;

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Output file name – original name with suffix
            string outputPath = Path.GetFileNameWithoutExtension(pdfPath) + "_header.pdf";

            using (PdfFileStamp stamp = new PdfFileStamp())
            {
                // Load the source PDF
                stamp.BindPdf(pdfPath);
                // Add the header image to each page
                stamp.AddHeader(headerImagePath, topMargin);
                // Save the modified PDF
                stamp.Save(outputPath);
                // Close the facade
                stamp.Close();
            }

            Console.WriteLine($"Header added: {outputPath}");
        }

        Console.WriteLine("Processing completed.");
    }
}