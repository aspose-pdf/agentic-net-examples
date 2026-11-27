using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath   = "manual_v2.pdf";
        const string xfdfPath  = "stampPage7.xfdf";
        const int    pageNumber = 7;

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor implements IDisposable – use a using block for deterministic disposal
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the source PDF document
                editor.BindPdf(pdfPath);

                // Export only stamp annotations from the specified page to XFDF format.
                // ExportAnnotationsXfdf overload accepts a stream, start page, end page and an array of annotation type names.
                using (FileStream xfdfStream = File.Create(xfdfPath))
                {
                    editor.ExportAnnotationsXfdf(
                        xfdfStream,
                        pageNumber,          // start page
                        pageNumber,          // end page (same as start to limit to one page)
                        new[] { "Stamp" }  // annotation type filter – "Stamp" is the XFDF name for stamp annotations
                    );
                }
            }

            Console.WriteLine($"Stamp annotations from page {pageNumber} exported to '{xfdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
