using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfAnnotationEditor
using Aspose.Pdf.Annotations;      // AnnotationType

class ExportHighlightAnnotations
{
    static void Main()
    {
        // Input PDF containing annotations
        const string inputPdfPath = "input.pdf";

        // Output XFDF file that will contain only Highlight annotations
        const string outputXfdfPath = "highlights.xfdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Create the annotation editor facade
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the PDF document to the editor
        editor.BindPdf(inputPdfPath);

        // Determine the page range (Aspose.Pdf uses 1‑based indexing)
        int startPage = 1;
        int endPage   = editor.Document.Pages.Count; // total pages in the PDF

        // Specify the annotation types to export – only Highlight annotations
        AnnotationType[] highlightTypes = new AnnotationType[] { AnnotationType.Highlight };

        // Export the selected annotations to an XFDF file
        using (FileStream xfdfStream = File.Create(outputXfdfPath))
        {
            editor.ExportAnnotationsXfdf(xfdfStream, startPage, endPage, highlightTypes);
        }

        // Clean up the editor (releases the bound document)
        editor.Close();

        Console.WriteLine($"Highlight annotations exported to '{outputXfdfPath}'.");
    }
}