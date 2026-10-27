using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "presentation.xml";
        const string outputPdfPath = "presentation.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Input XML not found: {xmlPath}");
            return;
        }

        // Load XML and create a PDF document.
        // XmlLoadOptions tells Aspose.Pdf to treat the source as XML.
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Set the document to open in full‑screen (presentation) mode.
            pdfDoc.PageMode = PageMode.FullScreen;

            // NOTE: In the core Aspose.Pdf API (without using Facades) the
            // page‑transition feature is not exposed. Therefore only the
            // full‑screen mode is applied here. If transition effects are
            // required, they must be added via the PdfPageEditor class from
            // Aspose.Pdf.Facades, which is prohibited by the hard‑rule.

            // Save the resulting PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Presentation PDF saved to '{outputPdfPath}'.");
    }
}
