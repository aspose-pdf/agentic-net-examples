using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API
using Aspose.Pdf.Facades;        // Facades for metadata handling
using Aspose.Pdf;                 // For SvgSaveOptions (same namespace)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // Source PDF
        const string outputSvgPath = "output.svg";  // Destination SVG

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PDF document (core API) – required for binding.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 2. Create PdfFileInfo facade, bind it to the loaded document,
            //    and update metadata properties.
            // -----------------------------------------------------------------
            using (PdfFileInfo pdfInfo = new PdfFileInfo())
            {
                pdfInfo.BindPdf(pdfDoc);                     // Bind to the document

                // Update standard metadata fields
                pdfInfo.Title      = "Updated Document Title";
                pdfInfo.Author     = "John Doe";
                pdfInfo.Subject    = "Sample SVG conversion";
                pdfInfo.Keywords   = "Aspose, PDF, SVG, Metadata";

                // Persist the changes back to the original PDF file.
                // SaveNewInfo writes only the updated info without altering content.
                pdfInfo.SaveNewInfo(inputPdfPath);
            }
        }

        // -----------------------------------------------------------------
        // 3. Reload the (now updated) PDF and convert it to SVG.
        //    Non‑PDF output requires explicit SaveOptions (SvgSaveOptions).
        // -----------------------------------------------------------------
        using (Document updatedDoc = new Document(inputPdfPath))
        {
            SvgSaveOptions svgOptions = new SvgSaveOptions(); // default options
            updatedDoc.Save(outputSvgPath, svgOptions);       // SVG output
        }

        Console.WriteLine($"Metadata updated and PDF converted to SVG: {outputSvgPath}");
    }
}