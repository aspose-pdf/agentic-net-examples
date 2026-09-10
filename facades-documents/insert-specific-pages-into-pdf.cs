using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the existing PDFs
        const string destinationPdfPath = "destination.pdf";
        const string sourcePdfPath      = "source.pdf";
        const string outputPdfPath      = "merged.pdf";

        // Pages to take from the source PDF (1‑based indexing)
        int[] pagesToInsert = new int[] { 2, 4, 5 };

        // Position in the destination PDF where the pages will be inserted (1‑based)
        int insertLocation = 3;

        // Verify that the input files exist
        if (!File.Exists(destinationPdfPath))
        {
            Console.Error.WriteLine($"Error: Destination file not found – {destinationPdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Error: Source file not found – {sourcePdfPath}");
            return;
        }

        // Open the streams for reading the source/destination PDFs and writing the result
        using (FileStream destStream = new FileStream(destinationPdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream srcStream  = new FileStream(sourcePdfPath,   FileMode.Open, FileAccess.Read))
        using (FileStream outStream  = new FileStream(outputPdfPath,  FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does not implement IDisposable, so it is instantiated directly
            Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();

            // Perform the insertion
            bool result = editor.Insert(
                destStream,          // inputStream – the original destination PDF
                insertLocation,      // insertLocation – where to insert in the destination
                srcStream,           // portStream – PDF containing pages to insert
                pagesToInsert,       // pageNumber – specific pages from the source PDF
                outStream);          // outputStream – resulting PDF

            if (result)
                Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdfPath}'.");
            else
                Console.Error.WriteLine("Failed to insert pages.");
        }
    }
}