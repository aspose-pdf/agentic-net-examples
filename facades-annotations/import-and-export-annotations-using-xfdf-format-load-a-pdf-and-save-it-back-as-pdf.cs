using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // ---------- Export annotations to XFDF ----------
        PdfAnnotationEditor exportEditor = new PdfAnnotationEditor();
        exportEditor.BindPdf(inputPdfPath);

        // Export all annotations to an XFDF file via a stream
        using (FileStream xfdfStream = File.Create(xfdfPath))
        {
            exportEditor.ExportAnnotationsToXfdf(xfdfStream);
        }

        exportEditor.Close(); // release resources held by the facade

        // ---------- Import annotations from XFDF and save PDF ----------
        PdfAnnotationEditor importEditor = new PdfAnnotationEditor();
        importEditor.BindPdf(inputPdfPath);

        // Import all annotations from the previously created XFDF file
        importEditor.ImportAnnotationsFromXfdf(xfdfPath);

        // Save the PDF (annotations are now part of the document)
        importEditor.Save(outputPdfPath);
        importEditor.Close();

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and re‑imported. Result saved as '{outputPdfPath}'.");
    }
}