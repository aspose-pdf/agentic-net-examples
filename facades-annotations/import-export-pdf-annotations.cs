using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath      = "schedule_v1.pdf";
        const string importXfdf   = "scheduleAnnotations.xfdf";
        const string exportXfdf   = "scheduleExport.xfdf";
        const int    startPage    = 3;
        const int    endPage      = 8;

        // Verify required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(importXfdf))
        {
            Console.Error.WriteLine($"Import XFDF not found: {importXfdf}");
            return;
        }

        // PdfAnnotationEditor does not implement IDisposable, so no using block is needed
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the PDF document to the editor
        editor.BindPdf(pdfPath);

        // Import annotations from the XFDF file into the bound PDF
        editor.ImportAnnotationsFromXfdf(importXfdf);

        // Export annotations from pages 3 through 8 to a new XFDF file
        // Resolve overload ambiguity by passing an explicit string[] (empty array = all types)
        string[] allAnnotationTypes = new string[0];
        using (FileStream fs = File.Create(exportXfdf))
        {
            editor.ExportAnnotationsXfdf(fs, startPage, endPage, allAnnotationTypes);
        }

        Console.WriteLine($"Annotations exported to '{exportXfdf}'.");
    }
}
