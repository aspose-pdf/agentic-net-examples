using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class BatchPdfToPdfA
{
    static void Main()
    {
        // Paths – adjust as needed
        string inputFolder  = @"C:\PdfBatch\Input";
        string outputFolder = @"C:\PdfBatch\Output";
        string reportPath   = Path.Combine(outputFolder, "ConversionReport.csv");

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Prepare CSV header
        StringBuilder reportBuilder = new StringBuilder();
        reportBuilder.AppendLine("SourceFile,OutputFile,OriginalSizeBytes,ConvertedSizeBytes,Status,Message");

        // Process each PDF in the input folder
        foreach (string sourceFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(sourceFile);
            string outputFile = Path.Combine(outputFolder, $"{fileNameWithoutExt}_PDF_A1b.pdf");
            string logFile    = Path.Combine(outputFolder, $"{fileNameWithoutExt}_conversion.log");

            try
            {
                // Load source PDF
                using (Document doc = new Document(sourceFile))
                {
                    // Apply high compression / optimization (default options)
                    doc.OptimizeResources();

                    // Convert to PDF/A‑1b using Document.Convert (core API)
                    doc.Convert(logFile, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                    // Save the converted document
                    doc.Save(outputFile);
                }

                // Gather file size information
                long originalSize = new FileInfo(sourceFile).Length;
                long convertedSize = new FileInfo(outputFile).Length;

                // Record success in the report
                reportBuilder.AppendLine($"{Path.GetFileName(sourceFile)},{Path.GetFileName(outputFile)},{originalSize},{convertedSize},Success,");
            }
            catch (Exception ex)
            {
                // Record failure in the report
                reportBuilder.AppendLine($"{Path.GetFileName(sourceFile)},{Path.GetFileName(outputFile)},,,Failed,\"{ex.Message.Replace("\"", "\"\"")}\"");
            }
        }

        // Write the CSV report
        File.WriteAllText(reportPath, reportBuilder.ToString(), Encoding.UTF8);

        Console.WriteLine($"Batch conversion completed. Report saved to: {reportPath}");
    }
}
