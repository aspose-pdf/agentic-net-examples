using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        // Pages to delete (1‑based indexing)
        int[] pagesToDelete = new int[] { 2, 3 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Delete pages and then resize the remaining pages
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream intermediateStream = new MemoryStream())
        {
            PdfFileEditor editor = new PdfFileEditor();

            // Delete specified pages; result is written to intermediateStream
            editor.Delete(inputStream, pagesToDelete, intermediateStream);
            intermediateStream.Position = 0; // reset for next operation

            // Resize contents of all pages to 80 % of original width/height
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                bool resized = editor.ResizeContentsPct(
                    intermediateStream,   // source (after deletion)
                    outputStream,        // destination
                    null,                // null = all pages
                    80,                  // new width in percent
                    80);                 // new height in percent

                if (!resized)
                {
                    Console.Error.WriteLine("Resize operation failed.");
                    return;
                }
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}