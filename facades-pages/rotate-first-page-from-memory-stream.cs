using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a minimal PDF in memory (placeholder) so we have something to edit.
        byte[] pdfBytes;
        using (var doc = new Document())
        {
            doc.Pages.Add(); // add a blank page
            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                pdfBytes = ms.ToArray();
            }
        }

        const string outputPath = "output.pdf";

        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Bind the PDF stream to the editor.
            pageEditor.BindPdf(inputStream);

            // Example manipulation: rotate the first page 90 degrees.
            pageEditor.ProcessPages = new int[] { 1 };
            pageEditor.Rotation = 90; // Valid values: 0, 90, 180, 270

            // Apply the changes to the document.
            pageEditor.ApplyChanges();

            // Save the edited PDF to a file.
            pageEditor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
