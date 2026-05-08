using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPdfPath = "output.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a simple PDF document in memory (no external file required).
        // ---------------------------------------------------------------------
        using (var sourceDoc = new Document())
        {
            // Add a blank page (you can add content here if you wish).
            sourceDoc.Pages.Add();

            // Save the document to a memory stream.
            using (var inputStream = new MemoryStream())
            {
                sourceDoc.Save(inputStream);
                // Reset the stream position so that PdfPageEditor can read from the start.
                inputStream.Position = 0;

                // ---------------------------------------------------------------
                // 2. Bind the in‑memory PDF to PdfPageEditor for page manipulation.
                // ---------------------------------------------------------------
                using (var pageEditor = new PdfPageEditor())
                {
                    pageEditor.BindPdf(inputStream);

                    // Example manipulation: rotate the first page 90 degrees.
                    pageEditor.ProcessPages = new int[] { 1 }; // edit only page 1
                    pageEditor.Rotation = 90;                 // valid values: 0, 90, 180, 270

                    // Apply the changes.
                    pageEditor.ApplyChanges();

                    // -----------------------------------------------------------
                    // 3. Save the edited PDF to another memory stream and then to disk.
                    // -----------------------------------------------------------
                    using (var outputStream = new MemoryStream())
                    {
                        pageEditor.Save(outputStream);
                        // Write the result to a file (or use the stream directly).
                        File.WriteAllBytes(outputPdfPath, outputStream.ToArray());
                    }
                }
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPdfPath}'.");
    }
}
