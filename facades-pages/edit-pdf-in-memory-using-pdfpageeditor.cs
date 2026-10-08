using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a minimal PDF in memory (acts as the "input" PDF).
        // ------------------------------------------------------------
        byte[] inputPdfBytes;
        using (var doc = new Document())
        {
            // Add a single blank page – you can add any content you need here.
            doc.Pages.Add();

            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                inputPdfBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Edit the PDF entirely in memory using PdfPageEditor.
        // ------------------------------------------------------------
        using (var sourceStream = new MemoryStream(inputPdfBytes))
        using (var destStream   = new MemoryStream())
        {
            // Ensure the source stream is positioned at the beginning.
            sourceStream.Position = 0;

            // PdfPageEditor does not implement IDisposable, so we instantiate it directly.
            var pageEditor = new PdfPageEditor();

            // Bind the PDF document from the source memory stream.
            pageEditor.BindPdf(sourceStream);

            // Specify which page(s) to affect – 1‑based indexing.
            pageEditor.ProcessPages = new int[] { 1 };

            // Rotation angle in degrees (allowed values: 0, 90, 180, 270).
            pageEditor.Rotation = 90;

            // Save the edited PDF into the destination memory stream.
            pageEditor.Save(destStream);

            // Reset the position of the destination stream for further use.
            destStream.Position = 0;

            // Optional: write the edited PDF to a file for verification.
            File.WriteAllBytes("output.pdf", destStream.ToArray());
        }
    }
}
