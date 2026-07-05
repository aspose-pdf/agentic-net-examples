using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Not required but kept for completeness

class BatchPdfAConverter
{
    static void Main()
    {
        // Input folder containing source PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Output folder for PDF/A‑1b files
        const string outputFolder = @"C:\OutputPdfA";
        // Path for the summary report (CSV format)
        const string reportPath = @"C:\ConversionReport.csv";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Prepare a StringBuilder for the report
        StringBuilder reportBuilder = new StringBuilder();
        reportBuilder.AppendLine("SourceFile,OutputFile,OriginalSizeBytes,ConvertedSizeBytes,SizeReductionPercent,Status");

        // Enumerate all PDF files in the input folder
        foreach (string sourcePath in Directory.EnumerateFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(sourcePath);
            string outputPath = Path.Combine(outputFolder, fileName + "_PDF_A_1b.pdf");

            try
            {
                // Load the source PDF inside a using block for deterministic disposal
                using (Document doc = new Document(sourcePath))
                {
                    // Configure conversion options for PDF/A‑1b with high compression
                    PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
                    {
                        // Enable the special mode that reduces file size (optimizes fonts, etc.)
                        OptimizeFileSize = true,
                        // Remove objects that cannot be converted to keep the process robust
                        ErrorAction = ConvertErrorAction.Delete
                    };

                    // Perform the conversion; the method returns true on success
                    bool conversionResult = doc.Convert(options);

                    if (!conversionResult)
                    {
                        // If conversion failed, record the status and continue
                        reportBuilder.AppendLine($"{sourcePath},{outputPath},,,," + "ConversionFailed");
                        continue;
                    }

                    // Additional resource optimization (removes unused objects, merges duplicates)
                    doc.OptimizeResources();

                    // Save the converted document as PDF/A‑1b
                    doc.Save(outputPath);
                }

                // Gather file size information for the report
                long originalSize = new FileInfo(sourcePath).Length;
                long convertedSize = new FileInfo(outputPath).Length;
                double reductionPercent = originalSize > 0
                    ? 100.0 * (originalSize - convertedSize) / originalSize
                    : 0.0;

                // Record a successful entry
                reportBuilder.AppendLine($"{sourcePath},{outputPath},{originalSize},{convertedSize},{reductionPercent:F2},Success");
            }
            catch (Exception ex)
            {
                // Record any exception that occurred during processing
                reportBuilder.AppendLine($"{sourcePath},{outputPath},,,," + $"Error:{ex.Message}");
            }
        }

        // Write the summary report to the specified file
        File.WriteAllText(reportPath, reportBuilder.ToString());
        Console.WriteLine($"Batch conversion completed. Report saved to: {reportPath}");
    }
}