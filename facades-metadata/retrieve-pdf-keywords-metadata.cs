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

        // PdfFileInfo implements IDisposable, so wrap it in a using block
        using (var pdfInfo = new PdfFileInfo(inputPath))
        {
            // Verify the file is a PDF
            if (!pdfInfo.IsPdfFile)
            {
                Console.WriteLine("The specified file is not a valid PDF.");
                return;
            }

            // Retrieve the Keywords metadata (empty string if not set)
            string keywords = pdfInfo.Keywords ?? string.Empty;

            // Display the value (show a placeholder when it is empty)
            Console.WriteLine(string.IsNullOrWhiteSpace(keywords)
                ? "Keywords: <none>"
                : $"Keywords: {keywords}");
        }
    }
}
