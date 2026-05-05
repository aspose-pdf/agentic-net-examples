using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Pages to delete (1‑based indexing)
        int[] pagesToDelete = new int[] { 2, 3 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Delete pages and store the result in a memory stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream afterDeleteStream = new MemoryStream())
        {
            PdfFileEditor editor = new PdfFileEditor();
            editor.Delete(inputStream, pagesToDelete, afterDeleteStream);

            // Reset position for the next operation
            afterDeleteStream.Position = 0;

            // Resize contents of all pages (shrink to 80% of original size)
            using (MemoryStream finalStream = new MemoryStream())
            {
                PdfFileEditor resizeEditor = new PdfFileEditor();
                // null pages array means all pages are processed
                resizeEditor.ResizeContentsPct(afterDeleteStream, finalStream, null, 80, 80);

                // Write the final PDF to disk
                File.WriteAllBytes(outputPath, finalStream.ToArray());
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}