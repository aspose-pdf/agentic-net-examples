using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_text_annots.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into the annotation editor
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Delete only annotations of type "Text"
        editor.DeleteAnnotations("Text");

        // Save the resulting PDF
        editor.Save(outputPath);

        Console.WriteLine($"Text annotations removed. Saved to '{outputPath}'.");
    }
}