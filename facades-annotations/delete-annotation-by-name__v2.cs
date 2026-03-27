using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string annotationName = "4cfa69cd-9bff-49e0-9005-e22a77cebf38";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Aspose.Pdf.Facades.PdfAnnotationEditor editor = new Aspose.Pdf.Facades.PdfAnnotationEditor();
        editor.BindPdf(inputPath);
        editor.DeleteAnnotation(annotationName);
        editor.Save(outputPath);
        Console.WriteLine($"Annotation '{annotationName}' deleted. Saved to '{outputPath}'.");
    }
}