using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the resulting PDF after manipulation
        const string resultPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Create a minimal PDF in memory (the sandbox has no pre‑existing files).
        // This ensures the example is self‑contained and does not throw a
        // FileNotFoundException at runtime.
        // ---------------------------------------------------------------------
        byte[] sourcePdfBytes;
        using (var seedDoc = new Document())
        {
            // Add a single blank page – enough for the page‑rotation demo.
            seedDoc.Pages.Add();
            using (var ms = new MemoryStream())
            {
                seedDoc.Save(ms);
                sourcePdfBytes = ms.ToArray();
            }
        }

        // ---------------------------------------------------------------------
        // Load the PDF from the in‑memory byte array into PdfPageEditor for
        // manipulation without touching the file system.
        // ---------------------------------------------------------------------
        using (var inputStream = new MemoryStream(sourcePdfBytes))
        {
            // PdfPageEditor does NOT implement IDisposable – do NOT wrap it in a using block.
            var pageEditor = new PdfPageEditor();

            // Bind the PDF from the memory stream. Ensure the stream position is at the start.
            inputStream.Position = 0;
            pageEditor.BindPdf(inputStream);

            // Specify which pages to affect (1‑based indexing). Here we rotate the first page.
            pageEditor.ProcessPages = new int[] { 1 };
            // Rotation is expressed in degrees: 0, 90, 180, 270.
            pageEditor.Rotation = 90; // rotate 90° clockwise

            // Save the modified PDF into another memory stream.
            using (var outputStream = new MemoryStream())
            {
                pageEditor.Save(outputStream);

                // Write the resulting bytes to a file.
                File.WriteAllBytes(resultPath, outputStream.ToArray());
            }
        }

        Console.WriteLine($"PDF page manipulation completed. Output saved to '{resultPath}'.");
    }
}
