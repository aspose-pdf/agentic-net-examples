using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the annotation editor and flatten annotations
        using (var editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Flatten all remaining annotations (does not trigger form events,
            // applies redactions and hides button annotations internally)
            editor.FlatteningAnnotations();

            // Save the flattened PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}
