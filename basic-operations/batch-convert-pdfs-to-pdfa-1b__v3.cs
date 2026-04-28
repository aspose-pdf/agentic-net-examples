using System;
using System.IO;
using Aspose.Pdf;

class BatchPdfAConverter
{
    static void Main()
    {
        // Resolve folders relative to the executable location to be platform‑independent
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = Path.GetFullPath(Path.Combine(baseDir, "InputPdfs"));
        string outputFolder = Path.GetFullPath(Path.Combine(baseDir, "OutputPdfA"));
        string csvLogPath = Path.GetFullPath(Path.Combine(baseDir, "ConversionLog.csv"));

        // Verify that the input folder exists; otherwise abort with a clear message
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Prepare CSV header (overwrite if file exists)
        using (StreamWriter csvWriter = new StreamWriter(csvLogPath, false))
        {
            csvWriter.WriteLine("SourceFile,OutputFile,Success");
        }

        // Process each PDF file in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(sourcePath);
            string outputPath = Path.Combine(outputFolder,
                Path.GetFileNameWithoutExtension(fileName) + "_pdfa.pdf");

            // Temporary per‑file log (optional, can be deleted after conversion)
            string tempLogPath = Path.Combine(outputFolder,
                Path.GetFileNameWithoutExtension(fileName) + "_log.txt");

            bool conversionResult = false;

            try
            {
                // Load the source PDF (using block ensures proper disposal)
                using (Document doc = new Document(sourcePath))
                {
                    // Convert to PDF/A‑1b; log conversion details to a temporary file
                    conversionResult = doc.Convert(
                        tempLogPath,
                        PdfFormat.PDF_A_1B,
                        ConvertErrorAction.Delete);

                    // Save the converted document as PDF/A
                    doc.Save(outputPath);
                }
            }
            catch (Exception ex)
            {
                // In case of an exception, conversionResult stays false
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
            finally
            {
                // Append the result to the CSV log
                using (StreamWriter csvWriter = new StreamWriter(csvLogPath, true))
                {
                    csvWriter.WriteLine($"{fileName},{Path.GetFileName(outputPath)},{conversionResult}");
                }

                // Clean up the temporary log file if it exists
                if (File.Exists(tempLogPath))
                {
                    try { File.Delete(tempLogPath); } catch { /* ignore */ }
                }
            }
        }

        Console.WriteLine("Batch conversion completed. Log written to " + csvLogPath);
    }
}
