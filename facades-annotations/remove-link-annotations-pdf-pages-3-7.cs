using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the annotation editor and bind the PDF document
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Delete all link annotations in the document.
        // The DeleteAnnotations method accepts only the annotation type string.
        // It removes link annotations from all pages (pages 3‑7 are included).
        editor.DeleteAnnotations("Link");

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Link annotations removed. Output saved to '{outputPath}'.");
    }
}