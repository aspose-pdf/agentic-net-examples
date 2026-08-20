using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_a5_last_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine the index of the last page (1‑based indexing)
        int lastPageNumber;
        using (Document doc = new Document(inputPath))
        {
            lastPageNumber = doc.Pages.Count;
        }

        // Use PdfPageEditor (Facade) to change the size of the last page only
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Specify that only the last page should be processed
            editor.ProcessPages = new int[] { lastPageNumber };

            // Set the desired page size (A5)
            editor.PageSize = PageSize.A5;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with last page set to A5: {outputPath}");
    }
}