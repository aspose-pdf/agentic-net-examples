using System;
using System.IO;
using Aspose.Pdf;

class BatchPdfAConverter
{
    static void Main()
    {
        // Input folder containing source PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where converted PDF/A‑1b files will be saved
        const string outputFolder = @"C:\OutputPdfA";
        // Folder for individual conversion log files
        const string logFolder = @"C:\ConversionLogs";
        // Path to the CSV file that will contain the batch results
        const string csvReportPath = @"C:\ConversionReport\conversion_results.csv";

        // Ensure output directories exist
        Directory.CreateDirectory(outputFolder);
        Directory.CreateDirectory(logFolder);
        Directory.CreateDirectory(Path.GetDirectoryName(csvReportPath));

        // Prepare CSV file with header
        using (StreamWriter csvWriter = new StreamWriter(csvReportPath, false))
        {
            csvWriter.WriteLine("InputFile,OutputFile,Success,LogFile,ErrorMessage");
        }

        // Process each PDF file in the input folder
        foreach (string inputFilePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputFilePath);
            string outputFilePath = Path.Combine(outputFolder, fileNameWithoutExt + "_pdfa.pdf");
            string logFilePath = Path.Combine(logFolder, fileNameWithoutExt + "_log.txt");

            bool conversionSuccess = false;
            string errorMessage = string.Empty;

            try
            {
                // Load the source PDF
                using (Document doc = new Document(inputFilePath))
                {
                    // Convert to PDF/A‑1b, logging conversion details
                    conversionSuccess = doc.Convert(logFilePath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                    // Save the converted document
                    doc.Save(outputFilePath);
                }
            }
            catch (Exception ex)
            {
                // Capture any exception details
                conversionSuccess = false;
                errorMessage = ex.Message;
            }

            // Append the result to the CSV report
            using (StreamWriter csvWriter = new StreamWriter(csvReportPath, true))
            {
                // Escape commas in paths if they ever appear
                string escapedInput = $"\"{inputFilePath}\"";
                string escapedOutput = $"\"{outputFilePath}\"";
                string escapedLog = $"\"{logFilePath}\"";
                string escapedError = $"\"{errorMessage}\"";

                csvWriter.WriteLine($"{escapedInput},{escapedOutput},{conversionSuccess},{escapedLog},{escapedError}");
            }
        }

        Console.WriteLine("Batch conversion completed. Report written to: " + csvReportPath);
    }
}