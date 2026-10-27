using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string logPath = "error.log";

        try
        {
            // Verify that the input file exists before processing
            if (!File.Exists(inputPath))
                throw new FileNotFoundException("Input PDF not found.", inputPath);

            // Load and process the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Example processing: add a blank page at the end of the document
                doc.Pages.Add();

                // Save the modified PDF (PDF format, no SaveOptions needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Build a detailed crash report
            var report = $"--- Crash Report ({DateTime.UtcNow:O}) ---{Environment.NewLine}";
            report += $"Message: {ex.Message}{Environment.NewLine}";
            report += $"Source: {ex.Source}{Environment.NewLine}";
            report += $"StackTrace:{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";

            // Include inner exception details recursively, if any
            Exception inner = ex.InnerException;
            while (inner != null)
            {
                report += $"--- Inner Exception ---{Environment.NewLine}";
                report += $"Message: {inner.Message}{Environment.NewLine}";
                report += $"Source: {inner.Source}{Environment.NewLine}";
                report += $"StackTrace:{Environment.NewLine}{inner.StackTrace}{Environment.NewLine}";
                inner = inner.InnerException;
            }

            // Attempt to write the report to a log file; fallback to console if logging fails
            try
            {
                File.AppendAllText(logPath, report);
                Console.Error.WriteLine($"An error occurred. Details written to '{logPath}'.");
            }
            catch
            {
                Console.Error.WriteLine("Failed to write error log. Reporting to console:");
                Console.Error.WriteLine(report);
            }
        }
    }
}