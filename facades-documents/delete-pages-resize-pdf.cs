using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path after deletion and resizing
        const string outputPath = "output_resized.pdf";

        // Pages to delete (1‑based indexing). Example: delete pages 2 and 3.
        int[] pagesToDelete = new int[] { 2, 3 };

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfFileEditor instance (does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Step 1: Delete the specified pages using streams
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream afterDeleteStream = new MemoryStream())
        {
            // Delete pages; pages are 1‑based
            editor.Delete(inputStream, pagesToDelete, afterDeleteStream);
            // Reset the stream position for the next operation
            afterDeleteStream.Position = 0;

            // Step 2: Resize the contents of the resulting document
            using (MemoryStream finalStream = new MemoryStream())
            {
                // null pages array means all pages will be processed
                // Resize to 80 % of original width and height (percent values)
                editor.ResizeContentsPct(afterDeleteStream, finalStream, null, 80, 80);
                finalStream.Position = 0;

                // Step 3: Save the final PDF to the output file
                using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    finalStream.CopyTo(outputStream);
                }
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}