using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the XSL‑FO source and the resulting PDF file
        const string xslFoPath   = "input.fo";
        const string outputPdf   = "output.pdf";

        // Verify that the XSL‑FO file exists
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Load the XSL‑FO document into a PDF using XslFoLoadOptions
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // Set PDF metadata (Title, Author, Subject, Keywords) via the Facades API
            using (PdfFileInfo pdfInfo = new PdfFileInfo())
            {
                // Bind the PdfFileInfo instance to the in‑memory PDF document
                pdfInfo.BindPdf(pdfDoc);

                // Assign desired metadata values
                pdfInfo.Title    = "Sample PDF Title";
                pdfInfo.Author   = "John Doe";
                pdfInfo.Subject  = "Demonstration of XSL‑FO metadata configuration";
                pdfInfo.Keywords = "Aspose.Pdf, XSL‑FO, metadata, example";

                // Save the PDF with the updated metadata (XMP metadata is also preserved)
                bool saved = pdfInfo.SaveNewInfoWithXmp(outputPdf);
                if (!saved)
                {
                    Console.Error.WriteLine("Failed to save PDF with updated metadata.");
                }
                else
                {
                    Console.WriteLine($"PDF saved successfully with metadata to '{outputPdf}'.");
                }
            }
        }
    }
}