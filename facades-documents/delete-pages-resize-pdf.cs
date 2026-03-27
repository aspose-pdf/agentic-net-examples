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
        // Desired new content size (in default PDF units)
        double newWidth = 500.0;
        double newHeight = 700.0;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Step 1: Delete pages using stream overloads
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream deletedStream = new MemoryStream())
        {
            PdfFileEditor editor = new PdfFileEditor();
            bool deleteResult = editor.Delete(inputStream, pagesToDelete, deletedStream);
            if (!deleteResult)
            {
                Console.Error.WriteLine("Failed to delete specified pages.");
                return;
            }

            // Reset the intermediate stream position for the next operation
            deletedStream.Position = 0;

            // Step 2: Resize contents of the remaining pages and save
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                bool resizeResult = editor.TryResizeContents(deletedStream, outputStream, null, newWidth, newHeight);
                if (!resizeResult)
                {
                    Console.Error.WriteLine("Resize operation failed.");
                }
                else
                {
                    Console.WriteLine("Pages deleted and document resized successfully.");
                }
            }
        }
    }
}