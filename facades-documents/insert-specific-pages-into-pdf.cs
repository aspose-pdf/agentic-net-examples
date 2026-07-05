using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string destinationPdf = "destination.pdf"; // PDF into which pages will be inserted
        const string sourcePdf      = "source.pdf";      // PDF providing pages to insert
        const string outputPdf      = "merged.pdf";      // Resulting PDF

        // Position (1‑based) in the destination PDF where pages will be inserted
        // For example, 1 inserts before the first page, 2 after the first page, etc.
        int insertPosition = 3;

        // Pages from the source PDF to insert (1‑based page numbers)
        int[] pagesToInsert = new int[] { 2, 4, 5 };

        // Validate input files
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
            // PdfFileEditor provides the Insert operation for file paths
            PdfFileEditor editor = new PdfFileEditor();

            // Insert the specified pages from sourcePdf into destinationPdf at insertPosition
            // The method returns true on success
            bool success = editor.Insert(
                destinationPdf,   // inputFile (the PDF to receive the pages)
                insertPosition,  // insertLocation (position in the input file)
                sourcePdf,        // portFile (the PDF providing pages)
                pagesToInsert,    // pageNumber (array of pages to insert)
                outputPdf);       // outputFile (resulting PDF)

            if (success)
                Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
            else
                Console.Error.WriteLine("Insert operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}