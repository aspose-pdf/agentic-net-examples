using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string ofdPath       = "source.ofd";   // OFD file containing bookmarks
        const string targetPdfPath = "target.pdf";   // Existing PDF to receive the bookmarks
        const string outputPdfPath = "result.pdf";   // PDF with imported bookmarks

        // Validate input files
        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"OFD file not found: {ofdPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the OFD file and convert it to an in‑memory PDF document
        // -----------------------------------------------------------------
        using (Document ofdDoc = new Document(ofdPath, new OfdLoadOptions()))
        using (MemoryStream pdfFromOfd = new MemoryStream())
        {
            // Save the converted PDF into a memory stream (no file I/O)
            ofdDoc.Save(pdfFromOfd);
            pdfFromOfd.Position = 0; // Reset for reading

            // -------------------------------------------------------------
            // 2. Export the bookmarks from the temporary PDF to an XML stream
            // -------------------------------------------------------------
            using (PdfBookmarkEditor ofdBookmarkEditor = new PdfBookmarkEditor())
            {
                ofdBookmarkEditor.BindPdf(pdfFromOfd); // Bind to the in‑memory PDF

                using (MemoryStream bookmarksXml = new MemoryStream())
                {
                    ofdBookmarkEditor.ExportBookmarksToXML(bookmarksXml);
                    bookmarksXml.Position = 0; // Reset for reading

                    // ---------------------------------------------------------
                    // 3. Load the target PDF and import the exported bookmarks
                    // ---------------------------------------------------------
                    using (Document targetDoc = new Document(targetPdfPath))
                    using (PdfBookmarkEditor targetBookmarkEditor = new PdfBookmarkEditor())
                    {
                        targetBookmarkEditor.BindPdf(targetDoc); // Bind to the target PDF
                        targetBookmarkEditor.ImportBookmarksWithXML(bookmarksXml); // Import

                        // -------------------------------------------------
                        // 4. Save the updated PDF with the imported bookmarks
                        // -------------------------------------------------
                        targetDoc.Save(outputPdfPath);
                    }
                }
            }
        }

        Console.WriteLine($"Bookmarks imported from '{ofdPath}' and saved to '{outputPdfPath}'.");
    }
}