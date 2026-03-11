using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for input PDF and output TeX files
        const string inputPdfPath  = "input.pdf";
        const string outputTexPath = "output.tex";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // Manipulate pages using façade APIs (PdfPageEditor as an example)
            // -----------------------------------------------------------------
            // Rotate the first page 90 degrees clockwise and set a zoom factor.
            PdfPageEditor pageEditor = new PdfPageEditor(pdfDoc);
            pageEditor.Rotation = 90;               // valid values: 0, 90, 180, 270
            pageEditor.Zoom     = 1.2f;             // 1.0 = 100% (float literal)
            pageEditor.ProcessPages = new int[] { 1 }; // apply only to page 1
            pageEditor.ApplyChanges();              // commit the changes

            // -----------------------------------------------------------------
            // Export the (now manipulated) PDF to TeX format while preserving layout
            // -----------------------------------------------------------------
            TeXSaveOptions texOptions = new TeXSaveOptions();
            // Example: enable glyph caching for better performance (optional)
            texOptions.CacheGlyphs = true;

            // Save the document as TeX using the TeXSaveOptions.
            // The overload with SaveOptions is required for non‑PDF formats.
            pdfDoc.Save(outputTexPath, texOptions);
        }

        Console.WriteLine($"PDF has been processed and saved as TeX to '{outputTexPath}'.");
    }
}
