using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputDir = "input-pdfs";
        string outputDir = "output-pdfa";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDir, fileName + ".pdf");

            try
            {
                using (Document doc = new Document(inputPath))
                {
                    string logPath = Path.Combine(outputDir, fileName + "_conversion.log");
                    doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                    doc.Save(outputPath);
                }
                Console.WriteLine($"Converted: {fileName}.pdf");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to convert '{inputPath}': {ex.Message}");
            }
        }
    }
}