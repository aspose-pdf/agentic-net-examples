using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (front part of the split)
        const string outputPath = "output.pdf";
        // Page number up to which the PDF will be split (inclusive)
        int endPage = 5; // adjust as needed

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source and destination streams.
        // The streams are wrapped in using blocks to ensure they are disposed.
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the first page to the specified end page.
            // The method returns true on success.
            bool success = editor.SplitFromFirst(inputStream, endPage, outputStream);

            if (success)
            {
                Console.WriteLine($"PDF successfully split. Front part saved to '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("PDF split operation failed.");
            }
        }
    }
}