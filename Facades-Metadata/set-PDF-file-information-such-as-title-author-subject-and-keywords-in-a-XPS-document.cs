using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file (source document)
        const string inputPdfPath = "input.pdf";

        // Temporary PDF file with updated metadata
        const string tempPdfPath = "temp_with_info.pdf";

        // Output XPS file
        const string outputXpsPath = "output.xps";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Update PDF metadata using PdfFileInfo facade
        // -----------------------------------------------------------------
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath))
        {
            // Set required metadata fields
            pdfInfo.Title    = "Sample Document Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "Demonstration of metadata update";
            pdfInfo.Keywords = "Aspose.Pdf, Metadata, XPS";

            // Save the updated PDF to a temporary file
            // SaveNewInfo writes only the changed info, preserving the rest of the document
            pdfInfo.SaveNewInfo(tempPdfPath);
        }

        // -----------------------------------------------------------------
        // 2. Convert the updated PDF to XPS format
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(tempPdfPath))
        {
            // XpsSaveOptions resides in Aspose.Pdf namespace (no separate sub‑namespace)
            XpsSaveOptions xpsOptions = new XpsSaveOptions();

            // Save the PDF as XPS using explicit SaveOptions (required for non‑PDF output)
            pdfDoc.Save(outputXpsPath, xpsOptions);
        }

        Console.WriteLine($"XPS file created with updated metadata: {outputXpsPath}");
    }
}