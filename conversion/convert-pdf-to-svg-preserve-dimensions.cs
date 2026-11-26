using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "SvgPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output folder exists.
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Preserve each page's original dimensions.
                // Aspose.Pdf uses 1‑based indexing (see global rule).
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    Page page = pdfDoc.Pages[i];

                    // MediaBox defines the physical page size.
                    // Assign it explicitly to PageInfo to avoid any implicit scaling.
                    page.PageInfo.Width  = page.MediaBox.Width;
                    page.PageInfo.Height = page.MediaBox.Height;
                }

                // Configure SVG export options.
                SvgSaveOptions svgOpts = new SvgSaveOptions();
                // No need to set PageSavingMode or OutputFileNamePattern –
                // when a folder path is supplied to Document.Save, Aspose.Pdf
                // automatically creates one SVG file per page using the pattern
                // "page_{pageNumber}.svg".

                // SVG conversion may rely on GDI+ (Windows only). Wrap in try‑catch.
                try
                {
                    // When a folder path is supplied, Aspose.Pdf creates one file per page.
                    pdfDoc.Save(outputDir, svgOpts);
                    Console.WriteLine($"SVG files have been saved to '{outputDir}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("SVG conversion requires Windows GDI+. Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
