using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string destinationPdf = "destination.pdf";
        const string sourcePdf = "source.pdf";
        const string outputPdf = "merged.pdf";

        if (!File.Exists(destinationPdf) || !File.Exists(sourcePdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Determine all page numbers from the source PDF (1‑based indexing)
        int[] sourcePageNumbers;
        using (Document srcDoc = new Document(sourcePdf))
        {
            int pageCount = srcDoc.Pages.Count;
            sourcePageNumbers = new int[pageCount];
            for (int i = 0; i < pageCount; i++)
            {
                sourcePageNumbers[i] = i + 1;
            }
        }

        // PdfFileEditor does NOT implement IDisposable; do NOT wrap it in a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Insert the source pages at the beginning (position 1) of the destination PDF
        bool result = editor.Insert(
            destinationPdf,   // inputFile: the PDF into which pages will be inserted
            1,                // insertLocation: position in the input file (1 = beginning)
            sourcePdf,        // portFile: PDF providing pages to insert
            sourcePageNumbers,// pageNumber: array of pages to insert
            outputPdf);       // outputFile: resulting PDF

        Console.WriteLine(result
            ? $"Pages inserted successfully. Output saved to '{outputPdf}'."
            : "Failed to insert pages.");
    }
}