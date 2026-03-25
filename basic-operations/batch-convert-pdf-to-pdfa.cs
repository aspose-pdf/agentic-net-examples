using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputDir = "input-pdfs";
        string outputDir = "output-pdfa";
        string csvPath = "conversion_results.csv";

        // Ensure the input directory exists; if not, create it and exit early.
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory '{inputDir}' does not exist. Creating it now. Place PDF files in this folder and re‑run the program.");
            Directory.CreateDirectory(inputDir);
            return; // No files to process yet.
        }

        Directory.CreateDirectory(outputDir);

        using (var csvWriter = new StreamWriter(csvPath))
        {
            csvWriter.WriteLine("InputFile,OutputFile,Success,LogFile");

            foreach (string inputPath in Directory.GetFiles(inputDir, "*.pdf"))
            {
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDir, fileName + "_pdfa.pdf");
                string logPath = Path.Combine(outputDir, fileName + "_log.txt");

                bool success = false;
                try
                {
                    using (Document doc = new Document(inputPath))
                    {
                        // Convert to PDF/A‑1b and write conversion log
                        success = doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                        // Save the converted document only if conversion succeeded
                        if (success)
                        {
                            doc.Save(outputPath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception details to the console for debugging purposes
                    Console.WriteLine($"Error processing '{inputPath}': {ex.Message}");
                    success = false;
                }

                csvWriter.WriteLine($"{EscapeCsv(inputPath)},{EscapeCsv(outputPath)},{success},{EscapeCsv(logPath)}");
            }
        }

        Console.WriteLine($"Batch conversion completed. Results saved to {csvPath}");
    }

    static string EscapeCsv(string value)
    {
        if (value == null)
            return string.Empty;
        if (value.Contains(",") || value.Contains("\""))
        {
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }
        return value;
    }
}
