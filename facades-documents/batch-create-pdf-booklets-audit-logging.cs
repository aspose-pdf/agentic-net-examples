using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be processed
        string[] inputFiles = { "input1.pdf", "input2.pdf" };

        // Directory where booklet PDFs will be saved
        string outputDirectory = "Booklets";

        // Path to the audit log file
        string logFilePath = "booklet_creation_log.txt";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Open a StreamWriter for logging; it will be disposed automatically
        using (StreamWriter log = new StreamWriter(logFilePath, false))
        {
            foreach (string inputPath in inputFiles)
            {
                // Verify the input file exists
                if (!File.Exists(inputPath))
                {
                    log.WriteLine($"{DateTime.Now}: INPUT NOT FOUND - {inputPath}");
                    continue;
                }

                // Determine the output booklet file name
                string outputPath = Path.Combine(
                    outputDirectory,
                    Path.GetFileNameWithoutExtension(inputPath) + "_booklet.pdf");

                log.WriteLine($"{DateTime.Now}: START - Creating booklet from '{inputPath}' to '{outputPath}'.");

                // Instantiate the PdfFileEditor facade
                PdfFileEditor editor = new PdfFileEditor();

                // Perform the booklet conversion
                bool result = editor.MakeBooklet(inputPath, outputPath);
                log.WriteLine($"{DateTime.Now}: MakeBooklet returned {result}.");

                // Capture any conversion log details
                string conversionLog = editor.ConversionLog;
                if (!string.IsNullOrEmpty(conversionLog))
                {
                    log.WriteLine($"{DateTime.Now}: ConversionLog:");
                    log.WriteLine(conversionLog);
                }

                log.WriteLine($"{DateTime.Now}: END - Finished processing '{inputPath}'.");
                log.WriteLine(new string('-', 60));
            }
        }

        Console.WriteLine($"Batch booklet creation completed. Audit log saved to '{logFilePath}'.");
    }
}