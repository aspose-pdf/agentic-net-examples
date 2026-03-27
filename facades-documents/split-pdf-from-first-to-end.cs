using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int endPage = 5; // page number to split up to (inclusive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.SplitFromFirst(inputPath, endPage, outputPath);
            if (success)
            {
                Console.WriteLine($"PDF split successfully. Pages 1-{endPage} saved to '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("PDF split failed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}