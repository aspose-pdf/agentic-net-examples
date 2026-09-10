using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added namespace for TextFragment

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a simple PDF in memory – this replaces the missing
        //    "source.pdf" file and makes the example self‑contained.
        // ------------------------------------------------------------
        byte[] sourcePdfBytes;
        using (var seedDoc = new Document())
        {
            // Add a single page with a text fragment so the PDF is not empty.
            var page = seedDoc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Hello, Aspose.Pdf!"));

            // Save the document to a MemoryStream and capture the byte array.
            using (var ms = new MemoryStream())
            {
                seedDoc.Save(ms);
                sourcePdfBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Edit the PDF entirely in memory using PdfPageEditor.
        // ------------------------------------------------------------
        using (var inputStream = new MemoryStream(sourcePdfBytes))
        using (var outputStream = new MemoryStream())
        using (var editor = new PdfPageEditor())
        {
            // Bind the in‑memory PDF to the editor.
            editor.BindPdf(inputStream);

            // Example edits: zoom to 50% and rotate every page by 90°.
            editor.Zoom = 0.5f;      // 0.5 = 50%
            editor.Rotation = 90;   // allowed values: 0, 90, 180, 270

            // Apply the pending changes.
            editor.ApplyChanges();

            // Save the edited PDF back into another MemoryStream.
            editor.Save(outputStream);

            // Optional: write the result to a file so you can inspect it.
            File.WriteAllBytes("edited.pdf", outputStream.ToArray());
        }
    }
}
