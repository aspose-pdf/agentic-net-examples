using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a simple source PDF entirely in memory – no external files are required.
        using (var sourceDoc = new Document())
        {
            // Add at least one page so the PDF is valid.
            sourceDoc.Pages.Add();

            // Save the source PDF to a memory stream.
            using (var sourceStream = new MemoryStream())
            {
                sourceDoc.Save(sourceStream);
                sourceStream.Position = 0; // Reset for reading.

                // Prepare the output stream that will hold the edited PDF.
                using (var outputStream = new MemoryStream())
                {
                    // PdfPageEditor can work with a stream via BindPdf(Stream).
                    using (var editor = new PdfPageEditor())
                    {
                        // Bind the in‑memory source PDF.
                        editor.BindPdf(sourceStream);

                        // Example modification – set a zoom factor (any other edits can be applied here).
                        editor.Zoom = 0.5f;

                        // Save the edited PDF directly into the output stream.
                        editor.Save(outputStream);
                    }

                    // At this point outputStream contains the modified PDF.
                    // Reset the position if the stream will be read later.
                    outputStream.Position = 0;

                    // Optional verification (commented out to avoid disk I/O):
                    // File.WriteAllBytes("modified.pdf", outputStream.ToArray());
                }
            }
        }
    }
}
