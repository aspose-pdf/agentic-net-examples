using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where PDF/A‑1b files will be written
        const string outputFolder = "OutputPdfA";
        // Path for the summary report
        const string reportPath = "ConversionReport.txt";

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the input folder exists before enumerating files
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No files to process.");
            // Still create an empty report so downstream steps do not fail
            File.WriteAllText(reportPath, "FileName,InputSize(Bytes),OutputSize(Bytes),Status,Message\n");
            return;
        }

        StringBuilder report = new StringBuilder();
        report.AppendLine("FileName,InputSize(Bytes),OutputSize(Bytes),Status,Message");

        foreach (string inputFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputFile);
            string outputFile = Path.Combine(outputFolder,
                Path.GetFileNameWithoutExtension(fileName) + "_PDF_A_1b.pdf");

            try
            {
                long inputSize = new FileInfo(inputFile).Length;

                // Load the source PDF
                using (Document doc = new Document(inputFile))
                {
                    // Configure conversion to PDF/A‑1b with high compression
                    PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
                    {
                        OptimizeFileSize = true,
                        // Optional per‑file log (can be omitted if not needed)
                        LogFileName = Path.Combine(outputFolder, fileName + ".log")
                    };

                    // Perform the conversion
                    doc.Convert(options);

                    // Save the resulting PDF/A‑1b document
                    doc.Save(outputFile);
                }

                long outputSize = new FileInfo(outputFile).Length;
                report.AppendLine($"{fileName},{inputSize},{outputSize},Success,");
            }
            catch (Exception ex)
            {
                // Record failure details
                string safeMessage = ex.Message.Replace("\"", "\"\"");
                report.AppendLine($"{fileName},,,Failed,\"{safeMessage}\"");
            }
        }

        // Write the summary report
        File.WriteAllText(reportPath, report.ToString());
        Console.WriteLine($"Batch conversion completed. Report saved to {reportPath}");
    }
}
