using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class BatchPdfAConverter
{
    static void Main()
    {
        // Input folder containing source PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Output folder for PDF/A‑1b files
        const string outputFolder = @"C:\OutputPdfA";
        // Path for the summary report
        const string reportPath = @"C:\ConversionReport.txt";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // StringBuilder to collect report lines
        StringBuilder reportBuilder = new StringBuilder();
        reportBuilder.AppendLine("SourceFile\tOutputFile\tOriginalSize(Bytes)\tConvertedSize(Bytes)\tReduction(%)\tStatus");

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string sourcePath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(sourcePath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_pdfa.pdf");

            try
            {
                // Load the source PDF inside a using block (ensures disposal)
                using (Document doc = new Document(sourcePath))
                {
                    // Configure conversion options for PDF/A‑1b with high compression
                    PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B, ConvertErrorAction.Delete)
                    {
                        OptimizeFileSize = true   // enable high‑compression mode
                    };

                    // Perform the conversion; returns true if successful
                    bool conversionResult = doc.Convert(convOptions);

                    // Save the converted document
                    doc.Save(outputPath);

                    // Gather size information
                    long originalSize = new FileInfo(sourcePath).Length;
                    long convertedSize = new FileInfo(outputPath).Length;
                    double reduction = originalSize > 0
                        ? 100.0 * (originalSize - convertedSize) / originalSize
                        : 0.0;

                    // Append success line to the report
                    reportBuilder.AppendLine($"{sourcePath}\t{outputPath}\t{originalSize}\t{convertedSize}\t{reduction:F2}\t{(conversionResult ? "Success" : "Partial")}");
                }
            }
            catch (Exception ex)
            {
                // In case of any error, log it and continue with next file
                reportBuilder.AppendLine($"{sourcePath}\t{outputPath}\t-\t-\t-\tError: {ex.Message}");
            }
        }

        // Write the summary report to disk
        File.WriteAllText(reportPath, reportBuilder.ToString(), Encoding.UTF8);
        Console.WriteLine($"Batch conversion completed. Report saved to '{reportPath}'.");
    }
}