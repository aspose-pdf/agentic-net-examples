using System;
using System.IO;
using Aspose.Pdf;

class BatchPdfAConverter
{
    static void Main()
    {
        // Input folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Output folder for PDF/A‑1b files
        const string outputFolder = "PdfA_Output";
        // CSV file that will contain conversion results
        const string csvReportPath = "conversion_results.csv";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Ensure input directory exists – if it does not, create it and inform the user
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine($"Input folder '{inputFolder}' was not found and has been created. Place PDF files there and rerun the program.");
            // No files to process, exit gracefully
            return;
        }

        // Prepare CSV file with header
        using (StreamWriter csvWriter = new StreamWriter(csvReportPath, false))
        {
            csvWriter.WriteLine("SourceFile,TargetFile,Success");
        }

        // Process each PDF file in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build target file name (e.g., original name + "_pdfa.pdf")
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(sourcePath);
            string targetPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_pdfa.pdf");

            // Temporary log file for Aspose.Pdf conversion errors
            string conversionLogPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_convert.log");

            bool conversionSuccess = false;

            try
            {
                // Load, convert, and save the document
                using (Document doc = new Document(sourcePath))
                {
                    // Convert to PDF/A‑1b, logging errors to the specified file
                    conversionSuccess = doc.Convert(conversionLogPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                    // Save the converted document only if conversion succeeded
                    if (conversionSuccess)
                    {
                        doc.Save(targetPath);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception to the conversion log file and mark as failure
                File.AppendAllText(conversionLogPath, $"Exception: {ex.Message}{Environment.NewLine}");
                conversionSuccess = false;
            }

            // Append result to CSV report
            using (StreamWriter csvWriter = new StreamWriter(csvReportPath, true))
            {
                csvWriter.WriteLine($"{sourcePath},{targetPath},{conversionSuccess}");
            }
        }

        Console.WriteLine("Batch conversion completed. Report written to " + csvReportPath);
    }
}
