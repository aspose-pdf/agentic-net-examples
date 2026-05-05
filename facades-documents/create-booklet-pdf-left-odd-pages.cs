using System;
using System.IO;
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

        // Open the source PDF as a read‑only stream and the destination as a writeable stream.
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Example page ordering: left pages are odd numbers, right pages are even numbers.
            // Adjust the arrays to match the actual page count of the source PDF.
            int[] leftPages  = new int[] { 1, 3, 5, 7, 9 };
            int[] rightPages = new int[] { 2, 4, 6, 8, 10 };

            // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
            PdfFileEditor editor = new PdfFileEditor();

            // Create a customized booklet using the specified left/right page sequences.
            bool result = editor.MakeBooklet(inputStream, outputStream, leftPages, rightPages);

            if (result)
                Console.WriteLine($"Booklet created successfully: {outputPath}");
            else
                Console.Error.WriteLine("Failed to create booklet.");
        }
    }
}