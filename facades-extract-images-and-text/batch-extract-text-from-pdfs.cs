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
        const string outputFolder = @"C:\PdfTextOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Build the output text file path (same name, .txt extension)
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string txtPath = Path.Combine(outputFolder, fileNameWithoutExt + ".txt");

            try
            {
                // Use PdfExtractor facade to extract text
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the PDF file to the extractor
                    extractor.BindPdf(pdfPath);

                    // Extract text using Unicode encoding (covers most languages)
                    extractor.ExtractText(Encoding.Unicode);

                    // Save the extracted text to the .txt file
                    extractor.GetText(txtPath);
                }

                Console.WriteLine($"Extracted text from '{pdfPath}' to '{txtPath}'.");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch extraction completed.");
    }
}