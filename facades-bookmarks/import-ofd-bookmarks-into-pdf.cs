using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string ofdPath      = "source.ofd";      // OFD file containing bookmarks
        const string targetPdfPath = "target.pdf";      // Existing PDF to receive bookmarks (or will be created)
        const string outputPath    = "output.pdf";      // Resulting PDF with imported bookmarks

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"OFD file not found: {ofdPath}");
            return;
        }

        // Load the OFD document (input‑only format) using the appropriate load options
        using (Document ofdDoc = new Document(ofdPath, new OfdLoadOptions()))
        {
            // Use PdfBookmarkEditor on the OFD document to extract its bookmarks
            using (PdfBookmarkEditor ofdEditor = new PdfBookmarkEditor(ofdDoc))
            {
                // Extract all bookmarks (may be empty if the OFD has none)
                Bookmarks ofdBookmarks = ofdEditor.ExtractBookmarks();

                // Load (or create) the target PDF where bookmarks will be added
                Document pdfDoc;
                if (File.Exists(targetPdfPath))
                {
                    pdfDoc = new Document(targetPdfPath);
                }
                else
                {
                    // Create a new PDF with a single blank page if the target does not exist
                    pdfDoc = new Document();
                    pdfDoc.Pages.Add();
                }

                using (pdfDoc)
                {
                    // Attach a PdfBookmarkEditor to the target PDF
                    using (PdfBookmarkEditor pdfEditor = new PdfBookmarkEditor(pdfDoc))
                    {
                        // Add each top‑level bookmark from the OFD to the PDF.
                        // CreateBookmarks(Bookmark) preserves the hierarchy of child items.
                        foreach (Bookmark bm in ofdBookmarks)
                        {
                            pdfEditor.CreateBookmarks(bm);
                        }

                        // Save the updated PDF with the imported bookmarks
                        pdfEditor.Save(outputPath);
                    }
                }

                Console.WriteLine($"Bookmarks imported from '{ofdPath}' and saved to '{outputPath}'.");
            }
        }
    }
}