using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath   = "compliance.log";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        bool isPdfUaCompliant;

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Check PDF/UA compliance via the built‑in property
            isPdfUaCompliant = doc.IsPdfUaCompliant;
        }

        // Prepare a log message
        string logMessage = $"PDF/UA compliant: {isPdfUaCompliant}";

        // Output to console (useful for immediate feedback)
        Console.WriteLine(logMessage);

        // Persist the result to a log file for further processing
        try
        {
            File.WriteAllText(logPath, logMessage);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write log file: {ex.Message}");
        }
    }
}