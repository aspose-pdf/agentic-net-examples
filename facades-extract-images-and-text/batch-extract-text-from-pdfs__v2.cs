using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Resolve input and output directories relative to the executable location.
        // This makes the script platform‑agnostic (works on Windows, Linux, macOS).
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputDir = Path.Combine(baseDir, "PdfInput");
        string outputDir = Path.Combine(baseDir, "PdfTextOutput");

        // Ensure the input directory exists; if not, inform the user and exit gracefully.
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Process each PDF file in the input directory.
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string txtPath = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(pdfPath) + ".txt");

            // Initialize PdfExtractor (uses the default constructor).
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor.
                extractor.BindPdf(pdfPath);
                // Extract all text from the PDF (Unicode encoding is default).
                extractor.ExtractText();
                // Save the extracted text to a .txt file.
                extractor.GetText(txtPath);
                // No need to call Close() explicitly – the using statement disposes the extractor.
            }
        }
    }
}
