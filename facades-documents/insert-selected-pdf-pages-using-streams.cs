using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string destinationPdfPath = "destination.pdf";
        const string sourcePdfPath      = "source.pdf";
        const string outputPdfPath      = "output.pdf";

        // Pages to insert from the source PDF (1‑based page numbers)
        int[] pagesToInsert = new int[] { 2, 4, 6 };

        // Position in the destination PDF where the pages will be inserted (1‑based)
        int insertPosition = 3;

        // Verify that the input files exist
        if (!File.Exists(destinationPdfPath))
        {
            Console.Error.WriteLine($"File not found: {destinationPdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"File not found: {sourcePdfPath}");
            return;
        }

        // Open streams for the source, destination, and output PDFs
        using (FileStream destStream = new FileStream(destinationPdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream srcStream  = new FileStream(sourcePdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream outStream  = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does NOT implement IDisposable, so it is instantiated directly
            Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();

            // Insert the selected pages from the source PDF into the destination PDF
            bool success = editor.Insert(destStream, insertPosition, srcStream, pagesToInsert, outStream);

            if (success)
                Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdfPath}'.");
            else
                Console.Error.WriteLine("Failed to insert pages.");
        }
    }
}