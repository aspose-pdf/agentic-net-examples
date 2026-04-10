using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class BatchPdfTextExtractor
{
    static void Main()
    {
        // Folder containing PDF files to process
        const string inputFolder = @"C:\PdfInput";
        // Folder where extracted text files will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build the output text file path with the same base name
            string txtPath = Path.Combine(
                outputFolder,
                Path.GetFileNameWithoutExtension(pdfPath) + ".txt");

            // Use PdfExtractor (facade) to extract text
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor
                extractor.BindPdf(pdfPath);

                // Extract all text using Unicode encoding
                extractor.ExtractText(Encoding.Unicode);

                // Save the extracted text to the .txt file
                extractor.GetText(txtPath);
            }

            Console.WriteLine($"Extracted text from '{pdfPath}' to '{txtPath}'.");
        }

        Console.WriteLine("Batch extraction completed.");
    }
}