using System;
using System.IO;
using Aspose.Pdf;

class BatchPdfAConverter
{
    static void Main()
    {
        // Base directory of the application (works for both Windows and Linux)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Input folder containing PDFs to convert (created if missing)
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        Directory.CreateDirectory(inputFolder);

        // Output folder for PDF/A‑1b files (created if missing)
        string outputFolder = Path.Combine(baseDir, "PdfA_Output");
        Directory.CreateDirectory(outputFolder);

        // CSV file that will contain conversion results
        string csvLogPath = Path.Combine(baseDir, "conversion_results.csv");

        // Prepare CSV file (write header if file does not exist)
        bool csvExists = File.Exists(csvLogPath);
        using (StreamWriter csvWriter = new StreamWriter(csvLogPath, true))
        {
            if (!csvExists)
            {
                csvWriter.WriteLine("SourceFile,OutputFile,Status,Message");
            }

            // Enumerate all PDF files in the input folder
            string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
            if (pdfFiles.Length == 0)
            {
                Console.WriteLine($"No PDF files found in '{inputFolder}'. Nothing to convert.");
            }

            foreach (string sourcePath in pdfFiles)
            {
                string fileName = Path.GetFileName(sourcePath);
                string outputPath = Path.Combine(outputFolder,
                    Path.GetFileNameWithoutExtension(sourcePath) + "_pdfa.pdf");

                try
                {
                    // Load source PDF inside a using block (deterministic disposal)
                    using (Document doc = new Document(sourcePath))
                    {
                        // Convert to PDF/A‑1b; a temporary XML log is created (can be ignored)
                        string tempXmlLog = Path.GetTempFileName();
                        doc.Convert(tempXmlLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                        // Save the converted document as PDF/A‑1b
                        doc.Save(outputPath);
                    }

                    // Log successful conversion
                    csvWriter.WriteLine($"{EscapeCsv(fileName)},{EscapeCsv(Path.GetFileName(outputPath))},Success,");
                }
                catch (Exception ex)
                {
                    // Log failure with exception message
                    csvWriter.WriteLine($"{EscapeCsv(fileName)},{EscapeCsv(Path.GetFileName(outputPath))},Failure,{EscapeCsv(ex.Message)}");
                }
            }
        }

        Console.WriteLine("Batch conversion completed. Results written to " + csvLogPath);
    }

    // Helper to escape CSV fields that may contain commas or quotes
    static string EscapeCsv(string field)
    {
        if (field == null) return "";
        if (field.Contains('"'))
            field = field.Replace("\"", "\"\"");
        if (field.Contains(',') || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
            field = $"\"{field}\"";
        return field;
    }
}
