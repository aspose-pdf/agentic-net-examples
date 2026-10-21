using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string samplePath = "sample.pdf";
        const string outputPath = "bound_output.pdf";

        if (!File.Exists(samplePath))
        {
            Console.Error.WriteLine($"Sample PDF not found: {samplePath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor does not implement IDisposable
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(samplePath);          // Verify binding
            editor.Save(outputPath);             // Save to confirm no errors
            Console.WriteLine($"PdfAnnotationEditor bound and saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"PdfAnnotationEditor binding failed: {ex.Message}");
        }
    }
}