using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "policy_update.pdf";
        const string xfdfPath  = "popupPage6.xfdf";
        const int    pageNumber = 6;

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfAnnotationEditor does NOT implement IDisposable, so we instantiate it directly.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(pdfPath);

        // Export only Popup annotations from the specified page.
        // Use ExportAnnotationsXfdf overload that accepts a stream, page range and an array of annotation type names.
        string[] popupTypes = new[] { "Popup" };
        using (FileStream xfdfStream = File.Create(xfdfPath))
        {
            editor.ExportAnnotationsXfdf(xfdfStream, pageNumber, pageNumber, popupTypes);
        }

        Console.WriteLine($"Popup annotations from page {pageNumber} exported to '{xfdfPath}'.");
    }
}
