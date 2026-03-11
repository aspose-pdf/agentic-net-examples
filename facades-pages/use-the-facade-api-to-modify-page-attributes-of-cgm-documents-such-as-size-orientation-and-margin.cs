using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputCgmPath = "input.cgm";               // source CGM file
        const string intermediatePdfPath = "intermediate.pdf"; // temporary PDF after conversion
        const string outputPdfPath = "output.pdf";             // final PDF with modifications

        // Verify input file exists
        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputCgmPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Convert CGM to PDF (default A4 page size)
            // ------------------------------------------------------------
            using (Document doc = new Document(inputCgmPath, new CgmLoadOptions()))
            {
                // Save the converted PDF to a temporary file
                doc.Save(intermediatePdfPath);
            }

            // ------------------------------------------------------------
            // 2. Add margins to the PDF pages using PdfFileEditor
            //    Margins are specified in default space units (points).
            //    Here we add 36 points (0.5 inch) on each side.
            // ------------------------------------------------------------
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
            PdfFileEditor marginEditor = new PdfFileEditor();
            // null pages array means all pages are processed
            marginEditor.AddMargins(
                intermediatePdfPath,   // source PDF
                outputPdfPath,         // destination PDF
                null,                  // all pages
                36, 36, 36, 36);       // left, right, top, bottom margins

            // ------------------------------------------------------------
            // 3. Change page size and orientation using PdfPageEditor
            //    Example: set page size to A5 and rotate 90 degrees.
            // ------------------------------------------------------------
            // PdfPageEditor also does NOT implement IDisposable.
            PdfPageEditor pageEditor = new PdfPageEditor();
            // Bind the PDF that we just created with margins
            pageEditor.BindPdf(outputPdfPath);

            // Set desired page size (A5) and rotation (90 degrees)
            pageEditor.PageSize = PageSize.A5;
            pageEditor.Rotation = 90; // valid values: 0, 90, 180, 270

            // Apply the changes and save back to the same file
            pageEditor.ApplyChanges();
            pageEditor.Save(outputPdfPath);

            // Cleanup temporary file
            if (File.Exists(intermediatePdfPath))
                File.Delete(intermediatePdfPath);

            Console.WriteLine($"CGM processing completed. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
