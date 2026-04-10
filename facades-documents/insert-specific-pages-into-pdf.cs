using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, the PDF whose pages will be inserted,
        // and the resulting PDF after insertion.
        const string sourcePdf = "source.pdf";
        const string insertPdf = "insert.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the input files exist before proceeding.
        if (!File.Exists(sourcePdf) || !File.Exists(insertPdf))
        {
            Console.Error.WriteLine("One or more input PDF files were not found.");
            return;
        }

        // Create the facade that performs page insertion.
        PdfFileEditor editor = new PdfFileEditor();

        // Insert pages 2 and 5 from insertPdf after page 1 of sourcePdf.
        // The TryInsert method writes the result directly to outputPdf.
        bool inserted = editor.TryInsert(
            sourcePdf,          // input PDF
            1,                  // insert location (after page 1)
            insertPdf,          // PDF containing pages to insert
            new int[] { 2, 5 }, // page numbers from insertPdf to insert
            outputPdf);         // destination file

        if (inserted)
        {
            Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to insert pages into the PDF.");
        }
    }
}