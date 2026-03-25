using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputDir = "input-pdfs";
        const string outputDir = "output-pdfs";
        const string reportPath = "conversion_report.txt";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);
        var reportBuilder = new StringBuilder();
        reportBuilder.AppendLine("FileName,OriginalSize(Bytes),ConvertedSize(Bytes),Status");

        foreach (string inputPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDir, $"{fileName}_pdfa.pdf");
            string logPath = Path.Combine(outputDir, $"{fileName}_log.txt");

            long originalSize = new FileInfo(inputPath).Length;
            try
            {
                using (Document doc = new Document(inputPath))
                {
                    // Convert to PDF/A‑1b, log conversion details
                    doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                    // Apply high compression by optimizing resources
                    doc.OptimizeResources();
                    // Save the PDF/A‑1b file
                    doc.Save(outputPath);
                }

                long convertedSize = new FileInfo(outputPath).Length;
                reportBuilder.AppendLine($"{fileName}.pdf,{originalSize},{convertedSize},Success");
            }
            catch (Exception ex)
            {
                reportBuilder.AppendLine($"{fileName}.pdf,{originalSize},0,Failed: {ex.Message.Replace(",", ";")}");
                // Optionally write the error log for this file
                File.WriteAllText(logPath, ex.ToString());
            }
        }

        File.WriteAllText(reportPath, reportBuilder.ToString());
        Console.WriteLine($"Conversion completed. Report saved to '{reportPath}'.");
    }
}