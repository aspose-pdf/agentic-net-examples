using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure input PDF exists – create a placeholder with enough pages if missing
        if (!File.Exists(inputPath))
        {
            using (var placeholder = new Document())
            {
                // Create three pages so that pages 2 and 3 can be deleted safely
                placeholder.Pages.Add(); // page 1
                placeholder.Pages.Add(); // page 2
                placeholder.Pages.Add(); // page 3
                placeholder.Save(inputPath);
            }
        }

        // Pages to delete (1‑based indexing)
        int[] pagesToDelete = new int[] { 2, 3 };

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        // MemoryStream will hold the PDF after pages are deleted
        using (MemoryStream afterDelete = new MemoryStream())
        {
            PdfFileEditor editor = new PdfFileEditor();

            // Delete the specified pages and write the result to afterDelete
            editor.Delete(inputStream, pagesToDelete, afterDelete);

            // Reset the stream position so it can be read again
            afterDelete.Position = 0;

            // Open the destination stream for the final resized PDF
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Resize the contents to 80 % of the original width and height.
                // Passing null for the pages array applies the resize to all pages.
                editor.ResizeContentsPct(afterDelete, outputStream, null, 80, 80);
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
