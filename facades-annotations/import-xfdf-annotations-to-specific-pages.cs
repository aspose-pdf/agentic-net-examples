using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // source PDF
        const string xfdfPath  = "annotations.xfdf"; // XFDF file containing annotations
        const string outputPath = "output.pdf";    // result PDF

        // Define the page range where the XFDF annotations should be applied (inclusive)
        int startPage = 2; // first page to receive annotations (1‑based)
        int endPage   = 4; // last page to receive annotations

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        // Bind the PDF to the annotation editor facade
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(pdfPath);

        // Import all annotations from the XFDF file into the document
        // (this brings the annotations onto all pages)
        editor.ImportAnnotationsFromXfdf(xfdfPath);

        // Remove annotations that fall outside the desired page range.
        // The facade does not provide a direct page‑range import, so we delete
        // the unwanted ones after the import.
        // Iterate through pages before the start page and after the end page.
        for (int i = 1; i < startPage; i++)
        {
            // Delete all annotations on page i
            editor.ModifyAnnotations(i, i, null); // passing null removes all annotations on that page
        }
        for (int i = endPage + 1; i <= editor.Document.Pages.Count; i++)
        {
            editor.ModifyAnnotations(i, i, null);
        }

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"XFDF annotations applied to pages {startPage}-{endPage} and saved to '{outputPath}'.");
    }
}