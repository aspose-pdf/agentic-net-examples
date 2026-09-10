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

        // Left pages (odd numbers) and right pages (even numbers) for the booklet layout
        int[] leftPages = new int[] { 1, 3, 5, 7 };
        int[] rightPages = new int[] { 2, 4, 6, 8 };

        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor editor = new PdfFileEditor();
            bool result = editor.MakeBooklet(inputStream, outputStream, leftPages, rightPages);
            Console.WriteLine(result ? "Booklet created successfully." : "Failed to create booklet.");
        }
    }
}