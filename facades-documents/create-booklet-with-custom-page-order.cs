using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define custom page order: left (odd‑hand) pages and right (even‑hand) pages.
        // Example arrangement – left pages are even numbers, right pages are odd numbers.
        int[] leftPages = new int[] { 2, 4, 6, 8 };
        int[] rightPages = new int[] { 1, 3, 5, 7, 9, 10 };

        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using statement.
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.MakeBooklet(inputPath, outputPath, PageSize.A4, leftPages, rightPages);

        Console.WriteLine(result
            ? $"Booklet created successfully: {outputPath}"
            : "Failed to create booklet.");
    }
}
