using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output PDF path.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: AsposePdfCleaner <input-pdf> <output-pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the incoming PDF into a stream.
        using var inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
        var doc = new Document(inputStream);

        // Remove all annotations using Aspose.Pdf.Facades.
        var annotationEditor = new PdfAnnotationEditor();
        annotationEditor.BindPdf(doc);
        annotationEditor.DeleteAnnotations();               // delete all annotations
        annotationEditor.FlatteningAnnotations();           // flatten any remaining annotation objects

        // Save the cleaned PDF to the output stream.
        using var outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
        doc.Save(outputStream);

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}
