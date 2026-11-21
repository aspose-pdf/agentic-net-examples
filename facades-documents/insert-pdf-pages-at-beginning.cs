using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePdf = "source.pdf";        // PDF whose pages will be inserted
        const string destPdf   = "destination.pdf";   // PDF into which pages will be inserted
        const string outputPdf = "merged_begin.pdf"; // Resulting PDF

        // Verify files exist
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(destPdf))
        {
            Console.Error.WriteLine($"Destination file not found: {destPdf}");
            return;
        }

        // Copy destination to output so the original file remains unchanged
        File.Copy(destPdf, outputPdf, true);

        // Load the source and the (copied) destination documents
        Document srcDoc    = new Document(sourcePdf);
        Document targetDoc = new Document(outputPdf);

        // Insert all pages from the source PDF at the beginning of the target PDF.
        // The Pages.Insert overload expects the position (1‑based) and a PageCollection.
        targetDoc.Pages.Insert(1, srcDoc.Pages);

        // Save the merged document back to the output path
        targetDoc.Save(outputPdf);

        Console.WriteLine($"Pages from '{sourcePdf}' have been inserted at the beginning of '{destPdf}'.");
        Console.WriteLine($"Result saved to '{outputPdf}'.");
    }
}