using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_text_annots.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the annotation editor (lifecycle: constructor → BindPdf → DeleteAnnotations → Save → Close)
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Load the PDF document into the editor
        editor.BindPdf(inputPath);

        // Delete only annotations of type "Text"
        editor.DeleteAnnotations("Text");

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources held by the editor
        editor.Close();

        Console.WriteLine($"Text annotations removed. Saved to '{outputPath}'.");
    }
}