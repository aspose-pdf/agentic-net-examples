using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class BatchPdfAConverter
{
    static void Main()
    {
        // Input folder containing PDFs to convert
        const string inputFolder = "InputPdfs";
        // Output folder for converted PDF/A files
        const string outputFolder = "OutputPdfA";
        // Path for the summary report
        const string reportPath = "ConversionReport.txt";

        // Verify that the input folder exists; if not, inform the user and exit gracefully.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Please create it and place PDF files to convert.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Prepare a StringBuilder for the report
        var reportBuilder = new StringBuilder();
        reportBuilder.AppendLine("FileName,OriginalSize(Bytes),ConvertedSize(Bytes),Status");

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Nothing to convert.");
            // Still write an empty report so the expected file exists.
            File.WriteAllText(reportPath, reportBuilder.ToString());
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(
                outputFolder,
                Path.GetFileNameWithoutExtension(fileName) + "_PDF_A_1b.pdf");

            try
            {
                // Load the source PDF
                using (var doc = new Document(inputPath))
                {
                    // Create conversion options for PDF/A‑1b with high compression
                    var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
                    {
                        // Delete objects that cannot be converted
                        ErrorAction = ConvertErrorAction.Delete,
                        // Enable the special mode that reduces file size
                        OptimizeFileSize = true
                    };

                    // Perform the conversion
                    bool conversionResult = doc.Convert(conversionOptions);
                    // (Optional) you can inspect conversionResult if needed

                    // Additional resource optimization (compression) if needed
                    doc.OptimizeResources();

                    // Save the converted document
                    doc.Save(outputPath);
                }

                // Gather file size information
                long originalSize = new FileInfo(inputPath).Length;
                long convertedSize = new FileInfo(outputPath).Length;

                // Append success entry to the report
                reportBuilder.AppendLine($"{fileName},{originalSize},{convertedSize},Success");
            }
            catch (Exception ex)
            {
                // On error, record the failure in the report
                reportBuilder.AppendLine($"{fileName},,,Failed: {ex.Message}");
            }
        }

        // Write the summary report to disk
        File.WriteAllText(reportPath, reportBuilder.ToString());

        Console.WriteLine($"Batch conversion completed. Report saved to {reportPath}");
    }
}
