using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Bind the source PDF to the facade
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Get the total number of pages (1‑based indexing)
        int lastPageNumber = editor.GetPages();

        // Apply changes only to the last page
        editor.ProcessPages = new int[] { lastPageNumber };
        editor.PageSize = PageSize.A5; // Set A5 size for the selected page

        // Apply modifications and save the result
        editor.ApplyChanges();
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Booklet created with last page set to A5: {outputPath}");
    }
}