using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "contract_final.pdf";
        const string xfdfPath = "contractAll.xfdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Create the annotation editor (does not implement IDisposable)
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the PDF document to the editor
        editor.BindPdf(pdfPath);

        // Specify the three annotation types to export
        AnnotationType[] types = new AnnotationType[]
        {
            AnnotationType.Text,       // Text annotations
            AnnotationType.Highlight,  // Highlight annotations
            AnnotationType.Stamp       // Stamp annotations
        };

        // Export annotations from pages 1 through 10 to an XFDF file
        using (FileStream fs = File.Create(xfdfPath))
        {
            // Correct method name and overload for PdfAnnotationEditor
            editor.ExportAnnotationsXfdf(fs, 1, 10, types);
        }

        // Optionally close the editor (releases internal resources)
        editor.Close();

        Console.WriteLine($"Annotations exported to '{xfdfPath}'.");
    }
}
