using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDF files to process
        const string folderPath = "C:\\PdfFolder";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Get all PDF files in the folder (case‑insensitive)
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the specified folder.");
            return;
        }

        foreach (string pdfFilePath in pdfFiles)
        {
            // Build the output text file name (same base name, .txt extension)
            string txtFileName = Path.ChangeExtension(Path.GetFileName(pdfFilePath), ".txt");
            string txtFilePath = Path.Combine(folderPath, txtFileName);

            // Extract text from the current PDF
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(pdfFilePath);
            extractor.ExtractText();
            extractor.GetText(txtFilePath);

            Console.WriteLine($"Extracted text from '{Path.GetFileName(pdfFilePath)}' to '{txtFileName}'.");
        }
    }
}