using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "flattened.pdf";

        // Verify input files exist.
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

        // Load the PDF document.
        Document doc = new Document(pdfPath);

        // Import annotations from the XFDF file.
        // The high‑level Document API provides ImportAnnotationsFromXfdf.
        doc.ImportAnnotationsFromXfdf(xfdfPath);

        // Flatten the imported annotations so they become part of the page content.
        // PdfAnnotationEditor (not PdfContentEditor) provides the FlatteningAnnotations method.
        using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
        {
            annotEditor.BindPdf(doc);
            annotEditor.FlatteningAnnotations(); // correct method name
            annotEditor.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and flattened. Saved to '{outputPath}'.");
    }
}
