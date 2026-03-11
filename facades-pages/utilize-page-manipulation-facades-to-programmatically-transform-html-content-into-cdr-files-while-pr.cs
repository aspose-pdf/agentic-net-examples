using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // required for HtmlLoadOptions if needed

class HtmlToCdrConverter
{
    static void Main()
    {
        // Input HTML file path
        const string htmlPath = "input.html";

        // Temporary PDF output path (Aspose.Pdf cannot save directly to CDR)
        const string tempPdfPath = "intermediate.pdf";

        // Final CDR output path – not supported by Aspose.Pdf.
        // The code will stop after creating the PDF.
        const string cdrPath = "output.cdr";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Load HTML content into a PDF Document.
            //    HtmlLoadOptions is used to correctly interpret the HTML.
            // -----------------------------------------------------------------
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document pdfDoc = new Document(htmlPath, loadOptions))
            {
                // -----------------------------------------------------------------
                // 2. Optional: manipulate pages using PdfPageEditor facade.
                //    Example: set all pages to A4 size and apply a uniform zoom.
                // -----------------------------------------------------------------
                PdfPageEditor pageEditor = new PdfPageEditor(pdfDoc);
                pageEditor.PageSize = PageSize.A4;   // set target page size
                pageEditor.Zoom = 1.0f;              // 100% zoom (no scaling)
                pageEditor.ApplyChanges();           // apply the changes

                // -----------------------------------------------------------------
                // 3. Save the intermediate PDF.
                // -----------------------------------------------------------------
                pdfDoc.Save(tempPdfPath);
            }

            // -----------------------------------------------------------------
            // 4. NOTE: Aspose.Pdf does NOT support saving directly to CorelDRAW
            //    (.cdr) format. The CDR format is proprietary and requires a
            //    dedicated library or conversion tool. The generated PDF can be
            //    used as an intermediate step for further conversion with an
            //    external utility that supports PDF → CDR.
            // -----------------------------------------------------------------
            Console.WriteLine($"HTML successfully converted to PDF: {tempPdfPath}");
            Console.WriteLine("Direct conversion to CDR is not supported by Aspose.Pdf.");
            Console.WriteLine("Use an external tool to convert the PDF to CDR if needed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}