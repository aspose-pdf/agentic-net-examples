using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the destination PDF, source PDF, and the resulting PDF
        const string destinationPdfPath = "destination.pdf";
        const string sourcePdfPath = "source.pdf";
        const string outputPdfPath = "merged.pdf";

        // Define the insertion parameters
        // Insert after page 1 of the destination PDF (1‑based indexing)
        int insertLocation = 1;
        // Insert pages 2 through 5 from the source PDF
        int startPage = 2;
        int endPage = 5;

        // Verify that the input files exist
        if (!File.Exists(destinationPdfPath) || !File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Open the streams and perform the insertion
        using (FileStream destStream = new FileStream(destinationPdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream srcStream = new FileStream(sourcePdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream outStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does not implement IDisposable, so it is instantiated directly
            PdfFileEditor editor = new PdfFileEditor();

            // Insert the specified page range from the source PDF into the destination PDF
            bool result = editor.Insert(destStream, insertLocation, srcStream, startPage, endPage, outStream);

            if (result)
                Console.WriteLine($"Successfully inserted pages {startPage}-{endPage} into '{outputPdfPath}'.");
            else
                Console.Error.WriteLine("Insert operation failed.");
        }
    }
}