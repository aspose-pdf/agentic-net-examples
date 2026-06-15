using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF and XFDF files
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdf = "output.pdf";

        // Define the page range (1‑based inclusive) where the XFDF annotations should be applied
        int startPage = 2; // first page to receive annotations
        int endPage = 4;   // last page to receive annotations

        // Ensure the source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Create the annotation editor and bind the target PDF
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(pdfPath);

        // Import all annotations from the XFDF file
        // (Aspose.Pdf does not provide a direct page‑range overload for import,
        //  so we import first and then remove any annotations that fall outside the desired range.)
        editor.ImportAnnotationsFromXfdf(xfdfPath);

        // Remove annotations that are outside the specified page range
        // Iterate through all pages and delete annotations on pages before startPage or after endPage.
        for (int i = 1; i <= editor.Document.Pages.Count; i++)
        {
            if (i < startPage || i > endPage)
            {
                var annotations = editor.Document.Pages[i].Annotations;
                // AnnotationCollection is 1‑based; delete from the end to avoid index shifting
                for (int j = annotations.Count; j >= 1; j--)
                {
                    annotations.Delete(j);
                }
            }
        }

        // Save the resulting PDF
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Annotations imported to pages {startPage}-{endPage} and saved as '{outputPdf}'.");
    }
}
