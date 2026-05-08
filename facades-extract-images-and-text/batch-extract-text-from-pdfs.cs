using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDF files
        const string inputFolder = "InputPdfs";
        // Folder where extracted text files will be saved
        const string outputFolder = "ExtractedText";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string txtPath = Path.Combine(outputFolder, fileNameWithoutExt + ".txt");

            try
            {
                // Use PdfExtractor to extract text from the PDF
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);      // Initialize the facade with the PDF file
                    extractor.ExtractText();         // Extract text from all pages
                    extractor.GetText(txtPath);      // Save extracted text to a .txt file
                }

                Console.WriteLine($"Extracted text saved to: {txtPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}