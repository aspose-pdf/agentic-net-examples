using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_even_duration.pdf";
        const int    durationSec = 5; // display duration in seconds for even pages

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document disposal with using)
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor bound to the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Build an array of even page numbers (Aspose.Pdf uses 1‑based indexing)
            int pageCount = doc.Pages.Count;
            int evenCount = pageCount / 2;
            int[] evenPages = new int[evenCount];
            for (int i = 0, pageNum = 2; i < evenCount; i++, pageNum += 2)
                evenPages[i] = pageNum;

            // Apply the settings only to the even pages
            editor.ProcessPages = evenPages;          // pages to be edited
            editor.DisplayDuration = durationSec;     // set desired duration (seconds)
            editor.ApplyChanges();                    // commit changes to the document

            // Save the modified PDF (using rule: document disposal with using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with display duration set on even pages: '{outputPath}'.");
    }
}