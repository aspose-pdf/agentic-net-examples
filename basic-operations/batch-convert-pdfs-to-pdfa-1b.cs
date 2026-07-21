using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputDirectory = "InputPdfs";
        const string outputDirectory = "OutputPdfA";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDirectory, fileNameWithoutExt + ".pdf");
            string logPath = Path.Combine(outputDirectory, fileNameWithoutExt + "_conversion.log");

            try
            {
                using (Document doc = new Document(inputPath))
                {
                    // Convert to PDF/A‑1b, log conversion issues, delete unconvertible objects
                    doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Converted: {fileNameWithoutExt}.pdf");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}