using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

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

        // Prepare report header
        var reportLines = new List<string>
        {
            "SourceFile,OutputFile,OriginalSizeBytes,ConvertedSizeBytes,Success,Message"
        };

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string sourcePath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(sourcePath);
            string outputPath = Path.Combine(outputFolder, fileName + "_PDF_A_1b.pdf");

            bool success = false;
            string message = string.Empty;
            long originalSize = 0;
            long convertedSize = 0;

            try
            {
                originalSize = new FileInfo(sourcePath).Length;

                // Load the source PDF (lifecycle rule: use constructor with path)
                using (Document doc = new Document(sourcePath))
                {
                    // Configure conversion options for PDF/A‑1b with high compression
                    PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
                    options.OptimizeFileSize = true;                     // high compression
                    options.ErrorAction = ConvertErrorAction.Delete;     // skip objects that cannot be converted

                    // Perform the conversion (PDF/A‑1b)
                    bool conversionResult = doc.Convert(options);
                    if (!conversionResult)
                    {
                        throw new InvalidOperationException("Conversion returned false.");
                    }

                    // Save the converted document (still a PDF, so no SaveOptions needed)
                    doc.Save(outputPath);
                }

                convertedSize = new FileInfo(outputPath).Length;
                success = true;
                message = "Converted successfully";
            }
            catch (Exception ex)
            {
                // In case of any error, capture the message
                success = false;
                message = ex.Message;
            }

            // Write per‑file result to console
            Console.WriteLine($"{fileName}: {(success ? "OK" : "FAIL")} - {message}");

            // Append line to the report
            reportLines.Add($"{sourcePath},{outputPath},{originalSize},{convertedSize},{success},{message}");
        }

        // Write the summary report to disk
        try
        {
            File.WriteAllLines(reportPath, reportLines);
            Console.WriteLine($"Summary report written to: {reportPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write report: {ex.Message}");
        }
    }
}