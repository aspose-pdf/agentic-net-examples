using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "source.pdf";      // PDF to receive annotations
        const string xfdfFile   = "annotations.xfdf"; // XFDF containing annotations
        const string outputPdf  = "annotated.pdf";   // Resulting PDF
        const int   startPage  = 2;                 // First page to import annotations onto (1‑based)
        const int   endPage    = 4;                 // Last page to import annotations onto (inclusive)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xfdfFile))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfFile}");
            return;
        }

        // Bind the target PDF to the annotation editor.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdf);

        // Import only the annotations that belong to the desired page range.
        // The ImportAnnotationsXfdf overload that accepts start/end page numbers
        // is not available, so we use ExportAnnotationsXfdf to extract the
        // relevant subset first, then import that temporary XFDF.
        string tempXfdf = Path.GetTempFileName();
        using (FileStream tempStream = File.Create(tempXfdf))
        {
            // Export the required page range from the original XFDF file.
            // ExportAnnotationsXfdf works on the bound PDF, so we first load the XFDF
            // into a temporary PDF document, export the desired pages, then import.
            // To avoid extra PDF creation, we use XfdfReader to read the XFDF into a
            // Document, then export the needed pages.
            Document tempDoc = new Document();
            using (FileStream xfdfStream = File.OpenRead(xfdfFile))
            {
                Aspose.Pdf.Annotations.XfdfReader.ReadAnnotations(xfdfStream, tempDoc);
            }

            // Bind the temporary document to a second editor to export the subset.
            PdfAnnotationEditor tempEditor = new PdfAnnotationEditor();
            tempEditor.BindPdf(tempDoc);
            // Export annotations from the desired page range to the temporary XFDF.
            tempEditor.ExportAnnotationsXfdf(tempStream, startPage, endPage, (AnnotationType[])null);
            tempEditor.Close();
        }

        // Now import the filtered XFDF back into the original PDF.
        editor.ImportAnnotationsFromXfdf(tempXfdf);
        editor.Save(outputPdf);
        editor.Close();

        // Clean up the temporary file.
        File.Delete(tempXfdf);

        Console.WriteLine($"Annotations from pages {startPage}-{endPage} imported to '{outputPdf}'.");
    }
}