using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string logPath    = "conversion.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Change PDF version to 1.5.
            // Convert logs any conversion issues to the specified log file.
            doc.Convert(logPath, PdfFormat.v_1_5, ConvertErrorAction.Delete);

            // Save with incremental update (parameterless Save uses incremental update mode).
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with version 1.5 and incremental update enabled: {outputPath}");
    }
}