using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the destination PDF, source PDF (pages to insert), and the output PDF.
        const string destinationPath = "destination.pdf";
        const string sourcePath      = "source.pdf";
        const string outputPath      = "result.pdf";

        // Pages (1‑based) from the source PDF that should be inserted.
        int[] pagesToInsert = new int[] { 2, 4, 5 };

        // Position (1‑based) in the destination PDF where the pages will be inserted.
        int insertPosition = 3;

        // Verify that the input files exist.
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Open the streams with proper disposal.
        using (FileStream destStream = new FileStream(destinationPath, FileMode.Open, FileAccess.Read))
        using (FileStream srcStream  = new FileStream(sourcePath,    FileMode.Open, FileAccess.Read))
        using (FileStream outStream  = new FileStream(outputPath,    FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor provides the Insert method that works directly with streams.
            PdfFileEditor editor = new PdfFileEditor();

            // Insert the selected pages from srcStream into destStream at the specified position.
            bool result = editor.Insert(destStream, insertPosition, srcStream, pagesToInsert, outStream);

            if (result)
                Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to insert pages.");
        }
    }
}