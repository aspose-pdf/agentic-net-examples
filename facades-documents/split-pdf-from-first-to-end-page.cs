using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path – will contain pages 1..endPage
        const string outputPath = "output.pdf";
        // Specify the last page to include in the front part (inclusive)
        const int endPage = 5;

        // Verify that the source file exists before attempting the operation
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
        PdfFileEditor editor = new PdfFileEditor();

        // SplitFromFirst extracts pages from the first page up to 'endPage' and saves them
        bool result = editor.SplitFromFirst(inputPath, endPage, outputPath);

        if (result)
        {
            Console.WriteLine($"Successfully split PDF. Front part saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to split PDF using PdfFileEditor.SplitFromFirst.");
        }

        // No explicit disposal required for PdfFileEditor
    }
}