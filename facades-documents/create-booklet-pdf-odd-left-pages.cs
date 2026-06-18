using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source PDF and the destination stream.
        using (FileStream inputStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Define left and right page sequences for the booklet.
            // Left pages are odd numbers (1‑based indexing), right pages are the even numbers.
            // Adjust the arrays to match the actual page count of your document.
            int[] leftPages  = { 1, 3, 5, 7, 9, 11 };
            int[] rightPages = { 2, 4, 6, 8,10, 12 };

            // Create the facade and invoke the customized booklet operation.
            PdfFileEditor editor = new PdfFileEditor();
            bool result = editor.MakeBooklet(inputStream, outputStream, leftPages, rightPages);

            Console.WriteLine(result ? "Booklet created successfully." : "Failed to create booklet.");
        }
    }
}