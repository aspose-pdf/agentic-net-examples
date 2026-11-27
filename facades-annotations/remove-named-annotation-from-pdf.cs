using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "minutes.pdf";
        const string xfdfPath = "minutes.xfdf";
        const string outputPath = "UpdatedMinutes.pdf";

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

        // PdfAnnotationEditor does not implement IDisposable (as of current Aspose.PDF version)
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the PDF document
        editor.BindPdf(pdfPath);

        // Import annotations from the XFDF file (ImportAnnotations expects a string[] of file paths)
        editor.ImportAnnotations(new[] { xfdfPath });

        // Delete the annotation named "Note5" (DeleteAnnotation expects a single annotation name)
        editor.DeleteAnnotation("Note5");

        // Save the updated PDF
        editor.Save(outputPath);

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
