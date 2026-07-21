using System;
using System.IO;
using Aspose.Pdf;

class BatchPdfAConverter
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Folder where converted PDF/A‑1b files will be saved
        const string outputFolder = @"C:\OutputPdfA";
        // Path to the CSV log file
        const string csvLogPath = @"C:\ConversionResults.csv";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        // Open CSV file for writing
        using (var csvWriter = new StreamWriter(csvLogPath, false))
        {
            // Write CSV header
            csvWriter.WriteLine("InputFile,OutputFile,Success,LogFile");

            foreach (string inputPath in pdfFiles)
            {
                // Build output PDF/A file name
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + "_PDF_A_1b.pdf");

                // Log file for this conversion
                string logPath = Path.Combine(outputFolder, fileNameWithoutExt + "_conversion.log");

                bool success = false;

                try
                {
                    // Load source PDF
                    using (Document doc = new Document(inputPath))
                    {
                        // Convert to PDF/A‑1b, errors are written to logPath
                        success = doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                        // Save the converted document (even if conversion reported false,
                        // the document may still contain partial results)
                        doc.Save(outputPath);
                    }
                }
                catch (Exception ex)
                {
                    // If an exception occurs, treat as failure and write exception message to log
                    success = false;
                    File.AppendAllText(logPath, $"Exception: {ex.Message}{Environment.NewLine}");
                }

                // Write result line to CSV
                csvWriter.WriteLine($"{EscapeCsv(inputPath)},{EscapeCsv(outputPath)},{success},{EscapeCsv(logPath)}");
            }
        }

        Console.WriteLine("Batch conversion completed. Results written to: " + csvLogPath);
    }

    // Helper to escape commas and quotes in CSV fields
    private static string EscapeCsv(string field)
    {
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}