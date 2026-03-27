using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "front-part.pdf";
        const int endPage = 5; // extract pages 1 through 5

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so do NOT use a using block.
            PdfFileEditor pdfEditor = new PdfFileEditor();
            bool success = pdfEditor.SplitFromFirst(inputPath, endPage, outputPath);
            if (success)
            {
                Console.WriteLine($"Pages 1-{endPage} saved to '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Split operation failed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
