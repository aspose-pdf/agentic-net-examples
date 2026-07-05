using System;
using System.IO;
using Aspose.Pdf;

class BatchPdfAConverter
{
    // Escapes a CSV field by surrounding it with double quotes and escaping internal quotes.
    static string EscapeCsv(string field)
    {
        if (field == null) return "";
        return $"\"{field.Replace("\"", "\"\"")}\"";
    }

    static void Main()
    {
        // Input directory containing PDFs to convert.
        const string inputDirectory = "InputPdfs";
        // Output directory for converted PDFs and individual log files.
        const string outputDirectory = "ConvertedPdfs";
        // Path to the summary CSV file.
        const string csvReportPath = "conversion_results.csv";

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        // Verify the input directory exists; if not, create it and exit gracefully.
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input directory '{inputDirectory}' does not exist. Creating it now. Place PDFs to convert inside this folder and re‑run the program.");
            Directory.CreateDirectory(inputDirectory);
            // No files to process, but we still create an empty CSV with header.
            using (StreamWriter csvWriter = new StreamWriter(csvReportPath, false))
            {
                csvWriter.WriteLine("SourcePdf,ConvertedPdf,Success,LogFile");
            }
            return;
        }

        // Prepare the CSV writer.
        using (StreamWriter csvWriter = new StreamWriter(csvReportPath, false))
        {
            // Write CSV header.
            csvWriter.WriteLine("SourcePdf,ConvertedPdf,Success,LogFile");

            // Enumerate all PDF files in the input directory.
            foreach (string sourcePdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
            {
                string fileBaseName = Path.GetFileNameWithoutExtension(sourcePdfPath);
                string convertedPdfPath = Path.Combine(outputDirectory, $"{fileBaseName}_PDF_A_1b.pdf");
                string logFilePath = Path.Combine(outputDirectory, $"{fileBaseName}_conversion.log");

                bool conversionSucceeded = false;

                try
                {
                    // Load the source PDF inside a using block for deterministic disposal.
                    using (Document doc = new Document(sourcePdfPath))
                    {
                        // Convert the document to PDF/A‑1b, writing conversion details to the log file.
                        conversionSucceeded = doc.Convert(logFilePath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                        // Save the converted document.
                        doc.Save(convertedPdfPath);
                    }
                }
                catch (Exception ex)
                {
                    // If an exception occurs, the conversion is considered failed.
                    conversionSucceeded = false;
                    // Optionally, write the exception message to the individual log file.
                    try
                    {
                        File.WriteAllText(logFilePath, $"Exception: {ex.Message}");
                    }
                    catch { /* ignore secondary errors */ }
                }

                // Write a line to the CSV report.
                csvWriter.WriteLine($"{EscapeCsv(sourcePdfPath)},{EscapeCsv(convertedPdfPath)},{conversionSucceeded},{EscapeCsv(logFilePath)}");
            }
        }

        Console.WriteLine("Batch conversion completed. Report saved to " + csvReportPath);
    }
}
