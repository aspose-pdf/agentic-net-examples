using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "image.cdr";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the CDR file using CdrLoadOptions
            CdrLoadOptions loadOptions = new CdrLoadOptions();

            // Wrap Document in a using block for deterministic disposal
            using (Document doc = new Document(inputPath, loadOptions))
            {
                // Save as PDF (default format)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Converted '{inputPath}' to PDF: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}