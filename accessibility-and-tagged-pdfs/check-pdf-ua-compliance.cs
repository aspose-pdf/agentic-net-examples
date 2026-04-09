using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath   = "compliance_log.txt";

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

            // Log the result to the console
            Console.WriteLine($"PDF/UA compliant: {isUaCompliant}");

            // Optionally write the result to a text file for further processing
            File.WriteAllText(logPath, $"PDF/UA compliant: {isUaCompliant}");
        }
    }
}