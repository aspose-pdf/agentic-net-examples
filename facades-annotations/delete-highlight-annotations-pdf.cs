using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "report.pdf";
        const string outputPath = "UpdatedReport.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable, so wrap it in a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Delete all highlight annotations. The overload expects a string, so pass the enum name.
            editor.DeleteAnnotations(AnnotationType.Highlight.ToString());

            // Save the updated PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
