using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // PDF to receive annotations
        const string xfdfPath  = "annotations.xfdf"; // XFDF file containing annotations
        const string outputPath = "output.pdf";

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

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Initialize the annotation editor facade
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF document to the editor
                editor.BindPdf(pdfDoc);

                // Import all annotations from the XFDF file
                // (Aspose.Pdf does not provide a page‑range overload for import;
                //  annotations are imported to the pages they were originally created on.)
                editor.ImportAnnotationsFromXfdf(xfdfPath);

                // If you need to limit annotations to a specific page range,
                // you can remove annotations outside that range after import.
                // Example: keep only pages 2‑4.
                int startPage = 2;
                int endPage   = 4;
                // Flatten (or delete) annotations outside the desired range
                // by flattening the whole document first and then re‑adding
                // only the needed ones is one approach; here we simply
                // flatten annotations on pages outside the range.
                // Flatten pages before the range
                if (startPage > 1)
                {
                    editor.FlatteningAnnotations(1, startPage - 1, null);
                }
                // Flatten pages after the range
                if (endPage < pdfDoc.Pages.Count)
                {
                    editor.FlatteningAnnotations(endPage + 1, pdfDoc.Pages.Count, null);
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}