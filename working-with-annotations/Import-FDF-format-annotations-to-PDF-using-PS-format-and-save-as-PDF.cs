using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPsPath = "input.ps";          // PostScript source
        const string fdfPath = "annotations.fdf";       // FDF with annotations
        const string outputPdfPath = "output.pdf";      // Resulting PDF

        // Verify source files exist
        if (!File.Exists(inputPsPath))
        {
            Console.Error.WriteLine($"PostScript file not found: {inputPsPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Load the PS file (input‑only format) into a Document
        using (Document doc = new Document(inputPsPath, new PsLoadOptions()))
        {
            // Bind the document to the annotation editor facade
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);

            // Import all annotations from the FDF file
            editor.ImportAnnotationsFromFdf(fdfPath);

            // Save the modified document as PDF (no SaveOptions needed)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPdfPath}'.");
    }
}