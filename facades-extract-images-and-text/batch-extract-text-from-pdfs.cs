using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input and output directories – can be supplied as command‑line arguments or fall back to defaults
        string inputDirectory  = args.Length > 0 ? args[0] : @"C:\PDFs";
        string outputDirectory = args.Length > 1 ? args[1] : @"C:\ExtractedText";

        // Validate input directory
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDirectory}");
            return;
        }

        // Ensure the output directory exists
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        // Get all PDF files in the input directory (non‑recursive – change SearchOption if needed)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            string txtPath = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(pdfPath) + ".txt");

            // PdfExtractor implements IDisposable, so we wrap it in a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);   // Load the PDF
                extractor.ExtractText();      // Extract all text
                extractor.GetText(txtPath);   // Write the extracted text to a .txt file
            }

            Console.WriteLine($"Extracted text from '{Path.GetFileName(pdfPath)}' to '{txtPath}'.");
        }
    }
}