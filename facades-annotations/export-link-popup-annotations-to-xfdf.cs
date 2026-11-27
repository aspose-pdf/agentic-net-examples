using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "policy_v1.pdf";
        const string xfdfPath  = "linksPopups.xfdf";
        const int    pageIndex = 5; // Aspose.Pdf uses 1‑based page numbers

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfAnnotationEditor does NOT implement IDisposable, so do NOT wrap it in a using block.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the source PDF to the editor.
        editor.BindPdf(pdfPath);

        // Export only Link (and associated Popup) annotations from the specified page.
        // Use ExportAnnotationsXfdf overload that accepts a string[] of annotation type names.
        using (FileStream xfdfStream = File.Create(xfdfPath))
        {
            editor.ExportAnnotationsXfdf(xfdfStream, pageIndex, pageIndex, new[] { "Link" });
        }

        Console.WriteLine($"Exported link and popup annotations from page {pageIndex} to '{xfdfPath}'.");
    }
}
