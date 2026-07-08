using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath   = "compliance_log.txt";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Check PDF/UA compliance via the built‑in property
            bool isUaCompliant = doc.IsPdfUaCompliant;

            // Output the result to the console
            Console.WriteLine($"PDF/UA compliant: {isUaCompliant}");

            // Persist the boolean result to a simple log file
            File.WriteAllText(logPath, $"IsPdfUaCompliant={isUaCompliant}");
        }
    }
}