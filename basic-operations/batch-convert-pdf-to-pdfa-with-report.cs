using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class BatchPdfAConverter
{
    static void Main()
    {
        // Input folder containing source PDFs
        const string inputFolder = @"InputPdfs";
        // Output folder for converted PDF/A‑1b files
        const string outputFolder = @"OutputPdfA";
        // Path for the summary report
        const string reportPath = @"ConversionReport.txt";

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // StringBuilder to accumulate report lines
        StringBuilder reportBuilder = new StringBuilder();
        reportBuilder.AppendLine("FileName\tStatus\tOriginalSize(Bytes)\tConvertedSize(Bytes)\tSizeReduction(%)\tMessage");

        // Process each PDF file in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(sourcePath);
            try
            {
                // Open source PDF inside a using block for deterministic disposal
                using (Document doc = new Document(sourcePath))
                {
                    // Record original file size
                    long originalSize = new FileInfo(sourcePath).Length;

                    // Set conversion options: PDF/A‑1b with high compression
                    PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
                    {
                        OptimizeFileSize = true
                    };

                    // Perform the conversion
                    doc.Convert(convOptions);

                    // Determine output file path
                    string outputPath = Path.Combine(outputFolder,
                        Path.GetFileNameWithoutExtension(sourcePath) + "_pdfa.pdf");

                    // Save the converted PDF/A‑1b document
                    doc.Save(outputPath);

                    // Record new file size
                    long newSize = new FileInfo(outputPath).Length;

                    // Calculate size reduction percentage
                    double reduction = (originalSize - newSize) / (double)originalSize * 100.0;

                    // Append success line to report
                    reportBuilder.AppendLine($"{fileName}\tSuccess\t{originalSize}\t{newSize}\t{reduction:F2}%\t");
                }
            }
            catch (Exception ex)
            {
                // Append failure line to report with error message
                reportBuilder.AppendLine($"{fileName}\tFailed\t0\t0\t0%\t{ex.Message}");
            }
        }

        // Write the summary report to disk
        try
        {
            File.WriteAllText(reportPath, reportBuilder.ToString());
            Console.WriteLine($"Conversion completed. Report saved to '{reportPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write report: {ex.Message}");
        }
    }
}