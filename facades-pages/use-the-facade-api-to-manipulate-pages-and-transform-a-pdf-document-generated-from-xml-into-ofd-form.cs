using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";          // Source XML
        const string pdfFromXml = "generated.pdf";   // PDF created from XML
        const string finalPdf = "final.pdf";         // PDF after page manipulation

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // ---------- 1. Generate PDF from XML ----------
        using (Document doc = new Document())
        {
            // Bind XML (optional XSLT can be supplied as a second argument)
            doc.BindXml(xmlPath);
            // Save the intermediate PDF
            doc.Save(pdfFromXml);
        }

        // ---------- 2. Manipulate pages using PdfPageEditor (Facade API) ----------
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the PDF produced in step 1
            editor.BindPdf(pdfFromXml);

            // Example manipulations:
            // Rotate first page 90 degrees
            editor.PageRotations = new Dictionary<int, int> { { 1, 90 } };
            // Set zoom factor (1.0 = 100%)
            editor.Zoom = 0.8f;
            // Change page size if needed (optional)
            // editor.PageSize = new PageSize(595, 842); // A4 size in points

            // Apply the changes
            editor.ApplyChanges();

            // Save the resulting PDF
            editor.Save(finalPdf);
        }

        // ---------- 3. OFD conversion note ----------
        // Aspose.Pdf does NOT support saving/exporting to OFD format.
        // OFD is input‑only; you can load an OFD file but cannot create one.
        // The final document is therefore saved as PDF (finalPdf).

        Console.WriteLine("PDF generated from XML, pages manipulated, and saved as PDF.");
    }
}