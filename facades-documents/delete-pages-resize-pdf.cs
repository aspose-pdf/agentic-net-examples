using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Pages to delete (1‑based indexing). Adjust as needed.
        int[] pagesToDelete = new int[] { 2, 3 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Delete pages and keep the result in a memory stream.
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream afterDelete = new MemoryStream())
        {
            PdfFileEditor editor = new PdfFileEditor();

            bool deleteSuccess = editor.Delete(inputStream, pagesToDelete, afterDelete);
            if (!deleteSuccess)
            {
                Console.Error.WriteLine("Failed to delete pages.");
                return;
            }

            // Prepare the stream for reading the intermediate PDF.
            afterDelete.Position = 0;

            // Resize the contents of all pages to 80 % of the original size.
            // Using the percentage‑based overload.
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                bool resizeSuccess = editor.ResizeContentsPct(
                    afterDelete,          // source PDF (after deletion)
                    outputStream,        // destination PDF
                    null,                // null = all pages
                    80,                  // new width in percent
                    80);                 // new height in percent

                if (!resizeSuccess)
                {
                    Console.Error.WriteLine("Failed to resize contents.");
                    return;
                }
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}