using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet_output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define custom left and right page sequences for the booklet
        int[] leftPages  = new int[] { 2, 4, 6 };
        int[] rightPages = new int[] { 1, 3, 5, 7 };

        // Create the PdfFileEditor and generate the customized booklet
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);

        if (!success)
        {
            Console.Error.WriteLine("MakeBooklet operation failed.");
            return;
        }

        // Open the resulting booklet to inspect page order
        using (Document booklet = new Document(outputPath))
        {
            Console.WriteLine($"Booklet created successfully. Total pages: {booklet.Pages.Count}");
            Console.WriteLine("Page order in the booklet (page numbers as they appear):");

            // Pages are 1‑based; iterate and display each page's number
            for (int i = 1; i <= booklet.Pages.Count; i++)
            {
                Page page = booklet.Pages[i];
                // The Page.Number property reflects the page's position in the document
                Console.WriteLine($"Page {i} -> Original page number: {page.Number}");
            }
        }
    }
}