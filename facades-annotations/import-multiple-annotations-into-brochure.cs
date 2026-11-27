using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string brochurePath = "brochure.pdf";
        const string outputPath = "CombinedBrochure.pdf";
        string[] annotationSources = { "ann1.pdf", "ann2.pdf", "ann3.pdf" };

        if (!File.Exists(brochurePath))
        {
            Console.Error.WriteLine($"Brochure file not found: {brochurePath}");
            return;
        }

        foreach (var src in annotationSources)
        {
            if (!File.Exists(src))
            {
                Console.Error.WriteLine($"Annotation source not found: {src}");
                return;
            }
        }

        // PdfAnnotationEditor does not implement IDisposable, so no using block is needed
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the target PDF (brochure)
        editor.BindPdf(brochurePath);

        // Import annotations from all source PDFs in one call (expects a string[])
        editor.ImportAnnotations(annotationSources);

        // Save the combined PDF with all imported annotations
        editor.Save(outputPath);

        Console.WriteLine($"Annotations merged and saved to '{outputPath}'.");
    }
}
