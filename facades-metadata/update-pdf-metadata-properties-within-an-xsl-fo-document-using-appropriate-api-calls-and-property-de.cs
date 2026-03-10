using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xslFoPath = "input.fo";      // Path to the XSL‑FO source
        const string outputPdf = "output.pdf";    // Desired PDF output path

        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Load the XSL‑FO file into a PDF document using XslFoLoadOptions
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // Use the PdfFileInfo facade to modify PDF metadata
            using (PdfFileInfo pdfInfo = new PdfFileInfo())
            {
                pdfInfo.BindPdf(pdfDoc);                 // Bind the facade to the document

                // Update standard metadata properties
                pdfInfo.Title    = "Updated Document Title";
                pdfInfo.Author   = "Jane Smith";
                pdfInfo.Subject  = "Demonstration of metadata update";
                pdfInfo.Keywords = "Aspose.Pdf, XSL‑FO, Metadata";
                pdfInfo.Creator  = "MetadataUpdaterApp";

                // PdfFileInfo expects dates as strings in PDF date format (e.g., "D:yyyyMMddHHmmss")
                pdfInfo.CreationDate = DateTime.Now.ToString("'D:'yyyyMMddHHmmss");
                pdfInfo.ModDate      = DateTime.Now.ToString("'D:'yyyyMMddHHmmss");

                // Save the PDF with the new metadata
                pdfInfo.SaveNewInfo(outputPdf);
            }
        }

        Console.WriteLine($"PDF saved with updated metadata to '{outputPdf}'.");
    }
}
