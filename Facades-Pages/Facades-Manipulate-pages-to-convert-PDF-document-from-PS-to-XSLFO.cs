using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string psInputPath   = "input.ps";      // PostScript source
        const string pdfOutputPath = "output.pdf";    // Resulting PDF
        const string editedPdfPath = "edited.pdf";   // PDF after page manipulation

        // Verify source file exists
        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"Source file not found: {psInputPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load the PostScript file into a PDF Document.
        //    PS is an input‑only format – use PsLoadOptions.
        // ------------------------------------------------------------
        PsLoadOptions psLoadOptions = new PsLoadOptions();

        using (Document pdfDoc = new Document())
        {
            // LoadFrom converts the PS file to PDF in memory.
            pdfDoc.LoadFrom(psInputPath, psLoadOptions);

            // Save the intermediate PDF (optional, shows conversion result)
            pdfDoc.Save(pdfOutputPath);
            Console.WriteLine($"PS → PDF saved to '{pdfOutputPath}'.");

            // ------------------------------------------------------------
            // 2. Manipulate pages using the PdfPageEditor facade.
            //    Example: rotate page 1 by 90°, change its size, and add a
            //    display duration (useful for presentations).
            // ------------------------------------------------------------
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the PDF document to the editor.
                editor.BindPdf(pdfDoc);

                // Rotate the first page (page numbers are 1‑based).
                editor.Rotation = 90;
                editor.ProcessPages = new int[] { 1 };
                editor.ApplyChanges();

                // Change page size for all pages (A4 in this example).
                editor.PageSize = PageSize.A4;
                editor.ApplyChanges();

                // Set a display duration of 5 seconds for each page.
                editor.DisplayDuration = 5;
                editor.ApplyChanges();

                // Save the edited PDF.
                editor.Save(editedPdfPath);
                Console.WriteLine($"Edited PDF saved to '{editedPdfPath}'.");
            }
        }
    }
}