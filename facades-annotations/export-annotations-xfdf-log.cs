using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string xfdfLogPath = "export_log.xfdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Perform the primary export operation (PDF save in this example)
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");

                // Diagnostic: write full XFDF content of all annotations to a log file
                doc.ExportAnnotationsToXfdf(xfdfLogPath);
                Console.WriteLine($"XFDF log written to '{xfdfLogPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}