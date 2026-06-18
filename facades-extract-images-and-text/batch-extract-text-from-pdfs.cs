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
        const string outputFolder = "ExtractedTexts";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string txtPath = Path.Combine(outputFolder, baseName + ".txt");

            try
            {
                // Use PdfExtractor to bind the PDF, extract text, and save it to a .txt file
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);
                    extractor.ExtractText();               // Unicode extraction of all pages
                    extractor.GetText(txtPath);            // Write extracted text to file
                }

                Console.WriteLine($"Extracted: {pdfPath} -> {txtPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}