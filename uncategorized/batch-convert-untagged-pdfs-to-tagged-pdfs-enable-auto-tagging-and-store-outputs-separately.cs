using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputFolder  = "input-pdfs";   // folder with source PDFs
        const string outputFolder = "tagged-pdfs"; // folder for tagged PDFs

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Enable global auto‑tagging settings (static singleton)
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                using (Document doc = new Document(inputPath))
                {
                    // Convert using PDF/A format to trigger auto‑tagging
                    var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
                    {
                        AutoTaggingSettings = AutoTaggingSettings.Default
                    };
                    doc.Convert(conversionOptions);

                    string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_tagged.pdf";
                    string outputPath = Path.Combine(outputFolder, outputFileName);
                    doc.Save(outputPath);
                    Console.WriteLine($"Tagged PDF saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{inputPath}': {ex.Message}");
            }
        }
    }
}