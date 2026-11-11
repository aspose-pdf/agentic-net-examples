using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

#pragma warning disable NU1902, NU1903 // Suppress known‑vulnerability package warnings

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a minimal PDF in memory (so we don't rely on a file)
        // ------------------------------------------------------------
        byte[] sourcePdfBytes;
        using (var doc = new Document())
        {
            // Add a single blank page – this is enough for the editor demo
            doc.Pages.Add();

            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                sourcePdfBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Edit the PDF entirely in memory using PdfPageEditor
        // ------------------------------------------------------------
        using (var sourceStream = new MemoryStream(sourcePdfBytes))
        using (var destinationStream = new MemoryStream())
        {
            var pageEditor = new PdfPageEditor();
            // Bind the source PDF stream
            pageEditor.BindPdf(sourceStream);

            // Rotate the first page (pages are 1‑based)
            pageEditor.ProcessPages = new int[] { 1 };
            pageEditor.Rotation = 90; // clockwise 90°

            // Save the edited PDF into the destination stream
            pageEditor.Save(destinationStream);

            // Optional: write the edited PDF to disk for manual verification
            File.WriteAllBytes("edited.pdf", destinationStream.ToArray());
        }
    }
}

#pragma warning restore NU1902, NU1903 // Re‑enable the warnings after the sample