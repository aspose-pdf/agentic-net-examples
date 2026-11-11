using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileSignature implements IDisposable, so wrap it in a using block.
        using (PdfFileSignature signature = new PdfFileSignature(inputPath))
        {
            // Detect extended usage rights.
            bool hasUsageRights = signature.ContainsUsageRights();

            Console.WriteLine($"Extended usage rights present: {hasUsageRights}");
        }
    }
}