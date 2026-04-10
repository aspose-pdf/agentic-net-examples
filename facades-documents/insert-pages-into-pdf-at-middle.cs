using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the destination PDF, the source PDF to be inserted, and the resulting PDF.
        const string destinationPdf = "destination.pdf";
        const string sourcePdf      = "source.pdf";
        const string outputPdf      = "merged_output.pdf";

        // Verify that the input files exist.
        if (!File.Exists(destinationPdf))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdf}");
            return;
        }
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        try
        {
            // Determine the middle position of the destination PDF.
            // Pages are 1‑based, so we insert after the middle page.
            int insertLocation;
            using (Document destDoc = new Document(destinationPdf))
            {
                int pageCount = destDoc.Pages.Count;
                // If the document has an even number of pages, insert after pageCount/2.
                // If odd, insert after (pageCount/2)+1 to place it roughly in the centre.
                insertLocation = (pageCount / 2) + 1;
            }

            // Determine the range of pages to take from the source PDF (here we take all pages).
            int startPage, endPage;
            using (Document srcDoc = new Document(sourcePdf))
            {
                startPage = 1;
                endPage   = srcDoc.Pages.Count;
            }

            // Perform the insertion using PdfFileEditor.
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Insert(
                destinationPdf,   // inputFile – the PDF into which pages will be inserted
                insertLocation,  // insertLocation – position in the destination PDF
                sourcePdf,        // portFile – the PDF providing pages to insert
                startPage,        // startPage – first page from source PDF
                endPage,          // endPage – last page from source PDF
                outputPdf);       // outputFile – resulting PDF

            if (success)
                Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
            else
                Console.Error.WriteLine("Insertion failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}