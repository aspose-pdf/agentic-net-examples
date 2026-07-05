using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BookletCreator
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source document
        const string nupPdf     = "temp_nup.pdf";       // intermediate N‑up file
        const string bookletPdf = "booklet_output.pdf"; // final booklet

        // -----------------------------------------------------------------
        // Ensure a source PDF exists. If it does not, create a simple one with
        // a few blank pages so the example can run without external files.
        // -----------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            using (Document sample = new Document())
            {
                // Create 4 pages (enough for a 2‑up booklet demonstration)
                for (int i = 0; i < 4; i++)
                {
                    sample.Pages.Add();
                }
                sample.Save(inputPdf);
                Console.WriteLine($"Sample source PDF created: {inputPdf}");
            }
        }

        // -----------------------------------------------------------------
        // Step 1: Create an N‑up version of the source PDF.
        // Here we arrange 2 pages side‑by‑side (2 columns, 1 row) on each
        // output page. The result is written to a temporary file.
        // -----------------------------------------------------------------
        using (FileStream inputStream = new FileStream(inputPdf, FileMode.Open, FileAccess.Read))
        using (FileStream nupStream   = new FileStream(nupPdf, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor editor = new PdfFileEditor();
            // MakeNUp(input, output, columns, rows)
            editor.MakeNUp(inputStream, nupStream, 2, 1);
        }

        // -----------------------------------------------------------------
        // Step 2: Convert the N‑up PDF into a booklet.
        // The MakeBooklet overload with PageSize allows us to define the
        // size of the final booklet pages (A4 in this example).
        // -----------------------------------------------------------------
        PdfFileEditor bookletEditor = new PdfFileEditor();
        bookletEditor.MakeBooklet(nupPdf, bookletPdf, PageSize.A4);

        Console.WriteLine($"Booklet created successfully: {bookletPdf}");
    }
}
