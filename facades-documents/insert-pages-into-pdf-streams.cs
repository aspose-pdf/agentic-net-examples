using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string destinationPdfPath = "destination.pdf";
        const string sourcePdfPath = "source.pdf";
        const string outputPdfPath = "merged.pdf";

        if (!File.Exists(destinationPdfPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Open streams for the destination PDF, source PDF, and the output PDF
        using (FileStream destinationStream = new FileStream(destinationPdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream sourceStream = new FileStream(sourcePdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
        {
            // Define the pages to insert from the source PDF (1‑based page numbers)
            int[] pagesToInsert = new int[] { 2, 4, 5 };
            // Define the position in the destination PDF after which pages will be inserted (1‑based)
            int insertPosition = 3;

            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Insert(destinationStream, insertPosition, sourceStream, pagesToInsert, outputStream);

            if (success)
            {
                Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdfPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Failed to insert pages.");
            }
        }
    }
}
