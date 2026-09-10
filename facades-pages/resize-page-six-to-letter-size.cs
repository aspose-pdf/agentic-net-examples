using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_letter_page6.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfPageEditor is a disposable facade – wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPdf);

            // Specify that only page 6 should be processed.
            editor.ProcessPages = new int[] { 6 };

            // Set the desired output page size to Letter (8.5" x 11" = 612 x 792 points).
            // The PageSize class does not expose a "Letter" member; use a custom size instead.
            editor.PageSize = new PageSize(612, 792);

            // Apply the changes to the bound document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Page 6 resized to Letter size and saved as '{outputPdf}'.");
    }
}
