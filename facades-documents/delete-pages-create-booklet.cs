using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, temporary file after deletion, and final booklet output
        const string inputPdf = "input.pdf";
        const string intermediatePdf = "intermediate.pdf";
        const string outputBooklet = "booklet.pdf";

        // Example: pages to remove (1‑based indexing)
        int[] pagesToRemove = new int[] { 2, 3 };

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Create a PdfFileEditor instance (no using – it does not implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Delete the unwanted pages and save to an intermediate file
        bool deleteSuccess = editor.Delete(inputPdf, pagesToRemove, intermediatePdf);
        if (!deleteSuccess)
        {
            Console.Error.WriteLine("Failed to delete specified pages.");
            return;
        }

        // Generate a booklet from the intermediate PDF
        bool bookletSuccess = editor.MakeBooklet(intermediatePdf, outputBooklet);
        if (!bookletSuccess)
        {
            Console.Error.WriteLine("Failed to create booklet.");
            return;
        }

        Console.WriteLine($"Booklet created successfully: {outputBooklet}");
    }
}