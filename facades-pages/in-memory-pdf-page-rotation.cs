using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a simple PDF document entirely in memory
        using (MemoryStream pdfStream = new MemoryStream())
        {
            // Build a minimal PDF (one blank page)
            Document doc = new Document();
            doc.Pages.Add();
            // Save the document into the memory stream
            doc.Save(pdfStream);
            // Rewind the stream so PdfPageEditor can read from the beginning
            pdfStream.Position = 0;

            // Perform in‑memory page manipulation with PdfPageEditor
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(pdfStream);
                // Example manipulation: rotate all pages 90° and set zoom to 80%
                editor.Rotation = 90;          // valid values: 0, 90, 180, 270
                editor.Zoom = 0.8f;            // 1.0 = 100%
                editor.ApplyChanges();

                // Save the modified PDF to a file (output path can be changed as needed)
                const string outputPath = "output.pdf";
                editor.Save(outputPath);
            }
        }

        Console.WriteLine("In‑memory PDF page manipulation completed.");
    }
}
