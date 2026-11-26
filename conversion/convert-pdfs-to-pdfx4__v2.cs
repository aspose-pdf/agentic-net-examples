using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing source PDFs
        const string sourceDir = @"directoryB";
        // Directory where converted PDFs will be saved
        const string outputDir = @"directoryB_Converted";

        if (!Directory.Exists(sourceDir))
        {
            Console.Error.WriteLine($"Source directory not found: {sourceDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Enumerate all PDF files in the source directory
        string[] pdfFiles = Directory.GetFiles(sourceDir, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to convert.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Build output file name (same name with suffix _pdfx4.pdf)
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDir, $"{fileNameWithoutExt}_pdfx4.pdf");

                // Load the source PDF inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // Convert to PDF/X-4 using the default output‑intent profile.
                    // The overload writes a conversion log (optional) and applies the requested format.
                    string conversionLog = Path.Combine(outputDir, $"{fileNameWithoutExt}_conversion_log.xml");
                    doc.Convert(conversionLog, PdfFormat.PDF_X_4, ConvertErrorAction.Delete);

                    // Save the converted document as PDF/X-4
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Converted: {Path.GetFileName(inputPath)} → {Path.GetFileName(outputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("All conversions completed.");
    }
}