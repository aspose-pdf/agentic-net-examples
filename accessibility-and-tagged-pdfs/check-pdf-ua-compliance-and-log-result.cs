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

        try
        {
            // Load the PDF document within a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Check PDF/UA compliance
                bool isUaCompliant = doc.IsPdfUaCompliant;

                // Output the result to the console
                Console.WriteLine($"PDF/UA compliant: {isUaCompliant}");

                // Log the boolean result to a text file for further processing
                File.WriteAllText(logPath, isUaCompliant.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}