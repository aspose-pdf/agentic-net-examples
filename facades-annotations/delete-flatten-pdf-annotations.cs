using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor to delete all annotations, flatten the remaining ones, and save.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);          // load the PDF
            editor.DeleteAnnotations();         // remove all annotations
            editor.FlatteningAnnotations();    // flatten remaining annotations (e.g., form fields)
            editor.Save(outputPath);            // write the result
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
