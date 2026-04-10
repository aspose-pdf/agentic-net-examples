using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added namespace for TextFragment

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a simple PDF document in memory. This removes the external
        //    file dependency that caused the FileNotFoundException.
        // ---------------------------------------------------------------------
        byte[] pdfBytes;
        using (var tempStream = new MemoryStream())
        {
            var doc = new Document();
            var page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample PDF for in‑memory editing."));
            doc.Save(tempStream);
            pdfBytes = tempStream.ToArray();
        }

        // ---------------------------------------------------------------------
        // 2. Prepare an output stream that will receive the edited PDF.
        // ---------------------------------------------------------------------
        using (var outputStream = new MemoryStream())
        {
            // Load the PDF bytes into a stream that PdfPageEditor can bind to.
            using (var inputStream = new MemoryStream(pdfBytes))
            {
                // PdfPageEditor implements IDisposable, so wrap it in a using block.
                using (var editor = new PdfPageEditor())
                {
                    // Bind the in‑memory PDF.
                    editor.BindPdf(inputStream);

                    // Example manipulation: rotate the first page 90 degrees.
                    editor.Rotation = 90;                     // allowed values: 0, 90, 180, 270
                    editor.ProcessPages = new int[] { 1 };    // apply only to page 1

                    // Apply the changes to the underlying document.
                    editor.ApplyChanges();

                    // Save the modified PDF into the output memory stream.
                    editor.Save(outputStream);
                }
            }

            // Reset the position before reading the stream (optional).
            outputStream.Position = 0;

            // Write the edited PDF to a file (or use the stream elsewhere).
            File.WriteAllBytes("output.pdf", outputStream.ToArray());
        }

        Console.WriteLine("PDF page manipulation completed.");
    }
}
