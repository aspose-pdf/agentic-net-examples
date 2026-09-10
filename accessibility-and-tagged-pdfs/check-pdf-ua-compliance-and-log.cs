using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath   = "ua_compliance_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (Document implements IDisposable)
        using (Document doc = new Document(inputPath))
        {
            // Check PDF/UA compliance
            bool isUaCompliant = doc.IsPdfUaCompliant;

            // Log to console
            Console.WriteLine($"PDF/UA compliant: {isUaCompliant}");

            // Append result to a log file for further processing
            try
            {
                File.AppendAllText(logPath, $"{DateTime.UtcNow:u} - {inputPath} - UA compliant: {isUaCompliant}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to write log: {ex.Message}");
            }
        }
    }
}