using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Prepare a PDF as a byte array.
        //    If you already have a PDF file, replace the generation block with
        //    `byte[] pdfBytes = File.ReadAllBytes("your_input.pdf");`
        // ---------------------------------------------------------------------
        byte[] pdfBytes;
        using (var tempDoc = new Document())
        {
            // Add a single blank page (you can add content here if needed)
            tempDoc.Pages.Add();
            using (var ms = new MemoryStream())
            {
                tempDoc.Save(ms);
                pdfBytes = ms.ToArray();
            }
        }

        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // 2. Load the PDF from the byte array into a MemoryStream and edit it.
        // ---------------------------------------------------------------------
        using (var pdfStream = new MemoryStream(pdfBytes))
        using (var editor = new PdfPageEditor())
        {
            // Bind the in‑memory PDF to the editor
            editor.BindPdf(pdfStream);

            // Set the desired page size for all pages (e.g., A4)
            editor.PageSize = PageSize.A4;

            // Apply the size change
            editor.ApplyChanges();

            // Save the modified PDF to a physical file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
