using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path (source PDF)
        const string inputPath = "input.pdf";

        // Output PDF file path (final result after deletion and resizing)
        const string outputPath = "output.pdf";

        // Pages to delete (1‑based indexing). Example: delete pages 2 and 3.
        int[] pagesToDelete = new int[] { 2, 3 };

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfFileEditor instance (facade for file‑level operations)
        PdfFileEditor editor = new PdfFileEditor();

        // First step: delete the specified pages.
        // Use streams to keep everything in memory before the resize step.
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream afterDeleteStream = new MemoryStream())
        {
            // Delete pages and write the intermediate result to afterDeleteStream
            editor.Delete(inputStream, pagesToDelete, afterDeleteStream);

            // Reset the position of the intermediate stream for the next operation
            afterDeleteStream.Position = 0;

            // Second step: resize the contents of the resulting document.
            // Here we shrink the page contents to 80 % of their original size.
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Resize using percentages (newWidth = 80 %, newHeight = 80 %)
                editor.ResizeContentsPct(afterDeleteStream, outputStream, null, 80, 80);
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}