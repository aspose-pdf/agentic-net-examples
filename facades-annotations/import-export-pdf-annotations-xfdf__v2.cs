using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "contract_v3.pdf";
        const string importXfdfPath = "contractAnnotations.xfdf";
        const string exportXfdfPath = "contractExport.xfdf";
        const int startPage = 2;
        const int endPage = 6;

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(importXfdfPath))
        {
            Console.Error.WriteLine($"XFDF import file not found: {importXfdfPath}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable, so wrap it in a using block
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(pdfPath);

            // Import annotations from the XFDF file into the bound PDF
            editor.ImportAnnotationsFromXfdf(importXfdfPath);

            // Export annotations from pages 2 through 6 to a new XFDF file
            // Use the overload that accepts a string[] of annotation type names.
            // An empty array means "export all annotation types".
            using (FileStream fs = File.Create(exportXfdfPath))
            {
                editor.ExportAnnotationsXfdf(fs, startPage, endPage, new string[0]);
            }
        }

        Console.WriteLine($"Annotations exported to '{exportXfdfPath}'.");
    }
}
