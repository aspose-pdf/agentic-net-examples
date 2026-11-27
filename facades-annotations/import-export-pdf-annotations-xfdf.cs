using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "proposal.pdf";
        const string importXfdf = "proposal.xfdf";
        const string exportXfdf = "roundtrip.xfdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(importXfdf))
        {
            Console.Error.WriteLine($"XFDF not found: {importXfdf}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(pdfPath);

            // ------------------------------------------------------------
            // Import annotations from the XFDF file into the PDF document
            // ------------------------------------------------------------
            using (FileStream xfdfIn = File.OpenRead(importXfdf))
            {
                // XfdfReader reads the XFDF and adds the annotations to the document
                XfdfReader.ReadAnnotations(xfdfIn, pdfDoc);
            }

            // ------------------------------------------------------------
            // Export the (now imported) annotations back to an XFDF file
            // ------------------------------------------------------------
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            // Bind the in‑memory Document (no need for a file path)
            editor.BindPdf(pdfDoc);

            using (FileStream xfdfOut = File.Create(exportXfdf))
            {
                int startPage = 1;
                int endPage   = pdfDoc.Pages.Count;
                // Pass null (or an empty string array) to export all annotation types
                string[] annotationTypes = null;
                editor.ExportAnnotationsXfdf(xfdfOut, startPage, endPage, annotationTypes);
            }

            Console.WriteLine($"Annotations imported from '{importXfdf}' and exported to '{exportXfdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
