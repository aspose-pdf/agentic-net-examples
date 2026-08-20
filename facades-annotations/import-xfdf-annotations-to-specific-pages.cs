using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // PDF to receive annotations
        const string xfdfPath       = "annotations.xfdf"; // XFDF file with annotations
        const string outputPdfPath  = "output.pdf";     // Resulting PDF
        const int  startPage        = 2;                // First page to keep annotations
        const int  endPage          = 4;                // Last page to keep annotations

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Load the target PDF
        using (Document targetDoc = new Document(inputPdfPath))
        {
            // Bind the PDF to the annotation editor facade
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(targetDoc);

                // Import all annotations from the XFDF file
                editor.ImportAnnotationsFromXfdf(xfdfPath);

                // Remove annotations from pages outside the desired range
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= targetDoc.Pages.Count; i++)
                {
                    if (i < startPage || i > endPage)
                    {
                        Page page = targetDoc.Pages[i];
                        // Delete all annotations on this page
                        while (page.Annotations.Count > 0)
                        {
                            // Annotations collection is also 1‑based
                            page.Annotations.Delete(page.Annotations.Count);
                        }
                    }
                }

                // Save the modified PDF
                editor.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Annotations imported to pages {startPage}-{endPage} and saved as '{outputPdfPath}'.");
    }
}