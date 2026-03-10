using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "source.pdf";   // input PDF
        const string ofdPath   = "template.ofd"; // existing OFD file (used as a template)
        const string outputPdf = "ofd_with_metadata.pdf"; // result will be saved as PDF (OFD cannot be saved)

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"OFD not found: {ofdPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Extract metadata from the PDF using PdfFileInfo (Facades API)
        // -----------------------------------------------------------------
        string title   = string.Empty;
        string author  = string.Empty;
        string subject = string.Empty;
        string keywords = string.Empty;

        // PdfFileInfo implements IDisposable, so wrap it in a using block
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            title    = pdfInfo.Title   ?? string.Empty;
            author   = pdfInfo.Author  ?? string.Empty;
            subject  = pdfInfo.Subject ?? string.Empty;
            keywords = pdfInfo.Keywords ?? string.Empty;
        }

        // ---------------------------------------------------------------
        // 2. Load the OFD document (input‑only format) using OfdLoadOptions
        // ---------------------------------------------------------------
        using (Document ofdDoc = new Document(ofdPath, new OfdLoadOptions()))
        {
            // Assign the extracted metadata to the OFD document.
            // The Document.Info property works for OFD as well because the
            // document is represented internally as a PDF object model.
            ofdDoc.Info.Title    = title;
            ofdDoc.Info.Author   = author;
            ofdDoc.Info.Subject  = subject;
            ofdDoc.Info.Keywords = keywords;

            // -----------------------------------------------------------
            // 3. Save the modified document.
            // -----------------------------------------------------------
            // OFD cannot be saved directly (no OFD save options exist).
            // The document is saved as PDF, preserving the metadata.
            ofdDoc.Save(outputPdf);
        }

        Console.WriteLine($"Metadata transferred and saved to '{outputPdf}'.");
    }
}