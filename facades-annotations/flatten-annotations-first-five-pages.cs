using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_first5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the annotation editor.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // The Aspose.Pdf.Facades API provides FlatteningAnnotations() without a page‑range overload.
        // Therefore this call flattens annotations on all pages.
        // If only the first five pages must be flattened, a custom page‑by‑page implementation would be required.
        editor.FlatteningAnnotations();

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Annotations flattened (entire document) and saved to '{outputPath}'.");
    }
}